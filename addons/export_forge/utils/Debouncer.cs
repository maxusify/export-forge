namespace ExportForge.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Debouncer() : IDisposable
    {
        public int DelayMilliseconds { get; set; }

        private CancellationTokenSource? _cancelTokenSource;

        public async void Debounce(Action action)
        {
            _cancelTokenSource?.Cancel();
            _cancelTokenSource?.Dispose();
            _cancelTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(DelayMilliseconds, _cancelTokenSource.Token);
                action();
            }
            catch (OperationCanceledException)
            {
                // Cancellation is expected. There is nothing to do.
            }
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
