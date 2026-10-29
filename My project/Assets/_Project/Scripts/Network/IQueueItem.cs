using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Network
{
    public interface IQueueItem
    {
        public UniTask Execute(CancellationToken lifetime);
        public void Cancel();
    }
}