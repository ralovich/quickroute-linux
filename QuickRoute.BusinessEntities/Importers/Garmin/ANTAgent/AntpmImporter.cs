// -*- mode: csharp; tab-width: 2; indent-tabs-mode: nil; c-basic-offset: 2; coding: utf-8-unix -*-
// ***** BEGIN LICENSE BLOCK *****
////////////////////////////////////////////////////////////////////
// Copyright (c) 2013 RALOVICH, Kristóf                           //
//                                                                //
// This program is free software; you can redistribute it and/or  //
// modify it under the terms of the GNU General Public License    //
// version 2 as published by the Free Software Foundation.        //
//                                                                //
////////////////////////////////////////////////////////////////////
// ***** END LICENSE BLOCK *****

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using QuickRoute.BusinessEntities.Importers.FIT;


namespace QuickRoute.BusinessEntities.Importers.Garmin.ANTAgent
{
  // importer for ANT+minus https://code.google.com/p/antpm/
  public class AntpmImporter : IGPSDeviceImporter
  {
    public AntpmImporter ()
    {
      mPath = getConfigFolder();
      System.Console.WriteLine("AntpmImporter: " + mPath);
    }

    private string getConfigFolder ()
    {
      //return "/home/tade/.config/antpm";

      string s1 = "/home/tade";
      string s2 = "/.config/antpm";
      string s3 = Path.Combine(s1,s2);

      string e0 = Environment.GetEnvironmentVariable ("ANTPM_DIR");
      if (!String.IsNullOrEmpty (e0))
        return Path.Combine (e0, "");

      // http://standards.freedesktop.org/basedir-spec/basedir-spec-latest.html
      // $XDG_CONFIG_HOME defines the base directory relative to which user specific configuration files should be stored.
      // If $XDG_CONFIG_HOME is either not set or empty, a default equal to $HOME/.config should be used. 

      string e1 = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
      string e2 = Environment.GetEnvironmentVariable("HOME");
      string e3 = Environment.GetEnvironmentVariable("USERPROFILE");
      Console.WriteLine("e1=" + e1);
      Console.WriteLine("e2=" + e2);
      Console.WriteLine("e3=" + e3);
      Console.WriteLine("e0=" + e0);
      if(!String.IsNullOrEmpty(e1))
        return Path.Combine(e1, "antpm/");
      else if(!String.IsNullOrEmpty(e2))
        return Path.Combine(e2, ".config/antpm/");
      else if(!String.IsNullOrEmpty(e3))
        return Path.Combine(e3, ".config/antpm/");
      else
        return Path.Combine("~", ".config/antpm/");
    }


    private HistoryItem itemToImport;

    #region IRouteImporter Members

    public ImportResult ImportResult { get; set; }

    /// <summary>
    /// The ...\\.config\\antpm\\ path. Shall end with a backslash.
    /// </summary>
    public string mPath { get; set; }

    public DialogResult ShowPreImportDialogs()
    {
      var historyItems = new List<object>();
      var baseDir = new DirectoryInfo(mPath);
      if (baseDir.Exists)
      {
        foreach (DirectoryInfo di in baseDir.GetDirectories())
        {
          string folder = mPath + di.Name + "\\";
          Console.WriteLine(folder);
          //DirectoryInfo di = new DirectoryInfo(folder);
          foreach(FileInfo fi in di.GetFiles("*.fit", SearchOption.AllDirectories))
          {
            string name = fi.FullName;
            string id = fi.LastWriteTimeUtc.ToString();
            Console.WriteLine("name: " + name + ", id=" + id);
            HistoryItem hi = new HistoryItem(name, id, fi);
            historyItems.Insert(0, hi);
          }
        }
      }

      using (var dlg = new SessionSelector())
      {
        if (BeginWork != null) BeginWork(this, new EventArgs());
        dlg.Sessions = historyItems;
        if (BeginWork != null) EndWork(this, new EventArgs());
        DialogResult result = dlg.ShowDialog();
        if (result == DialogResult.OK)
        {
          itemToImport = (HistoryItem)dlg.SelectedSession;
        }
        dlg.Dispose();

        return result;
      }
    }

    public void Import()
    {
      ImportResult = new ImportResult();
      var fitImporter = new FITImporter
                          {
                            FileName = itemToImport.FileInfo.FullName//,
                            /*IdToImport = DateTime.Parse(itemToImport.Id).ToString("yyyy-MM-dd HH:mm:ss")*/
                          };
      fitImporter.Import();
      ImportResult = fitImporter.ImportResult;
    }

    public event EventHandler<EventArgs> BeginWork;
    public event EventHandler<EventArgs> EndWork;
    public event EventHandler<WorkProgressEventArgs> WorkProgress;

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
        return "ANT+minus";
      }
    }

    public void Refresh()
    {
      // do nothing
    }

    #endregion
  }
}

