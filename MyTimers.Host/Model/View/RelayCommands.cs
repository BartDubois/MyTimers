using System;
using Timers.Model.View;

namespace MyTimers.Model.View
{
    public sealed class RelayCommand
        : Command, IDisposable
    {
        public new bool CanExecute
        {
            get { return base.CanExecute; }
            set { base.CanExecute = value; }
        }

        public RelayCommand(Action action)
        {
            _action = action;
        }

        #region Overrides of Command

        protected override void OnExecute()
        {
            _action();
        }
        private Action _action;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _action = null;
        }

        #endregion
    }
}