using System;
using System.Windows.Input;

namespace FileManagerLab.MVVM;

public class ApplicationCommand : ICommand
{
    private Action<object> _action;

    public ApplicationCommand(Action<object> action)
    {
        _action = action;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}