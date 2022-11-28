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

    [ObservableProperty] public BaseTab currentTab;

    [ObservableProperty] public string newFolderName;
    [ObservableProperty] public string newFileName;
    public ICommand LeftHierarchyRenameFolderCommand { get; set; }
    public ICommand RightHierarchyRenameFolderCommand { get; set; }
    public ICommand LeftHierarchyRenameFileCommand { get; set; }
    public ICommand RightHierarchyRenameFileCommand { get; set; }
    public ICommand LeftHierarchyOpenFileCommand { get; set; }
    public ICommand RightHierarchyOpenFileCommand { get; set; }
    
    public ICommand CloseTabCommand { get; set; }

    private bool _isRenameFolderWindowOpen = false;
    private bool _isRenameFileWindowOpen = false;
    private FileManagerState _renameFolderState;
    private FileManagerState _renameFileState;
    
    [ObservableProperty] public ObservableCollection<BaseTab> tabs;

    public MainViewModel()
    {
        Left = new FileManagerState(@"C:\etc\websites\ads");
        Right = new FileManagerState("");
        Left.Other = right;
        Right.Other = left;
        tabs = new ObservableCollection<BaseTab>();
        tabs.Add(new FileManagerTab());
        #region left
        LeftHierarchyRenameFolderCommand = new ActionCommand(() =>
        {
            HierarchyRenameFolderShowWindow(Left);
        });
        LeftHierarchyRenameFileCommand = new ActionCommand(() =>
        {
            HierarchyRenameFileShowWindow(Left);
        });
        LeftHierarchyOpenFileCommand = new ActionCommand((p) =>
        {
            var param = p as MouseButtonEventArgs;
            if (param.ChangedButton == MouseButton.Left)
            {
                OpenNewFileTab(Left);
            }
        });
        #endregion

        Left.RenameFolderCommand = LeftHierarchyRenameFolderCommand;
        Left.RenameFileCommand = LeftHierarchyRenameFileCommand;
        Left.FileMouseClickCommand = LeftHierarchyOpenFileCommand;

        #region right
        RightHierarchyRenameFolderCommand = new ActionCommand(() =>
        {
            HierarchyRenameFolderShowWindow(Right);
        });
        RightHierarchyRenameFileCommand = new ActionCommand(() =>
        {
            HierarchyRenameFileShowWindow(Right);
        });
        RightHierarchyOpenFileCommand = new ActionCommand((p) =>
        {
            var param = p as MouseButtonEventArgs;
            if (param.ChangedButton == MouseButton.Left)
            {
                OpenNewFileTab(Right);
            }
        });
        #endregion

        Right.RenameFolderCommand = RightHierarchyRenameFolderCommand;
        Right.RenameFileCommand = RightHierarchyRenameFileCommand;
        Right.FileMouseClickCommand = RightHierarchyOpenFileCommand;

        CloseTabCommand = new ActionCommand((t) =>
        {
            var tab = t as BaseTab;
            if (tab == null) return;
            CloseTab(tab);
        });
    }
    private void CloseTab(BaseTab tab)
    {
        if (tab != Tabs[0]) Tabs.Remove(tab);
    }
    private void OpenNewFileTab(FileManagerState state)
    {
        var newTab = TabFactory.Instance.CreateTabFromPath(state.SelectedFile.FullName);
        if (newTab == null)
        {
            if (File.Exists(state.SelectedFile.FullName))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = state.SelectedFile.FullName,
                    UseShellExecute = true
                });
                return;
            }
            return;
        }
        newTab.GenerateContent();
        Tabs.Add(newTab);
    }

    [RelayCommand]
    private void FinishRenamingFolder(Window window)
    {
        window.Close();
        _isRenameFolderWindowOpen = false;
        HierarchyRenameFolder(_renameFolderState);
        NewFolderName = "";
    }

    [RelayCommand]
    private void FinishRenamingFile(Window window)
    {
        window.Close();
        _isRenameFileWindowOpen = false;
        HierarchyRenameFile(_renameFileState);
        NewFileName = "";
    }
    
    [RelayCommand]
    private void CloseFolderWindow()
    {
        _isRenameFolderWindowOpen = false;
    }

    [RelayCommand]
    private void CloseFileWindow()
    {
        _isRenameFileWindowOpen = false;
    }
    private void HierarchyRenameFolderShowWindow(FileManagerState state)
    {
        if (_isRenameFolderWindowOpen) return;
        RenameDialog dialog = new RenameDialog();
        dialog.DataContext = this;
        dialog.Show();
        _renameFolderState = state;
        _isRenameFolderWindowOpen = true;
        NewFolderName = state.SelectedItem.Info.Name;
    }

    private void HierarchyRenameFileShowWindow(FileManagerState state)
    {
        if (_isRenameFileWindowOpen) return;
        RenameFileDialog dialog = new RenameFileDialog();
        dialog.DataContext = this;
        dialog.Show();
        _renameFileState = state;
        _isRenameFileWindowOpen = true;
        NewFileName = state.SelectedFile.Name;
    }
    private void HierarchyRenameFolder(FileManagerState state)
    {
        try
        {
            if (NewFolderName.Trim().Length == 0) return;
            Directory.Move(state.SelectedItem.Info.FullName, $@"{state.CurrentHierarchy.Info.FullName}\{NewFolderName}");
            state.CurrentHierarchy.UpdateInfo();
        }
        catch (Exception e)
        {
            MessageBox.Show($"There was an exception while renaming the folder: {e.Message}", "Exception",
                MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }
    }

    private void HierarchyRenameFile(FileManagerState state)
    {
        try
        {
            if (NewFileName.Trim().Length == 0) return;
            File.Move(state.SelectedFile.FullName,
                $@"{state.CurrentHierarchy.Info.FullName}\{NewFileName}");
            state.CurrentHierarchy.UpdateInfo();
        }
        catch (Exception e)
        {
            MessageBox.Show($"There was an exception while moving the file: {e.Message}", "Exception",
                MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }
    }
}