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
    public FileManager()
    {
        InitializeComponent();
    }
}