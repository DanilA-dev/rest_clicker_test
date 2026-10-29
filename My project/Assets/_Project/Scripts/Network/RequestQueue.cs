using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Network
{
    public class RequestQueue : IRequestQueue, IDisposable
    {
        #region Fields

        private readonly LinkedList<IQueueItem> _queue = new();
        private readonly CancellationTokenSource _lifetime = new();

        private bool _isProcessing;

        #endregion

        #region Public

        public UniTask<TResult> Enqueue<TResult>(IRequest<TResult> request, CancellationToken token = default)
        {
            if (token.IsCancellationRequested || _lifetime.IsCancellationRequested)
                return UniTask.FromCanceled<TResult>(token);

            var item = new QueueItem<TResult>(request, token);
            var node = _queue.AddLast(item);

            item.QueueRegistration = token.Register(() =>
            {
                if (node.List == null)
                    return;

                _queue.Remove(node);
                item.Cancel();
            });

            ProcessAsync().Forget();
            return item.Task;
        }

        #endregion

        #region Private

        private async UniTaskVoid ProcessAsync()
        {
            if (_isProcessing)
                return;

            _isProcessing = true;

            while (_queue.Count > 0 && !_lifetime.IsCancellationRequested)
            {
                var item = _queue.First.Value;
                _queue.RemoveFirst();
                await item.Execute(_lifetime.Token);
            }

            _isProcessing = false;
        }

        #endregion

        #region Disposable

        public void Dispose()
        {
            _lifetime.Cancel();
            _lifetime.Dispose();

            foreach (var item in _queue)
                item.Cancel();

            _queue.Clear();
        }

        #endregion
    }
}
