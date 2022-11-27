using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FileManagerLab.Model;

namespace FileManagerLab.Views;

public partial class FileManager : UserControl
{

    public static readonly DependencyProperty HierarchyProperty = DependencyProperty.Register(
        "Hierarchy", typeof(ApplicationDirectory),
        typeof(FileManager)
    );
    public ApplicationDirectory Hierarchy
    {
        get => (ApplicationDirectory) GetValue(HierarchyProperty);
        set => SetValue(HierarchyProperty, value);
    }
    
    public static readonly DependencyProperty GoUpHierarchyCommandProperty = DependencyProperty.Register(
        "GoUpHierarchyCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand GoUpHierarchyCommand
    {
        get => (ICommand) GetValue(GoUpHierarchyCommandProperty);
        set => SetValue(GoUpHierarchyCommandProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
        "SelectedItem", typeof(ApplicationDirectory),
        typeof(FileManager)
    );
    public ApplicationDirectory SelectedItem
    {
        get => (ApplicationDirectory) GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty HierarchyMouseClickCommandProperty = DependencyProperty.Register(
        "HierarchyMouseClickCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand HierarchyMouseClickCommand
    {
        get => (ICommand) GetValue(HierarchyMouseClickCommandProperty);
        set => SetValue(HierarchyMouseClickCommandProperty, value);
    }

    public static readonly DependencyProperty HierarchySelectItemCommandProperty = DependencyProperty.Register(
        "HierarchySelectItemCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand HierarchySelectItemCommand
    {
        get => (ICommand) GetValue(HierarchySelectItemCommandProperty);
        set => SetValue(HierarchySelectItemCommandProperty, value);
    }

    #region FolderCommands

    public static readonly DependencyProperty CreateFolderCommandProperty = DependencyProperty.Register(
        "CreateFolderCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand CreateFolderCommand
    {
        get => (ICommand) GetValue(CreateFolderCommandProperty);
        set => SetValue(CreateFolderCommandProperty, value);
    }

    public static readonly DependencyProperty DeleteFolderCommandProperty = DependencyProperty.Register(
        "DeleteFolderCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand DeleteFolderCommand
    {
        get => (ICommand) GetValue(DeleteFolderCommandProperty);
        set => SetValue(DeleteFolderCommandProperty, value);
    }

    public static readonly DependencyProperty RenameFolderCommandProperty = DependencyProperty.Register(
        "RenameFolderCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand RenameFolderCommand
    {
        get => (ICommand) GetValue(RenameFolderCommandProperty);
        set => SetValue(RenameFolderCommandProperty, value);
    }

    public static readonly DependencyProperty MoveFolderCommandProperty = DependencyProperty.Register(
        "MoveFolderCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand MoveFolderCommand
    {
        get => (ICommand) GetValue(MoveFolderCommandProperty);
        set => SetValue(MoveFolderCommandProperty, value);
    }
    #endregion

    #region FileCommands

    public static readonly DependencyProperty CopyFileCommandProperty = DependencyProperty.Register(
        "CopyFileCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand CopyFileCommand
    {
        get => (ICommand) GetValue(CopyFileCommandProperty);
        set => SetValue(CopyFileCommandProperty, value);
    }

    public static readonly DependencyProperty MoveFileCommandProperty = DependencyProperty.Register(
        "MoveFileCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand MoveFileCommand
    {
        get => (ICommand) GetValue(MoveFileCommandProperty);
        set => SetValue(MoveFileCommandProperty, value);
    }

    public static readonly DependencyProperty DuplicateFileCommandProperty = DependencyProperty.Register(
        "DuplicateFileCommand", typeof(ICommand),
        typeof(FileManager)
    );
    public ICommand DuplicateFileCommand
    {
        get => (ICommand) GetValue(DuplicateFileCommandProperty);
        set => SetValue(DuplicateFileCommandProperty, value);
    }

    public static readonly DependencyProperty DeleteFileCommandProperty = DependencyProperty.Register(
        "DeleteFileCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand DeleteFileCommand
    {
        get => (ICommand) GetValue(DeleteFileCommandProperty);
        set => SetValue(DeleteFileCommandProperty, value);
    }

    public static readonly DependencyProperty CreateFileCommandProperty = DependencyProperty.Register(
        "CreateFileCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand CreateFileCommand
    {
        get => (ICommand) GetValue(CreateFileCommandProperty);
        set => SetValue(CreateFileCommandProperty, value);
    }

    public static readonly DependencyProperty RenameFileCommandProperty = DependencyProperty.Register(
        "RenameFileCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand RenameFileCommand
    {
        get => (ICommand) GetValue(RenameFileCommandProperty);
        set => SetValue(RenameFileCommandProperty, value);
    }

    public static readonly DependencyProperty FileMouseClickCommandProperty = DependencyProperty.Register(
        "FileMouseClickCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand FileMouseClickCommand
    {
        get => (ICommand) GetValue(FileMouseClickCommandProperty);
        set => SetValue(FileMouseClickCommandProperty, value);
    }

    public static readonly DependencyProperty SelectFileCommandProperty = DependencyProperty.Register(
        "SelectFileCommand", typeof(ICommand),
        typeof(FileManager)
    );

    public ICommand SelectFileCommand
    {
        get => (ICommand) GetValue(SelectFileCommandProperty);
        set => SetValue(SelectFileCommandProperty, value);
    }
    #endregion
    
    public FileManager()
    {
        InitializeComponent();
    }
}