using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTimers
{
    public class WeakEventSource<TEvent> where TEvent : class
    {
        public WeakEventSource(Action<TEvent> rise)
        {
            _rise = rise;
        }

        public WeakEventSource()
        {
        }

        public void Add(TEvent handler)
        {
            if (_handlers == null)
            {
                _handlers = new List<WeakReference>();
            }

            _handlers.Add(new WeakReference(handler));
        }

        private IList<WeakReference> _handlers;

        public void Remove(TEvent handler)
        {
            if (_handlers != null)
            {
                _handlers = _handlers
                    .Where(wr => wr.Target != null && (wr.Target as TEvent) != handler)
                    .ToList();
            }
        }

        public void Invoke()
        {
            if (_rise == null)
            {
                throw new InvalidOperationException(
                    "Use constructor with parameter if you wnat to use rise events with this function.");
            }
            Invoke(_rise);
        }

        private readonly Action<TEvent> _rise;

        public void Invoke(Action<TEvent> rise)
        {
            if (_handlers != null)
            {
                _handlers = _handlers
                    .Where(wr => wr.Target != null)
                    .ToList();

                foreach (var hander in _handlers)
                {
                    rise.Invoke((hander.Target as TEvent));
                }
            }
        }
    }
}