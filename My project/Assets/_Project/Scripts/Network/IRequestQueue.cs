using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Network
{
    public interface IRequestQueue
    {
        public UniTask<TResult> Enqueue<TResult>(IRequest<TResult> request, CancellationToken token = default);
    }
}
