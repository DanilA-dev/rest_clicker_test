using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Network
{
    public interface IRequest<TResult>
    {
        public UniTask<TResult> Execute(CancellationTokenSource cToken);
    }
}