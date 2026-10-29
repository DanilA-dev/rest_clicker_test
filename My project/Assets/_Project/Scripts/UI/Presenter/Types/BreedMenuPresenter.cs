using System;
using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Breeds;
using _Project.Scripts.UI.Model;
using _Project.Scripts.UI.View.Items;
using _Project.Scripts.UI.View.Types;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI.Presenter.Types
{
    public class BreedMenuPresenter : BaseMenuPresenter<BreedMenuView>
    {
        #region Fields

        private readonly BreedsProvider _provider;
        private readonly BreedItemView.Pool _itemPool;
        private readonly BreedInfoView _breedInfoView;

        private readonly List<BreedItemView> _items = new();
        private readonly CompositeDisposable _disposables = new();

        private CancellationToken _menuToken;
        private CancellationTokenSource _detailsCts;
        private BreedItemView _loadingItem;

        #endregion

        #region Properties

        public override MenuId Id => MenuId.Breed;

        #endregion

        #region Constructor

        public BreedMenuPresenter(BreedMenuView view, BreedsProvider provider, BreedItemView.Pool itemPool,
            BreedInfoView breedInfoView) : base(view)
        {
            _provider = provider;
            _itemPool = itemPool;
            _breedInfoView = breedInfoView;
        }

        #endregion

        #region Menu

        protected override void OnActivate(CancellationToken token)
        {
            _menuToken = token;

            _breedInfoView.OnOkClick
                .Subscribe(_ => _breedInfoView.Hide())
                .AddTo(_disposables);

            LoadBreedsAsync(token).Forget();
        }

        protected override void OnDeactivate()
        {
            CancelDetails();
            _disposables.Clear();
            _breedInfoView.Hide();
            ClearItems();
        }

        #endregion

        #region Breeds

        private async UniTaskVoid LoadBreedsAsync(CancellationToken token)
        {
            View.OnLoad();

            try
            {
                var breeds = await _provider.GetBreedsAsync(token);
                if (token.IsCancellationRequested)
                    return;

                ShowBreeds(breeds);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Breeds request failed: {e.Message}");
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    View.OnReady();
            }
        }

        private void ShowBreeds(IReadOnlyList<Breed> breeds)
        {
            for (var i = 0; i < breeds.Count; i++)
            {
                var breed = breeds[i];
                var item = _itemPool.Spawn($"{i + 1} - {breed.Name}");
                _items.Add(item);

                item.OnClick
                    .Subscribe(_ => LoadDetailsAsync(breed.Id, item).Forget())
                    .AddTo(_disposables);
            }
        }

        private void ClearItems()
        {
            foreach (var item in _items)
                _itemPool.Despawn(item);

            _items.Clear();
        }

        #endregion

        #region Details

        private async UniTaskVoid LoadDetailsAsync(string id, BreedItemView item)
        {
            CancelDetails();

            _detailsCts = CancellationTokenSource.CreateLinkedTokenSource(_menuToken);
            var token = _detailsCts.Token;

            _loadingItem = item;
            item.SetLoading(true);

            try
            {
                var breed = await _provider.GetBreedAsync(id, token);
                if (token.IsCancellationRequested)
                    return;

                _breedInfoView.Init(breed.Name, breed.Description);
                _breedInfoView.Show();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Breed {id} request failed: {e.Message}");
            }
            finally
            {
                if (!token.IsCancellationRequested)
                {
                    item.SetLoading(false);
                    _loadingItem = null;
                }
            }
        }

        private void CancelDetails()
        {
            _detailsCts?.Cancel();
            _detailsCts?.Dispose();
            _detailsCts = null;

            _loadingItem?.SetLoading(false);
            _loadingItem = null;
        }

        #endregion
    }
}
