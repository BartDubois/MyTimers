using Timers.Model.View;

namespace MyTimers.Model.View
{
    public abstract class ViewModel
        : NotifyPropertyChanged

    {
        protected ViewModel()
            : this(null)
        {
        }

        protected ViewModel(string displayName)
        {
            _displayName = displayName;
        }


        public string DisplayName
        {
            get
            {
                return _displayName ?? string.Empty;
            }
            protected set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged(this, vm => vm.DisplayName);
                }
            }
        }
        private string _displayName;
    }
}
