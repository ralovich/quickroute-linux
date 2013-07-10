using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Config;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;


namespace QuickRoute.Common
{
  public static class LogUtil
  {
    private static bool configured;
    private static decimal lastTime = -1;
#if __MonoCS__
	  private static readonly Dictionary<object, HighPerformanceTimerNix> timers = new Dictionary<object, HighPerformanceTimerNix>();
	  private static readonly HighPerformanceTimerBase standardTimer = new HighPerformanceTimerNix();
#else
    private static readonly Dictionary<object, HighPerformanceTimer> timers = new Dictionary<object, HighPerformanceTimer>();
    private static readonly HighPerformanceTimerBase standardTimer = new HighPerformanceTimer();
#endif

    public static void MonoFixMe()
    {
      MonoFixMe(null);
    }

    public static void MonoFixMe(string message)
    {
      LogDebug("MONO FIXME\n" + message==null?"":message);
    }

	  public static bool IsRunningOnMono ()
    {
      return Type.GetType ("Mono.Runtime") != null;
    }

    public static void LogDebug(string message)
    {
      WriteToLog(message, LogLevel.Debug);
    }

    public static void LogInfo(string message)
    {
      WriteToLog(message, LogLevel.Info);
    }

    public static void LogWarn(string message)
    {
      WriteToLog(message, LogLevel.Warn);
    }

    public static void LogError(string message)
    {
      WriteToLog(message, LogLevel.Error);
    }

    public static void LogFatal(string message)
    {
      WriteToLog(message, LogLevel.Fatal);
    }

    public static void LogTrace()
    {
      StackTrace st = new StackTrace(true);
      string stackIndent = "";
      for(int i =0; i< st.FrameCount; i++ )
      {
        StackFrame sf = st.GetFrame(i);
        WriteToLog(stackIndent + sf.ToString(), LogLevel.Debug);
        WriteToLog(stackIndent + "Method: " + sf.GetMethod().ToString(), LogLevel.Debug );
        WriteToLog(stackIndent + "File: " + sf.GetFileName(), LogLevel.Debug);
        WriteToLog(stackIndent + "Line Number: " + sf.GetFileLineNumber(), LogLevel.Debug);
        stackIndent += " ";
      }
    }
  
    private static void WriteToLog(string message, LogLevel level)
    {
      if (!configured) throw new Exception("The LogUtil is not configured.");

      var thisTime = standardTimer.GetCurrentTime();
      var duration = (lastTime == -1 ? 0 : thisTime - lastTime);
      lastTime = thisTime;
      var caller = GetCaller();
      var log = log4net.LogManager.GetLogger(caller.DeclaringType);

      var m = String.Format("{0:0.000}", thisTime) + " " +
              String.Format("{0:0.000}", duration) + " " +
              caller.Name + ": " +
              message;
      switch (level)
      {
        case LogLevel.Debug:
          log.Debug(m);
          break;
        case LogLevel.Info:
          log.Info(m);
          break;
        case LogLevel.Warn:
          log.Warn(m);
          break;
        case LogLevel.Error:
          log.Error(m);
          break;
        case LogLevel.Fatal:
          log.Fatal(m);
          break;
      }
      // Debugging aid, shows up in monodevelop console.
      //System.Console.WriteLine(m);
    }

    public static void Configure()
    {
      Configure(null);
    }

    public static void Configure(string logFileName)
    {
      XmlConfigurator.Configure();
      if (logFileName != null)
      {
        // log to the specified log file name
        var hierarchy = (log4net.Repository.Hierarchy.Hierarchy) LogManager.GetRepository();
        foreach (var a in hierarchy.Root.Appenders)
        {
          if (a is FileAppender)
          {
            var fa = (FileAppender) a;
            fa.File = logFileName;
            fa.ActivateOptions();
          }
        }
      }

      configured = true;
    }

    private static MethodBase GetCaller()
    {
      var stackFrame = new StackTrace().GetFrame(3);
      return stackFrame.GetMethod();
    }

    private static HighPerformanceTimerBase GetTimer()
    {
      return standardTimer;
    }

    private static HighPerformanceTimerBase GetTimer(object key)
    {
      if (!timers.ContainsKey(key)) timers.Add(key, new HighPerformanceTimerNix());
      return timers[key];
    }

    private static void DisposeTimer(object key)
    {
      if (timers.ContainsKey(key)) timers.Remove(key);
    }

  }

  public abstract class HighPerformanceTimerBase
  {
    // Start the timer
    public abstract decimal Start();

    // Stop the timer
    public abstract decimal Stop();

    // Returns the duration of the timer (in seconds)
    public virtual decimal Duration { get; set; }

    public abstract void Reset();

    public void ResetAndStart()
    {
      Reset();
      Start();
    }

    public abstract decimal GetCurrentTime();
  }

  public class HighPerformanceTimer : HighPerformanceTimerBase
  {
    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(out long lpFrequency);

    private long startTime, stopTime;
    private static readonly long freq;

    private bool isStarted;
    private decimal duration;

    static HighPerformanceTimer()
    {
      if (QueryPerformanceFrequency(out freq) == false)
      {
        // high-performance counter not supported
        throw new Win32Exception();
      }
    }

    // Constructor
    public HighPerformanceTimer()
      : this(false)
    {
    }

    public HighPerformanceTimer(bool startImmediately)
    {
      if (startImmediately) Start();
    }

    // Start the timer
    public override decimal Start()
    {
      // lets do the waiting threads there work
      Thread.Sleep(0);

      QueryPerformanceCounter(out startTime);
      isStarted = true;
      return duration;
    }

    // Stop the timer
    public override decimal Stop()
    {
      if (!isStarted) return duration;
      QueryPerformanceCounter(out stopTime);
      duration += (decimal)(stopTime - startTime) / freq;
      return duration;
    }

    // Returns the duration of the timer (in seconds)
    public override decimal Duration
    {
      get
      {
        if (isStarted)
        {
          long currentTime=0;
          QueryPerformanceCounter(out currentTime);
          return duration + (decimal)(currentTime - startTime) / freq;
        }
        return duration;
      }
    }

    public override void Reset()
    {
      isStarted = false;
      duration = 0;
    }

    // in seconds
    public override decimal GetCurrentTime()
    {
      long currentTime=0;
      QueryPerformanceCounter(out currentTime);
      return (decimal)currentTime / freq;
    }
  }

  public class HighPerformanceTimerNix : HighPerformanceTimerBase
  {
    struct timeval
    {
      public int seconds;
      public int useconds;
    }

    [DllImport ("libc")]
    static extern int gettimeofday (out timeval tv, IntPtr unused);

    private double startTime, stopTime;

    private bool isStarted;
    private decimal duration;
    

		public HighPerformanceTimerNix()
			: this(false)
		{
		}

		public HighPerformanceTimerNix(bool startImmediately)
		{
			if(startImmediately)
				Start();
		}

		public override decimal Start()
		{
			Thread.Sleep(0);
			timeval t;
      IntPtr unused;
			gettimeofday(out t, unused);
			startTime = (double)t.seconds + ((double)t.useconds)/1000000.0;
			System.Console.WriteLine("startTime={0}\n", startTime);
			isStarted = true;
			return duration;
		}

    public override decimal Stop()
    {
      if (!isStarted) return duration;
      timeval t;
      IntPtr unused;
      gettimeofday(out t, unused);
      stopTime = t.seconds + ((double)t.useconds)/1000000.0;
      System.Console.WriteLine("stopTime={0}\n", stopTime);
      //QueryPerformanceCounter(out stopTime);
      duration += (decimal)(stopTime - startTime);
      return duration;
    }

    public override void Reset()
    {
      isStarted = false;
      duration = 0;
    }

    public override decimal GetCurrentTime()
    {
      timeval t;
      IntPtr unused;
      gettimeofday(out t, unused);
      return (decimal)(((double)t.seconds) + ((double)t.useconds)/1000000.0);
    }
  }

  public enum LogLevel
  {
    Debug,
    Info,
    Warn,
    Error,
    Fatal
  }

}
