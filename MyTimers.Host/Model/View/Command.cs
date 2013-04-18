using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace MyTimers.Model.View
{
    public abstract class Command
        : INotifyPropertyChanged, ICommand

    {
        protected Command()
        {
            _onCanExecuteChangedHelper = new WeakEventSource<EventHandler>(eh => eh.Invoke(this, EventArgs.Empty));
        }

        public void Execute()
        {
            if (CanExecute)
            {
                OnBeforeExecute();
                OnExecute();
            }
        }

        protected abstract void OnExecute();

        private void OnBeforeExecute()
        {
            var handler = BeforeExecute;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler BeforeExecute;

        public bool CanExecute
        {
            get { return _canExecute; }
            protected set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    OnCanExecuteChangedHelper.Invoke();
                    OnPropertyChanged(this, c => c.CanExecute);
                }
            }
        }

        protected WeakEventSource<EventHandler> OnCanExecuteChangedHelper
        {
            get { return _onCanExecuteChangedHelper; }
        }
        private readonly WeakEventSource<EventHandler> _onCanExecuteChangedHelper;

        private bool _canExecute = true;

        #region Implementation of ICommand

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }


        event EventHandler ICommand.CanExecuteChanged
        {
            add { OnCanExecuteChangedHelper.Add(value); }
            remove { OnCanExecuteChangedHelper.Remove(value); }
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<TSource, TKey>(TSource source, Expression<Func<TSource, TKey>> propertySelector)
        {
            var changedHandler = PropertyChanged;
            if (changedHandler != null)
            {
                PropertyChanged(source,
                                new PropertyChangedEventArgs(((MemberExpression)(propertySelector.Body)).Member.Name));
            }
        }

        #endregion
    }
}