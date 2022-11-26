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
    public DirectoryInfo info;
    [ObservableProperty]
    public List<ApplicationFile> fileInfo;
    [ObservableProperty]
    public List<ApplicationDirectory> children;
    
    public ApplicationDirectory(string path)
    {
        Info = new DirectoryInfo(path);
        UpdateInfo();
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
            Children = DriveInfo.GetDrives().Select(d => new ApplicationDirectory(d.RootDirectory)).ToList();
            FileInfo = null;
        }
    }

    public void GoDown(ApplicationDirectory child)
    {
        Info = child.Info;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Children = Info.GetDirectories().Where(d => Directory.Exists(d.FullName) && !d.Attributes.HasFlag(FileAttributes.Hidden)).Select(d => new ApplicationDirectory(d)).ToList();
        var files = Info.GetFiles();
        FileInfo = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
            .Select(f => new ApplicationFile(f)).ToList(); 
    }
}