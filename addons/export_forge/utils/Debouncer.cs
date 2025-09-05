namespace ExportForge.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Debouncer() : IDisposable
    {
        public int DelayMilliseconds { get; set; }

        private CancellationTokenSource? _cancelTokenSource;

        public void Debounce(Action action)
        {
            _cancelTokenSource?.Cancel();
            _cancelTokenSource = new CancellationTokenSource();

            Task
                .Delay(DelayMilliseconds, _cancelTokenSource.Token)
                .ContinueWith(t => {
                    if (!t.IsCanceled)
                    {
                        action();
                    }
                }, TaskScheduler.Default);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cancelTokenSource?.Dispose();
                _cancelTokenSource = null;
            }
        }
    }
}
