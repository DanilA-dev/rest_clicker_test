using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Scripts.Network.Requests
{
    public class JsonRequest<TDto> : BaseWebRequest<TDto>
    {
        #region Constructor

        public JsonRequest(string url, IReadOnlyDictionary<string, string> headers = null) : base(url, headers)
        {
        }

        #endregion

        #region Request

        protected override UnityWebRequest CreateRequest(string url) => UnityWebRequest.Get(url);

        protected override TDto Parse(UnityWebRequest request) => JsonUtility.FromJson<TDto>(request.downloadHandler.text);

        #endregion
    }
}
