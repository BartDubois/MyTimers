using System;
using System.Diagnostics;

namespace MyTimers
{
    public abstract class Disposable : IDisposable
    {
        protected Disposable()
        {
            Trace.WriteLine(String.Format("[{0}:{1}] Creating", base.GetHashCode(), GetType().Name), "Debug");
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Trace.WriteLine(String.Format("[{0}:{1}] Disposing", base.GetHashCode(), GetType().Name), "Debug");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Disposable()
        {
            Trace.WriteLine(String.Format("[{0}:{1}] Finalizing", base.GetHashCode(), GetType().Name), "Debug");
            Dispose(false);
        }

        public bool IsDisposed
        {
            get { return _isDisposed; }
            private set
            {
                if (!_isDisposed && _isDisposed != value)
                {
                    IsDisposedChanged(_isDisposed = value);
                }
            }
        }
        private bool _isDisposed;

        protected virtual void IsDisposedChanged(bool disposed)
        {
        }


        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                OnDispose(disposing);
            }
        }

        protected virtual void OnDispose(bool disposing)
        {
            IsDisposed = true;
        }

        #endregion

        protected void AssertIsDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
