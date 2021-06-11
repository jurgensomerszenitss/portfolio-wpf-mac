using System;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Mdm.App.Infrastructure
{
    public static class CommandExtensions
    {
        public static async Task ExecuteAsync(this ICommand command)
        {
            if(command is RelayCommand)
            {
                await ((RelayCommand)command).Execute();
            }
        }

        public static async Task ExecuteAsync<T>(this ICommand command, T parameter)
        {
            if (command is RelayCommand<T>)
            {
                await ((RelayCommand<T>)command).Execute(parameter);
            }
        }
    }


    public class RelayCommand : ICommand
    {

        public RelayCommand(Func<Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            CommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);

        }

        ~RelayCommand()
        {
            RemoveCommand();
        }



        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        async void ICommand.Execute(object parameter)
        {
            await Execute();
            CommandManager.RefreshCommandStates();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }


        public bool CanExecute()
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        private readonly Action RaiseCanExecuteChangedAction;


        public event EventHandler CanExecuteChanged;

        public async Task Execute()
        {
            await Task.Factory.StartNew(() => _execute());
        }

        public void RemoveCommand()
        {
            CommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);
        }
    }

    public class RelayCommand<T> : ICommand
    {

        public RelayCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            CommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }

        ~RelayCommand()
        {
            RemoveCommand();
        }

        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly Action RaiseCanExecuteChangedAction;
        public event EventHandler CanExecuteChanged;

        async void ICommand.Execute(object parameter)
        {
            await Execute((T)parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }


        public bool CanExecute(T parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public async Task Execute(T parameter)
        {
            await _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        } 

        public void RemoveCommand()
        {
            CommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);
        }
    }
}
