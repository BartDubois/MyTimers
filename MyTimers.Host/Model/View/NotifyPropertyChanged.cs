using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MyTimers.Model.View
{
    public class NotifyPropertyChanged
        : INotifyPropertyChanged
    {
        #region -- INotifyPropertyChanged Members --

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<TSource, TKey>(TSource source, Expression<Func<TSource, TKey>> propertySelector)
        {
            var changedHandler = PropertyChanged;

            if (changedHandler != null)
            {
                changedHandler(source,
                    new PropertyChangedEventArgs(((MemberExpression)(propertySelector.Body)).Member.Name));
            }
        }

        protected void OnPropertiesChanged<TSource>(TSource source)
        {
            var changedHandler = PropertyChanged;

            if (changedHandler != null)
            {
                changedHandler(source, AllPropertiesChangedEventArgs);
            }
        }
        private static readonly PropertyChangedEventArgs AllPropertiesChangedEventArgs = new PropertyChangedEventArgs(String.Empty);

        public void RemoveAllHandlers()
        {
            var changedHandler = PropertyChanged;

            if (changedHandler != null)
            {
                foreach (var handler in changedHandler.GetInvocationList())
                {
                    changedHandler -= (PropertyChangedEventHandler)handler;
                }
            }
        }

        #endregion
    }
}
