using System;

namespace MyTimers
{
    public static class EventHandlerExtensions
    {
        public static void Rise(this EventHandler handler, object sender)
        {
            if (handler != null)
            {
                handler(sender, EventArgs.Empty);
            }
        }

        public static void Rise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
