using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Scripts.Network.Requests
{
    public class TextureRequest : BaseWebRequest<Texture2D>
    {
        public TextureRequest(string url, IReadOnlyDictionary<string, string> headers = null) : base(url, headers)
        {
        }

        protected override UnityWebRequest CreateRequest(string url) => UnityWebRequestTexture.GetTexture(url);

        protected override Texture2D Parse(UnityWebRequest request) => DownloadHandlerTexture.GetContent(request);
    }
}
