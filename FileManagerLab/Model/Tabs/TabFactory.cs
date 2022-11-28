using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace FileManagerLab.Model.Tabs;

public class TabFactory
{
    private static TabFactory _instance;
    private static string[] _imageTypes;
    public static TabFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TabFactory();
            }
            return _instance;
        }
    }

    private TabFactory()
    {
        _imageTypes = new string[] { ".png", ".jpg"};
    }
    public BaseTab CreateTabFromPath(string path)
    {
        if (!File.Exists(path)) return null;
        var extension = Path.GetExtension(path);
        if (extension == ".txt") return new TextFileTab(path);
        if (_imageTypes.Contains(extension)) return new ImageFileTab(path);
        if (extension == "") return new ByteFileTab(path);
        return null;
    }
}