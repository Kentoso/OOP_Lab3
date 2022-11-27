using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FileManagerLab.Model;
using FileManagerLab.Model.Tabs;
using FileManagerLab.MVVM;
using FileManagerLab.Views;
using Microsoft.Xaml.Behaviors.Core;

namespace FileManagerLab.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] public FileManagerState left;
    [ObservableProperty] public FileManagerState right;
    
    [ObservableProperty] public ApplicationDirectory leftHierarchy;
    [ObservableProperty] public ApplicationDirectory rightHierarchy;

    [ObservableProperty] public ApplicationDirectory leftHierarchySelectedItem;
    [ObservableProperty] public ApplicationDirectory rightHierarchySelectedItem;

    [ObservableProperty] public ApplicationDirectory leftHierarchySelectedFile;
    [ObservableProperty] public ApplicationDirectory rightHierarchySelectedFile;

    [ObservableProperty] public BaseTab currentTab;

    [ObservableProperty] public string newFolderName;
    public ICommand LeftHierarchySelectItemCommand { get; set; }
    public ICommand LeftHierarchySelectFileCommand { get; set; }
    public ICommand LeftHierarchyMouseClickCommand { get; set; }
    public ICommand LeftHierarchyFileMouseClickCommand { get; set; }
    public ICommand LeftHierarchyCreateFolderCommand { get; set; }
    public ICommand LeftHierarchyDeleteFolderCommand { get; set; }
    public ICommand LeftHierarchyRenameFolderCommand { get; set; }
    public ICommand LeftHierarchyMoveFolderCommand { get; set; }
    public ICommand LeftHierarchyMoveFileCommand { get; set; }
    public ICommand LeftHierarchyDuplicateFileCommand { get; set; }
    public ICommand LeftHierarchyDeleteFileCommand { get; set; }
    public ICommand LeftHierarchyCopyFileCommand { get; set; }
    public ICommand LeftHierarchyRenameFileCommand { get; set; }
    
    public ICommand RightHierarchySelectItemCommand { get; set; }
    public ICommand RightHierarchySelectFileCommand { get; set; }
    public ICommand RightHierarchyMouseClickCommand { get; set; }
    public ICommand RightHierarchyFileMouseClickCommand { get; set; }
    public ICommand RightHierarchyCreateFolderCommand { get; set; }
    public ICommand RightHierarchyDeleteFolderCommand { get; set; }
    public ICommand RightHierarchyRenameFolderCommand { get; set; }
    public ICommand RightHierarchyMoveFolderCommand { get; set; }
    public ICommand RightHierarchyMoveFileCommand { get; set; }
    public ICommand RightHierarchyDuplicateFileCommand { get; set; }
    public ICommand RightHierarchyDeleteFileCommand { get; set; }
    public ICommand RightHierarchyCopyFileCommand { get; set; }
    public ICommand RightHierarchyRenameFileCommand { get; set; }

    private bool _isRenameFolderWindowOpen = false;
    private ApplicationDirectory _renameFolderHierarchy;
    private ApplicationDirectory _renameFolderSelectedItem;
    [ObservableProperty] public ObservableCollection<BaseTab> tabs;

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

        #region left
        
        LeftHierarchySelectItemCommand = new ApplicationCommand((p) =>
        {
            HierarchySelectItem(p, ref leftHierarchySelectedItem);
        });
        LeftHierarchyMouseClickCommand = new ActionCommand((p) =>
        {
            HierarchyMouseClick(p, LeftHierarchy, LeftHierarchySelectedItem);
        });
        LeftHierarchyCreateFolderCommand = new ActionCommand(() => { HierarchyCreateFolder(LeftHierarchy); });
        LeftHierarchyDeleteFolderCommand = new ActionCommand(() =>
        {
            HierarchyDeleteFolder(LeftHierarchy, LeftHierarchySelectedItem);
        });
        LeftHierarchyRenameFolderCommand = new ActionCommand(() =>
        {
            HierarchyRenameFolderShowWindow(LeftHierarchy, LeftHierarchySelectedItem);
        });
        LeftHierarchyMoveFolderCommand = new ActionCommand(() =>
        {
            if (RightHierarchy.Info != null)
            {
                try
                {
                    Directory.Move(LeftHierarchySelectedItem.Info.FullName,
                        $@"{RightHierarchy.Info.FullName}\{LeftHierarchySelectedItem.Info.Name}");
                    LeftHierarchy.UpdateInfo();
                    RightHierarchy.UpdateInfo();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was an exception while moving the file: {e.Message}", "Exception",
                        MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }
        });

        #endregion

        #region right
        RightHierarchySelectItemCommand = new ApplicationCommand((p) =>
        {
            HierarchySelectItem(p, ref rightHierarchySelectedItem);
        });
        RightHierarchyMouseClickCommand = new ActionCommand((p) =>
        {
            HierarchyMouseClick(p, RightHierarchy, RightHierarchySelectedItem);
        });
        RightHierarchyCreateFolderCommand = new ActionCommand(() => { HierarchyCreateFolder(RightHierarchy); });
        RightHierarchyDeleteFolderCommand = new ActionCommand(() =>
        {
            HierarchyDeleteFolder(RightHierarchy, RightHierarchySelectedItem);
        });
        RightHierarchyRenameFolderCommand = new ActionCommand(() =>
        {
            HierarchyRenameFolderShowWindow(RightHierarchy, RightHierarchySelectedItem);
        });
        RightHierarchyMoveFolderCommand = new ActionCommand(() =>
        {
            if (LeftHierarchy.Info != null)
            {
                try
                {
                    Directory.Move(RightHierarchySelectedItem.Info.FullName,
                        $@"{LeftHierarchy.Info.FullName}\{RightHierarchySelectedItem.Info.Name}");
                    LeftHierarchy.UpdateInfo();
                    RightHierarchy.UpdateInfo();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was an exception while moving the file: {e.Message}", "Exception",
                        MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }
        });
        #endregion
        
    }

    private void InitializeLeftHierarchyCommands()
    {
        
    }

    private void InitializeRightHierarchyCommands()
    {

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

    [RelayCommand]
    private void FinishRenamingFolder(Window window)
    {
        window.Close();
        _isRenameFolderWindowOpen = false;
        HierarchyRenameFolder(_renameFolderHierarchy, _renameFolderSelectedItem);
        NewFolderName = "";
    }

    private void HierarchyCreateFolder(ApplicationDirectory hierarchy)
    {
        if (hierarchy.Info.FullName.Trim().Length == 0 || hierarchy.Info.Parent == null) return;
        var startPath = @$"{hierarchy.Info.FullName}\New folder";
        if (!Directory.Exists(startPath))
        {
            Directory.CreateDirectory(startPath);
            hierarchy.UpdateInfo();
        }
        else
        {
            int i = 1;
            while (true)
            {
                string newPath = @$"{hierarchy.Info.FullName}\New folder ({i})";
                if (Directory.Exists(newPath))
                {
                    i++;
                    continue;
                }

                Directory.CreateDirectory(newPath);
                hierarchy.UpdateInfo();
                break;
            }
        }
    }

    private void HierarchyDeleteFolder(ApplicationDirectory hierarchy, ApplicationDirectory selectedItem)
    {
        Directory.Delete(selectedItem.Info.FullName);
        hierarchy.UpdateInfo();
    }

    private void HierarchyRenameFolderShowWindow(ApplicationDirectory hierarchy, ApplicationDirectory selectedItem)
    {
        if (_isRenameFolderWindowOpen) return;
        RenameDialog dialog = new RenameDialog();
        dialog.DataContext = this;
        dialog.Show();
        _renameFolderHierarchy = hierarchy;
        _renameFolderSelectedItem = selectedItem;
        _isRenameFolderWindowOpen = true;
    }

    private void HierarchyRenameFolder(ApplicationDirectory hierarchy, ApplicationDirectory selectedItem)
    {
        try
        {
            Directory.Move(selectedItem.Info.FullName, $@"{hierarchy.Info.FullName}\{NewFolderName}");
            hierarchy.UpdateInfo();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }

    private void HierarchySelectItem(object parameter, ref ApplicationDirectory selectedItem)
    {
        SelectionChangedEventArgs p = parameter as SelectionChangedEventArgs;
        if (p.AddedItems.Count == 0) return;
        var addedItem = p.AddedItems[0] as ApplicationDirectory;
        selectedItem = addedItem;
        Debug.Write(selectedItem.Info.Name);
    }

    private void HierarchyMouseClick(object parameter, ApplicationDirectory hierarchy,
        ApplicationDirectory selectedItem)
    {
        var p = parameter as MouseButtonEventArgs;
        if (p == null) return;
        if (p.ChangedButton == MouseButton.Left)
        {
            hierarchy.GoDown(selectedItem);
        }
    }
}