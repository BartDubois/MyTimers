using System;

namespace MyTimers.Model
{
    public class TimerEvent
    {
        public long Id { get; set; }

        public bool IsStart { get; set; }

        public DateTime Occured { get; set; }

        public TimeSpan Duration { get; set; }

        public TimerInfo Timer { get; set; }

    }
}
