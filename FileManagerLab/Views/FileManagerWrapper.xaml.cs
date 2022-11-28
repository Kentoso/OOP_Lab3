using System.Windows;
using System.Windows.Controls;
using FileManagerLab.Model;

namespace FileManagerLab.Views;

public partial class FileManagerWrapper : UserControl
{
    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
        "State", typeof(FileManagerState),
        typeof(FileManagerWrapper)
    );

    public FileManagerState State
    {
        get => (FileManagerState) GetValue(StateProperty);
        set => SetValue(StateProperty, value);
    }

    public FileManagerWrapper()
    {
        InitializeComponent();
    }
}