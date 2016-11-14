using System;
using System.Windows.Input;

namespace Utilities.MVVM
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

        public void RaiseCanExecuteChanged()
        {
            var dlg = CanExecuteChanged;
            if (dlg != null)
            {
                dlg(this, EventArgs.Empty);
            }
        }
    }
}
