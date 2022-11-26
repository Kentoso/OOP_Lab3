using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

namespace FileManagerLab.Model;

public partial class ApplicationDirectory : ObservableObject
{
    [ObservableProperty] 
    private DirectoryInfo info;
    [ObservableProperty]
    private List<ApplicationFile> fileInfo;
    [ObservableProperty]
    private List<ApplicationDirectory> children;
    
    public ApplicationDirectory(string path)
    {
        if (path.Trim().Length == 0)
        {
            Children = DriveInfo.GetDrives().Select(d => new ApplicationDirectory(d.RootDirectory)).ToList();
            FileInfo = null;
        }
        else
        {
            Info = new DirectoryInfo(path);
            UpdateInfo();
        }
    }

    public ApplicationDirectory(DirectoryInfo info)
    {
        Info = info;
    }
    public void GoUp()
    {
        if (Info.Parent != null)
        {
            Info = Info.Parent;
            UpdateInfo();
        }
        else
        {
            Info = null;
            Children = DriveInfo.GetDrives().Select(d => new ApplicationDirectory(d.RootDirectory)).ToList();
            FileInfo = null;
        }
    }

    public void GoDown(ApplicationDirectory child)
    {
        Info = child.Info;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        Children = Info.GetDirectories().Where(d => Directory.Exists(d.FullName) && !d.Attributes.HasFlag(FileAttributes.Hidden)).Select(d => new ApplicationDirectory(d)).ToList();
        var files = Info.GetFiles();
        FileInfo = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
            .Select(f => new ApplicationFile(f)).ToList(); 
    }
}