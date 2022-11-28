using System.IO;

namespace FileManagerLab.Model.Tabs;

public class ImageFileTab : BaseTab
{
    public ImageFileTab(string path)
    {
        _path = path;
    }
    public override void GenerateContent()
    {
        TabName = Path.GetFileName(_path);
        Content = _path;
    }
}