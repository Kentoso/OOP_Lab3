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
    [ObservableProperty] public ApplicationDirectory leftHierarchy;
    [ObservableProperty] public ApplicationDirectory rightHierarchy;

    [ObservableProperty]
    public ApplicationDirectory leftHierarchySelectedItem;
    [ObservableProperty]
    public ApplicationDirectory rightHierarchySelectedItem;
    
    [ObservableProperty]
    public BaseTab currentTab;
    public ICommand LeftHierarchySelectItemCommand { get; set; } 
    public ICommand LeftHierarchyMouseClickCommand { get; set; }
    public ICommand LeftHierarchyCreateFolderCommand { get; set; }
    public ICommand LeftHierarchyDeleteFolderCommand { get; set; }

    public ICommand RightHierarchySelectItemCommand { get; set; }
    public ICommand RightHierarchyMouseClickCommand { get; set; }
    public ICommand RightHierarchyCreateFolderCommand { get; set; }

    
    [ObservableProperty]
    public ObservableCollection<BaseTab> tabs;
    public MainViewModel()
    {
        leftHierarchy = new ApplicationDirectory(@"C:\etc\websites\ads");
        rightHierarchy = new ApplicationDirectory(@"");
        tabs = new ObservableCollection<BaseTab>();
        tabs.Add(new FileManagerTab());
        tabs.Add(new ImageFileTab(@"C:\Users\Demian\Downloads\AD2.png"));
        tabs.Add(new TextFileTab(@"C:\Users\Demian\Downloads\Yevgeniy Vicktorovich Prigozhin3.pdf"));
        tabs.Add(new ByteFileTab(@"C:\Users\Demian\Downloads\Yevgeniy Vicktorovich Prigozhin3.pdf"));
        tabs[1].GenerateContent();
        tabs[2].GenerateContent();
        tabs[3].GenerateContent();
        LeftHierarchySelectItemCommand = new ApplicationCommand((p) =>
        {
            HierarchySelectItem(p, ref leftHierarchySelectedItem);
        });
        LeftHierarchyMouseClickCommand = new ActionCommand((p) =>
        {
            HierarchyMouseClick(p, LeftHierarchy, LeftHierarchySelectedItem);
        });
        LeftHierarchyCreateFolderCommand = new ActionCommand(() =>
        {
            HierarchyCreateFolder(LeftHierarchy);
        });
        LeftHierarchyDeleteFolderCommand = new ActionCommand(() =>
        {
            Directory.Delete(LeftHierarchySelectedItem.Info.FullName);
        });
        
        RightHierarchySelectItemCommand = new ApplicationCommand((p) =>
        {
            HierarchySelectItem(p, ref rightHierarchySelectedItem);
        });
        RightHierarchyMouseClickCommand = new ActionCommand((p) =>
        {
            HierarchyMouseClick(p, RightHierarchy, RightHierarchySelectedItem);
        });
        RightHierarchyCreateFolderCommand = new ActionCommand(() =>
        {
            HierarchyCreateFolder(RightHierarchy);
        });
    }

    [RelayCommand]
    private void GoUpLeftHierarchy()
    {
        LeftHierarchy.GoUp();
    }

    [RelayCommand]
    private void GoUpRightHierarchy()
    {
        RightHierarchy.GoUp();
    }
    
    private void HierarchyCreateFolder(ApplicationDirectory hierarchy)
    {
        if (hierarchy.Info.FullName.Trim().Length == 0 || hierarchy.Info.Parent == null) return;
        Directory.CreateDirectory(@$"{hierarchy.Info.FullName}\New folder");
        hierarchy.UpdateInfo();
    }
    private void HierarchySelectItem(object parameter, ref ApplicationDirectory selectedItem)
    {
        SelectionChangedEventArgs p = parameter as SelectionChangedEventArgs;
        if (p.AddedItems.Count == 0) return;
        var addedItem = p.AddedItems[0] as ApplicationDirectory;
        selectedItem = addedItem;
        Debug.Write(selectedItem.Info.Name);
    }

    private void HierarchyMouseClick(object parameter, ApplicationDirectory hierarchy, ApplicationDirectory selectedItem)
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
            hierarchy.GoDown(selectedItem);
        }
    }
}