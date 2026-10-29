using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.Breeds.Dto;
using _Project.Scripts.Network;
using _Project.Scripts.Network.Requests;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Breeds
{
    public class BreedsProvider
    {
        #region Fields

        private readonly BreedsConfig _config;
        private readonly IRequestQueue _queue;

        #endregion

        #region Constructor

        public BreedsProvider(BreedsConfig config, IRequestQueue queue)
        {
            _config = config;
            _queue = queue;
        }

        #endregion

        #region Public

        public async UniTask<IReadOnlyList<Breed>> GetBreedsAsync(CancellationToken token)
        {
            var dto = await _queue.Enqueue(new JsonRequest<BreedListResponseDto>(_config.BreedsUrl), token);

            if (dto?.data == null)
                throw new InvalidOperationException("Breeds response has no data");

            return dto.data
                .Take(_config.BreedsCount)
                .Select(Map)
                .ToList();
        }

        public async UniTask<Breed> GetBreedAsync(string id, CancellationToken token)
        {
            var dto = await _queue.Enqueue(new JsonRequest<BreedResponseDto>($"{_config.BreedsUrl}/{id}"), token);

            if (dto?.data == null)
                throw new InvalidOperationException($"Breed {id} response has no data");

            return Map(dto.data);
        }

        #endregion

        #region Private

        private static Breed Map(BreedDto dto) => new(dto.id, dto.attributes?.name, dto.attributes?.description);

        #endregion
    }
}
