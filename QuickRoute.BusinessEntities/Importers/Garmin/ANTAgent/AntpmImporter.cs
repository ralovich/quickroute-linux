// -*- mode: csharp; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2; coding: utf-8-unix -*-
// ***** BEGIN LICENSE BLOCK *****
////////////////////////////////////////////////////////////////////
// Copyright (c) 2013 RALOVICH, Kristóf                           //
//                                                                //
// This program is free software; you can redistribute it and/or  //
// modify it under the terms of the GNU General Public License    //
// version 3 or later as published by the Free Software Foundation//
//                                                                //
////////////////////////////////////////////////////////////////////
// ***** END LICENSE BLOCK *****

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using QuickRoute.BusinessEntities.Importers.FIT;
using QuickRoute.Common;


namespace QuickRoute.BusinessEntities.Importers.Garmin.ANTAgent
{
  // importer for ANT+minus https://code.google.com/p/antpm/
  public class AntpmImporter : IGPSDeviceImporter
  {
    public AntpmImporter ()
    {
      //LogUtil.LogTrace();

      mFitFileList = new List<FitFile> ();
      mPath = getConfigFolder();
      LogUtil.LogInfo("AntpmImporter: " + mPath);

    }

    private string getConfigFolder ()
    {
      string e0 = Environment.GetEnvironmentVariable ("ANTPM_DIR");

      // http://standards.freedesktop.org/basedir-spec/basedir-spec-latest.html
      // $XDG_CONFIG_HOME defines the base directory relative to which user specific configuration files should be stored.
      // If $XDG_CONFIG_HOME is either not set or empty, a default equal to $HOME/.config should be used. 

      string e1 = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
      string e2 = Environment.GetEnvironmentVariable("HOME");
      string e3 = Environment.GetEnvironmentVariable("USERPROFILE");
      //LogUtil.LogDebug("e1=" + e1);
      //LogUtil.LogDebug("e2=" + e2);
      //LogUtil.LogDebug("e3=" + e3);
      //LogUtil.LogDebug("e0=" + e0);

      if (!String.IsNullOrEmpty (e0))
        return Path.Combine (e0, "");
      if(!String.IsNullOrEmpty(e1))
        return Path.Combine(e1, "antpm/");
      else if(!String.IsNullOrEmpty(e2))
        return Path.Combine(e2, ".config/antpm/");
      else if(!String.IsNullOrEmpty(e3))
        return Path.Combine(e3, ".config/antpm/");
      else
        return Path.Combine("~", ".config/antpm/");
    }

    private void discover()
    {
      if(!IsConnected)
      {
        LogUtil.LogWarn("\""+mPath+"\" doesn't exist\n");
        return;
      }
      mFitFileList.Clear();
      LogUtil.LogDebug("Listing \"" + mPath + "\"...");
      var baseDir = new DirectoryInfo(mPath);
      int nDirs = baseDir.GetDirectories().Length;
      foreach (DirectoryInfo di in baseDir.GetDirectories())
      {
        string folder = mPath + di.Name + "\\";
        LogUtil.LogDebug(folder);
        foreach(FileInfo fi in di.GetFiles("*.fit", SearchOption.AllDirectories))
        {
          string name = fi.FullName;
     
          // skip directory file     
          if(name.EndsWith("0000.fit"))
            continue;

          // try getting fit activity timestamp -> date
          // skip files without activity/date
          var fiti = new FITImporter{ FileName = name };
          fiti.Import();
          if(!fiti.ImportResult.Succeeded
             || fiti.CreationTime==InvalidCreationTime)
          {
            //LogUtil.LogDebug("name=" + fi.Name + " NO_DATE");
            continue;
          }
          
          FitFile ff = new FitFile ();
          ff.fi = fi;
          ff.fit = fiti;
          mFitFileList.Add (ff);
        }
      }
      LogUtil.LogDebug("Found " + nDirs + " dirs, " + mFitFileList.Count + " files");

//      for(int i = 0; i < mFileList.Count; i++)
//      {
//        FileInfo fi = mFileList[i];
//        FITImporter fiti = mFitList[i];
//        LogUtil.LogDebug("n=" + fi.Name +
//                         ", lw=" + fi.LastWriteTime.ToString() +
//                         ", ct=" + fiti.CreationTime.ToString() +
//                         ", f="  + fiti.FirstTime.ToString() +
//                         ", l="  + fiti.LastTime.ToString());
//      }

      mFitFileList.Sort((x, y) => x.fit.CreationTime.CompareTo(y.fit.CreationTime));
    }

    private class FitFile {
      public FileInfo    fi;
      public FITImporter fit;
    };

    private HistoryItem itemToImport;
    /// <summary>
    /// The list of FIT files with activities and timestamps.
    /// </summary>
    private List<FitFile> mFitFileList;
    /// <summary>
    /// The ...\\.config\\antpm\\ path. Shall end with a backslash.
    /// </summary>
    public string mPath { get; set; }

    #region IRouteImporter Members

    public ImportResult ImportResult { get; set; }

    public DialogResult ShowPreImportDialogs()
    {
      discover();

      var historyItems = new List<object>();
      for(int i = 0; i < mFitFileList.Count; i++)
      {
        FileInfo fi = mFitFileList[i].fi;
        FITImporter fiti = mFitFileList[i].fit;
        string displayName = fi.Name;
        // Previously we used the last-written time of the .FIT
        // file, but this might be off for various reasons, thus
        // the most accuracy comes from inside the .FIT itself.
        //string id = fi.LastWriteTime.ToString();
        string id = fiti.CreationTime.ToLocalTime().ToString();
        HistoryItem hi = new HistoryItem(displayName, id, fi);
        historyItems.Add(hi);
      }

      using (var dlg = new SessionSelector())
      {
        if (BeginWork != null) BeginWork(this, new EventArgs());
        dlg.Sessions = historyItems;
        if (BeginWork != null) EndWork(this, new EventArgs());
        DialogResult result = dlg.ShowDialog();
        if (result == DialogResult.OK)
        {
          if(historyItems.Count<1)
            itemToImport = null;
          else
            itemToImport = (HistoryItem)dlg.SelectedSession;
        }
        dlg.Dispose();

        return result;
      }
    }

    public void Import()
    {
      if(itemToImport == null)
      {
        ImportResult = new ImportResult();
        ImportResult.Succeeded = false;
        ImportResult.Error = ImportError.Unknown;
        ImportResult.ErrorMessage = "No valid FIT activities in folder";
        return;
      }
        
      LogUtil.LogDebug("Importing \"" + itemToImport.ToString() + "\"\n");
      ImportResult = new ImportResult();
      var fitImporter = new FITImporter { FileName = itemToImport.FileInfo.FullName };
      fitImporter.Import();
      ImportResult = fitImporter.ImportResult;
    }

    public event EventHandler<EventArgs> BeginWork;
    public event EventHandler<EventArgs> EndWork;
    public event EventHandler<WorkProgressEventArgs> WorkProgress;
    private DateTime InvalidCreationTime = new DateTime(1989, 12, 31, 00, 00, 00, DateTimeKind.Utc);

    #endregion


    #region IGPSDeviceImporter Members

    public bool IsConnected
    {
      get
      {
        bool exists = Directory.Exists(mPath);
        return exists;
      }
    }

    public bool CachedDataExists
    {
      get { return IsConnected; }
    }

    public string DeviceName
    {
      get
      {
        return "ANT+minus (" + mPath + ")";
      }
    }

    public void Refresh()
    {
      discover ();
    }

    #endregion
  }
}

