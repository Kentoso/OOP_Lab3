using System;
using System.IO;
using System.Windows;
using System.Windows.Automation;
using CommunityToolkit.Mvvm.Input;

namespace FileManagerLab.Model.Tabs;

public partial class TextFileTab : BaseTab
{
    public TextFileTab(string path)
    {
        _path = path;
    }
    public override void GenerateContent()
    {
        TabName = Path.GetFileName(_path); ;
        Content = File.ReadAllText(_path);
    }

    [RelayCommand]
    private void SaveFile()
    {
        try
        {
            Save();
        }
        catch (Exception e)
        {
            MessageBox.Show($"There was an exception while saving the file: {e.Message}", "Exception",
                MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }
    }
    
    private void Save()
    {
        File.WriteAllText(_path, Content);
    }
}