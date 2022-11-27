using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Xaml.Behaviors.Core;

namespace FileManagerLab.Model;

public partial class FileManagerState : ObservableObject
{
    public FileManagerState Other { get; set; }
    [ObservableProperty]
    public ApplicationDirectory currentHierarchy;
    [ObservableProperty]
    public ApplicationDirectory selectedItem;

    public ICommand SelectItemCommand { get; set; }
    public ICommand SelectFileCommand { get; set; }
    public ICommand MouseClickCommand { get; set; }
    public ICommand FileMouseClickCommand { get; set; }
    public ICommand CreateFolderCommand { get; set; }
    public ICommand DeleteFolderCommand { get; set; }
    public ICommand RenameFolderCommand { get; set; }
    public ICommand MoveFolderCommand { get; set; }
    public ICommand MoveFileCommand { get; set; }
    public ICommand DuplicateFileCommand { get; set; }
    public ICommand DeleteFileCommand { get; set; }
    public ICommand CopyFileCommand { get; set; }
    public ICommand RenameFileCommand { get; set; }

    public FileManagerState(string path)
    {
        currentHierarchy = new ApplicationDirectory(path);
    }

    public void SetOther(FileManagerState other)
    {
        Other = other;
    }

    private void InitializeCommands()
    {
        SelectItemCommand = new ActionCommand((parameter) =>
        {
            SelectionChangedEventArgs p = parameter as SelectionChangedEventArgs;
            if (p.AddedItems.Count == 0) return;
            var addedItem = p.AddedItems[0] as ApplicationDirectory;
            selectedItem = addedItem;
        });
        MouseClickCommand = new ActionCommand((parameter) =>
        {
            var p = parameter as MouseButtonEventArgs;
            if (p == null) return;
            if (p.ChangedButton == MouseButton.Left)
            {
                CurrentHierarchy.GoDown(SelectedItem);
            }
        });
        CreateFolderCommand = new ActionCommand(() =>
        {
            if (CurrentHierarchy.Info.FullName.Trim().Length == 0 || CurrentHierarchy.Info.Parent == null) return;
            var startPath = @$"{CurrentHierarchy.Info.FullName}\New folder";
            if (!Directory.Exists(startPath))
            {
                Directory.CreateDirectory(startPath);
                CurrentHierarchy.UpdateInfo();
            }
            else
            {
                int i = 1;
                while (true)
                {
                    string newPath = @$"{CurrentHierarchy.Info.FullName}\New folder ({i})";
                    if (Directory.Exists(newPath))
                    {
                        i++;
                        continue;
                    }

                    Directory.CreateDirectory(newPath);
                    CurrentHierarchy.UpdateInfo();
                    break;
                }
            }
        });
        DeleteFolderCommand = new ActionCommand(() =>
        {
            Directory.Delete(SelectedItem.Info.FullName);
            CurrentHierarchy.UpdateInfo();
        });
        MoveFolderCommand = new ActionCommand(() =>
        {
            if (Other.CurrentHierarchy.Info != null)
            {
                try
                {
                    Directory.Move(SelectedItem.Info.FullName,
                        $@"{Other.CurrentHierarchy.Info.FullName}\{SelectedItem.Info.Name}");
                    CurrentHierarchy.UpdateInfo();
                    Other.CurrentHierarchy.UpdateInfo();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was an exception while moving the file: {e.Message}", "Exception",
                        MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }
        });
    }
}