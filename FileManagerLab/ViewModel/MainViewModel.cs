using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileManagerLab.Model;
using FileManagerLab.Model.Tabs;
using FileManagerLab.MVVM;
using Microsoft.Xaml.Behaviors.Core;

namespace FileManagerLab.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] public ApplicationDirectory hierarchy;

    [ObservableProperty]
    public ApplicationDirectory leftHierarchySelectedItem;
    
    [ObservableProperty]
    public BaseTab currentTab;
    public ICommand SelectFolderItemCommand { get; set; } 
    public ICommand DirectoryListMouseClickCommand { get; set; }
    
    [ObservableProperty]
    public ObservableCollection<BaseTab> tabs;
    public MainViewModel()
    {
        Hierarchy = new ApplicationDirectory(@"C:\etc\websites\ads");
        tabs = new ObservableCollection<BaseTab>();
        tabs.Add(new FileManagerTab());
        tabs.Add(new ImageFileTab(@"C:\Users\Demian\Downloads\AD2.png"));
        tabs.Add(new TextFileTab(@"C:\Users\Demian\Downloads\Yevgeniy Vicktorovich Prigozhin3.pdf"));
        tabs.Add(new ByteFileTab(@"C:\Users\Demian\Downloads\Yevgeniy Vicktorovich Prigozhin3.pdf"));
        tabs[1].GenerateContent();
        tabs[2].GenerateContent();
        tabs[3].GenerateContent();
        SelectFolderItemCommand = new ApplicationCommand((p) =>
        {
            SelectFolderItem(p);
        });
        DirectoryListMouseClickCommand = new ActionCommand((p) =>
        {
            DirectoryListMouseClick(p);
        });
        // var a = DriveInfo.GetDrives();
    }

    [RelayCommand]
    private void GoUpLeftHierarchy()
    {
        Hierarchy.GoUp();
    }
    
    private void SelectFolderItem(object parameter)
    {
        SelectionChangedEventArgs p = parameter as SelectionChangedEventArgs;
        if (p.AddedItems.Count == 0) return;
        var addedItem = p.AddedItems[0] as ApplicationDirectory;
        LeftHierarchySelectedItem = addedItem;
        Debug.Write(LeftHierarchySelectedItem.Info.Name);
    }
    
    private void DirectoryListMouseClick(object parameter)
    {
        var p = parameter as MouseButtonEventArgs;
        if (p == null) return;
        if (p.ChangedButton == MouseButton.Right)
        {
            p.Handled = true;
            var src = p.Source as ListView;
            ContextMenu cm = src.ContextMenu;
            cm.IsOpen = true;
        }
        else if (p.ChangedButton == MouseButton.Left)
        {
            Hierarchy.GoDown(LeftHierarchySelectedItem);
        }
    }

}