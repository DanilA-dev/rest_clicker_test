using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.AudioSystem
{
    public class AudioPlayer : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<AudioConfig> _audioConfigs = new();

        private AudioSource _audioSource;
        private AudioConfig _lastAudioConfig;
        private IAudioManager _audioManager;

        #endregion

        #region Properties

        private AudioSource Source
        {
            get
            {
                if (_audioSource == null && !TryGetComponent(out _audioSource))
                {
                    _audioSource = gameObject.AddComponent<AudioSource>();
                    _audioSource.playOnAwake = false;
                    _audioSource.mute = _audioManager != null && _audioManager.IsMuted;
                }
                return _audioSource;
            }
        }

        #endregion

        #region Construct

        [Inject]
        public void Construct([InjectOptional] IAudioManager audioManager) => _audioManager = audioManager;

        #endregion

        #region Monobehaviour

        private void Awake()
        {
            TryGetComponent(out _audioSource);
            TryStartAwakeAudio();
        }

        private void Start()
        {
            if (_audioManager != null)
                _audioManager.OnMuteStateChanged += OnMuteChanged;
            if (_audioSource != null && _audioManager != null)
                _audioSource.mute = _audioManager.IsMuted;
        }

        private void OnDestroy()
        {
            if (_audioManager != null)
                _audioManager.OnMuteStateChanged -= OnMuteChanged;
        }

        #endregion

        #region Public

        public void Play(string configName)
        {
            var configByName = _audioConfigs.FirstOrDefault(a => a.AudioConfigName == configName);
            if (configByName == null)
                return;

            Play(configByName);
        }

        public void PlayOneShot(string configName)
        {
            var configByName = _audioConfigs.FirstOrDefault(a => a.AudioConfigName == configName);
            if (configByName == null)
                return;

            PlayOneShot(configByName);
        }

        public void PlayOneShotWithDelay(string configName)
        {
            var configByName = _audioConfigs.FirstOrDefault(a => a.AudioConfigName == configName);
            if (configByName == null)
                return;

            PlayOneShotWithDelay(configByName);
        }

        public void PlayWithFade(string configName)
        {
            var configByName = _audioConfigs.FirstOrDefault(a => a.AudioConfigName == configName);
            if (configByName == null)
                return;

            PlayWithFade(configByName);
        }


        public void Play(int configIndex)
        {
            if (_audioConfigs[configIndex] == null)
                return;

            Play(_audioConfigs[configIndex]);
        }

        public void PlayOneShot(int configIndex)
        {
            if (_audioConfigs[configIndex] == null)
                return;

            PlayOneShot(_audioConfigs[configIndex]);
        }

        public void PlayOneShotWithDelay(int configIndex)
        {
            if (_audioConfigs[configIndex] == null)
                return;

            PlayOneShotWithDelay(_audioConfigs[configIndex]);
        }

        public void PlayWithFade(int configIndex)
        {
            if (_audioConfigs[configIndex] == null)
                return;

            PlayWithFade(_audioConfigs[configIndex]);
        }

        public void Play(AudioConfig audioConfig)
        {
            if (audioConfig == null)
                return;

            if (audioConfig.RouteThroughManager && _audioManager != null)
            {
                _lastAudioConfig = audioConfig;
                _audioManager.RequestSound(audioConfig, transform.position);
                return;
            }

            AudioSource source = Source;
            audioConfig.SetAudioSource(ref source);
            _lastAudioConfig = audioConfig;
            switch (audioConfig.DelayType)
            {
                case DelayType.None:
                    source.Play();
                    break;
                case DelayType.SimpleDelay:
                    source.PlayDelayed(audioConfig.Delay);
                    break;
                case DelayType.ScheduledDelay:
                    source.PlayScheduled(audioConfig.ScheduledTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void PlayOneShot(AudioConfig audioConfig)
        {
            if (audioConfig == null)
                return;

            if (audioConfig.RouteThroughManager && _audioManager != null)
            {
                _lastAudioConfig = audioConfig;
                _audioManager.RequestSound(audioConfig, transform.position);
                return;
            }

            AudioSource source = Source;
            audioConfig.SetAudioSource(ref source);
            _lastAudioConfig = audioConfig;
            source.PlayOneShot(audioConfig.GetClip());
        }

        public void PlayOneShotWithDelay(AudioConfig audioConfig)
        {
            if (audioConfig == null)
                return;

            StartCoroutine(OneShotAudioDelayed(audioConfig));
        }

        public void PlayWithFade(AudioConfig audioConfig)
        {
            if (audioConfig == null)
                return;

            AudioSource source = Source;
            audioConfig.SetAudioSource(ref source);
            if (source.isPlaying && _lastAudioConfig != null)
                StartCoroutine(FadePlay(audioConfig));
            else
                Play(audioConfig);
        }

        public void Stop()
        {
            if (_lastAudioConfig != null
                && _lastAudioConfig.RouteThroughManager
                && _audioManager != null)
            {
                _audioManager.StopSound(_lastAudioConfig);
                return;
            }

            if (_audioSource != null)
                _audioSource.Stop();
        }

        public void StopWithFade()
        {
            if (_lastAudioConfig != null
                && _lastAudioConfig.RouteThroughManager
                && _audioManager != null)
            {
                _audioManager.StopSoundWithFade(_lastAudioConfig);
                return;
            }

            if (_audioSource != null && _audioSource.isPlaying && _lastAudioConfig != null)
                StartCoroutine(FadeStop());
            else
                Stop();
        }

        public void Pause()
        {
            if (_audioSource != null)
                _audioSource.Pause();
        }

        public void UnPause()
        {
            if (_audioSource != null)
                _audioSource.UnPause();
        }

        #endregion

        #region Private

        private void OnMuteChanged(bool muted)
        {
            if (_audioSource != null)
                _audioSource.mute = muted;
        }

        private void TryStartAwakeAudio()
        {
            if (_audioConfigs.Count <= 0)
                return;

            var firstAwakeAudio = _audioConfigs.FirstOrDefault(a => a.PlayOnAwake);
            if (firstAwakeAudio == null)
                return;

            Play(firstAwakeAudio);
        }

        #endregion

        #region Coroutines

        private IEnumerator FadePlay(AudioConfig audioConfig)
        {
            AudioSource source = Source;
            if (source.isPlaying)
            {
                for (float i = 0; i < _lastAudioConfig.FadeTime; i += Time.deltaTime)
                {
                    source.volume = _lastAudioConfig.Volume - (i / _lastAudioConfig.FadeTime);
                    yield return null;
                }
            }

            source.Stop();
            audioConfig.SetAudioSource(ref source);
            source.Play();

            for (float i = 0; i < audioConfig.FadeTime; i += Time.deltaTime)
            {
                source.volume = (i / audioConfig.FadeTime) * 1;
                yield return null;
            }
        }

        private IEnumerator OneShotAudioDelayed(AudioConfig audioConfig)
        {
            yield return new WaitForSeconds(audioConfig.Delay);
            PlayOneShot(audioConfig);
        }

        private IEnumerator FadeStop()
        {
            AudioSource source = Source;
            if (source.isPlaying)
            {
                float startVolume = source.volume;
                for (float i = 0; i < _lastAudioConfig.FadeTime; i += Time.deltaTime)
                {
                    source.volume = startVolume * (1 - i / _lastAudioConfig.FadeTime);
                    yield return null;
                }
            }

            source.Stop();
            source.volume = _lastAudioConfig.Volume;
        }

        #endregion
    }
}
