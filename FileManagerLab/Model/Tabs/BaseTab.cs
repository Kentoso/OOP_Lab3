using CommunityToolkit.Mvvm.ComponentModel;

namespace FileManagerLab.Model.Tabs;

public abstract partial class BaseTab : ObservableObject
{
    [ObservableProperty]
    public string content;
    public string TabName { get; set; }
    protected string _path;
    public abstract void GenerateContent();
}