using System;
using System.IO;
using System.Runtime.InteropServices;
using QuickRoute.Common;
#if __MonoCS__
using System.Diagnostics;
#endif

namespace QuickRoute.UI.Classes
{
  public static class GoogleEarthUtil
  {
#if !__MonoCS__
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
#endif

    public static void OpenInGoogleEarth(Stream stream)
    {
      // create file from stream
      var reader = new BinaryReader(stream);
      stream.Position = 0;
      var data = reader.ReadBytes((int)stream.Length);
      reader.Close();
      var fileName = CommonUtil.GetTempFileName("kmz");
      Console.WriteLine(fileName);
      var fileStream = File.Create(fileName);
      fileStream.Write(data, 0, data.Length);
      fileStream.Close();
      fileStream.Dispose();
      
#if !__MonoCS__
      var googleEarthApplication = new EARTHLib.ApplicationGEClass();
      googleEarthApplication.OpenKmlFile(fileName, 1);
      BringWindowToForeground((IntPtr)googleEarthApplication.GetMainHwnd());
#else
      // Start the child process.
      Process p = new Process();
      p.EnableRaisingEvents = false;
      p.StartInfo.UseShellExecute = false;
      p.StartInfo.RedirectStandardOutput = false;

      string command = GetGoogleEarthPath();
      string args = " " + fileName;
      Console.WriteLine(command);
      Console.WriteLine(args);
      p.StartInfo.FileName = command;
      p.StartInfo.Arguments = args;
      p.Start();
      //string output = p.StandardOutput.ReadToEnd();
      //p.WaitForExit();
#endif
    }

#if !__MonoCS__
    private static void BringWindowToForeground(IntPtr hWnd)
    {
      SetForegroundWindow(hWnd);
    }
#else
    private static string GetGoogleEarthPath()
    {
      
      if(File.Exists("/usr/bin/google-earth"))
        return "/usr/bin/google-earth";
      else
        return "/opt/google/earth/free/google-earth";
    }
#endif


  }


}
