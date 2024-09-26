using System.ComponentModel;
using System.Windows.Input;

public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly List<INotifyPropertyChanged> _observedObjects = new List<INotifyPropertyChanged>();

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ObservePropertyChanges(INotifyPropertyChanged obj)
        {
            if (!_observedObjects.Contains(obj))
            {
                obj.PropertyChanged += OnObservedPropertyChanged;
                _observedObjects.Add(obj);
            }
        }

        private void OnObservedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        public void Dispose()
        {
            foreach (var obj in _observedObjects)
            {
                obj.PropertyChanged -= OnObservedPropertyChanged;
            }
            _observedObjects.Clear();
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private readonly List<INotifyPropertyChanged> _observedObjects = new List<INotifyPropertyChanged>();

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ObservePropertyChanges(INotifyPropertyChanged obj)
        {
            if (!_observedObjects.Contains(obj))
            {
                obj.PropertyChanged += OnObservedPropertyChanged;
                _observedObjects.Add(obj);
            }
        }

        private void OnObservedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        public void Dispose()
        {
            foreach (var obj in _observedObjects)
            {
                obj.PropertyChanged -= OnObservedPropertyChanged;
            }
            _observedObjects.Clear();
        }
    }
