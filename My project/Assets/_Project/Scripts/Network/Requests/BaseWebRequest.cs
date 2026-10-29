using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace _Project.Scripts.Network.Requests
{
    public abstract class BaseWebRequest<TResult> : IRequest<TResult>
    {
        #region Fields

        private readonly string _url;
        private readonly IReadOnlyDictionary<string, string> _headers;

        #endregion

        #region Constructor

        protected BaseWebRequest(string url, IReadOnlyDictionary<string, string> headers = null)
        {
            _url = url;
            _headers = headers;
        }

        #endregion

        #region Public

        public async UniTask<TResult> Execute(CancellationToken token)
        {
            using var request = CreateRequest(_url);

            if (_headers != null)
            {
                foreach (var header in _headers)
                    request.SetRequestHeader(header.Key, header.Value);
            }
            await request.SendWebRequest().WithCancellation(token);
            return Parse(request);
        }

        #endregion

        #region Abstract

        protected abstract UnityWebRequest CreateRequest(string url);
        protected abstract TResult Parse(UnityWebRequest request);

        #endregion
    }
}
