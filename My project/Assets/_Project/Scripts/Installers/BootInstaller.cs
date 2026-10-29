using _Project.Scripts.AudioSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class BootInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private AudioManager _audioManager;

        #endregion
        
        #region Bindings

        public override void InstallBindings()
        {
            Container.Bind<IAudioManager>().FromInstance(_audioManager).AsSingle();
        }

        #endregion
    }
}