using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Network
{
    public class QueueItem<TResult> : IQueueItem
    {
        #region Fields

        private readonly IRequest<TResult> _request;
        private readonly CancellationToken _token;
        private readonly UniTaskCompletionSource<TResult> _source = new();

        public CancellationTokenRegistration QueueRegistration;
        
        #endregion

        #region Properties
        public UniTask<TResult> Task => _source.Task;

        #endregion

        #region Constructor

        public QueueItem(IRequest<TResult> request, CancellationToken token)
        {
            _request = request;
            _token = token;
        }

        #endregion

        #region Public

        public async UniTask Execute(CancellationToken lifetime)
        {
            QueueRegistration.Dispose();

            using var linked = CancellationTokenSource.CreateLinkedTokenSource(_token, lifetime);
            try
            {
                var result = await _request.Execute(linked.Token);
                _source.TrySetResult(result);
            }
            catch (OperationCanceledException)
            {
                Cancel();
            }
            catch (Exception e)
            {
                _source.TrySetException(e);
            }
        }

        public void Cancel()
        {
            QueueRegistration.Dispose();
            _source.TrySetCanceled(_token);
        }

        #endregion
    }
}