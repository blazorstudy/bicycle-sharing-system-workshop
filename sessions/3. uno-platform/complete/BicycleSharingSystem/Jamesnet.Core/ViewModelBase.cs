using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Windows.Input;

namespace Jamesnet.Core;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        OnPropertyChangedExtended(propertyName);
    }

    protected bool SetProperty<T>(ref T storage, T value, Action? callback = null, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        callback?.Invoke();
        return true;
    }

    private void OnPropertyChangedExtended(string propertyName)
    {
        var property = GetType().GetProperty(propertyName);
        var attributes = property?.GetCustomAttributes<CanExecuteAttribute>(inherit: true);

        if (attributes != null)
        {
            foreach (var attribute in attributes)
            {
                var commandName = attribute.CommandName;
                var commandProperty = GetType().GetProperty(commandName);
                var command = commandProperty?.GetValue(this) as ICommand;

                if (command is RelayCommand relayCommand)
                {
                    relayCommand.RaiseCanExecuteChanged();
                }
                else if (command is RelayCommand<object> relayCommandGeneric)
                {
                    relayCommandGeneric.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
