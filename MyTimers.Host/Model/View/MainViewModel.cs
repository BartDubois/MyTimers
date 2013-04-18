using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Timers.Model.View;

namespace MyTimers.Model.View
{
    public class MainViewModel
        : ViewModel
    {

        public MainViewModel()
            : base("Timers")
        {
            _synchronization = SynchronizationContext.Current;
            _timer = new Timer(OnTimer, null, new TimeSpan(0, 0, 0, 1), new TimeSpan(0, 0, 0, 1));
        }
        private readonly SynchronizationContext _synchronization;
        private readonly Timer _timer;

        private void OnTimer(object state)
        {
            if (SynchronizationContext.Current != _synchronization)
            {
                _synchronization.Post(
                    delegate
                        {
                            OnTimer(state);
                        }, null);
            }

            foreach (var timer in _timers)
            {
                timer.Refresh();
            }
        }


        public Command Add { get { return _add ?? (_add = new RelayCommand(OnAddCommand)); } }
        private RelayCommand _add;

        private void OnAddCommand()
        {
            var info = new TimerInfo
                           {
                               Name = String.Format("Timer {0}", _timers.Count)
                           };

            _timers.Add(new TimerViewModel(info));
        }

        public IEnumerable<TimerViewModel> Timers { get { return _timers; } }
        private readonly ICollection<TimerViewModel> _timers = new ObservableCollection<TimerViewModel>();
    }
}
