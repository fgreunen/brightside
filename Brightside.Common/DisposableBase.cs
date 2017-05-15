using System;

namespace Brightside.Common
{
    public abstract class DisposableBase
        : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                DoDispose();
            }
        }

        protected virtual void DoDispose()
        {

        }
    }
}