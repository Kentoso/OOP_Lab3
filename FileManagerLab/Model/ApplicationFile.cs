using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FileManagerLab.Model;

public partial class ApplicationFile : ObservableObject
{
    [ObservableProperty] public FileInfo fileInfo;
    [ObservableProperty] public BitmapSource fileIcon;

    public ApplicationFile(FileInfo fInf)
    {
        fileInfo = fInf;
        using (var icon = Icon.ExtractAssociatedIcon(fileInfo.FullName))
        {
            fileIcon = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        
    }
}