using System;
using MyTimers.Model;
using MyTimers.Model.View;

namespace Timers.Model.View
{
    public class TimerViewModel
        : ViewModel
    {

        public TimerViewModel(TimerInfo info)
        {
            _info = info;
            _start = new RelayCommand(OnStartCommand) {CanExecute = true};
        }
        private readonly TimerInfo _info;

        public string Name
        {
            get { return _info.Name; }
            set {
                _info.Name = value;

                OnPropertyChanged(this, vm => vm.Name);
            }
        }

        public TimeSpan Value
        {
            get { return _value; }
            private set { _value = value;
            OnPropertyChanged(this, vm => vm.Value);}
        }
        private TimeSpan _value;

        public TimeSpan Sum
        {
            get { return _sum; }
            private set
            {
                _sum = value;
                OnPropertyChanged(this, vm => vm.Sum);
            }
        }
        private TimeSpan _sum;


        public Command Update { get { return _update ?? (_update = new RelayCommand(OnUpdateCommand)); } }
        private Command _update;

        private void OnUpdateCommand()
        {
        }

        public Command Start { get { return _start; } }
        private readonly Command _start;

        private void OnStartCommand()
        {
            if (_isStarted)
            {
                Value = DateTime.Now - _started;
                Sum += Value;
            }
            else
            {
                Value = new TimeSpan();
                _started = DateTime.Now;
            }
            _isStarted = !_isStarted;
        }

        private bool _isStarted;
        private DateTime _started;

        public void Refresh()
        {
            if (_isStarted)
            {
                Value = DateTime.Now - _started;
            }
        }
    }
}