namespace FileManagerLab.Model.Tabs;

public class ImageFileTab : BaseTab
{
    private string _path;
    public ImageFileTab(string path)
    {
        _path = path;
    }
    public override void GenerateContent()
    {
        TabName = _path;
        Content = _path;
    }
}