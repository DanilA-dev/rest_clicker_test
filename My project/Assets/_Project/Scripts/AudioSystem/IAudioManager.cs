using System;
using UnityEngine;

namespace _Project.Scripts.AudioSystem
{
    public interface IAudioManager
    {
        public event Action<bool> OnMuteStateChanged;

        public bool IsMuted { get; }
        public bool IsMusicMuted { get; }
        public bool IsSFXMuted { get; }

        public void SetVolume(MixerGroupType type, float value);
        public AudioMixerGroupVolumeConfig GetMixerGroup(MixerGroupType type);
        public void SetMusicVolume(float value);
        public void SetSFXVolume(float value);

        public void MuteAllAudioSources(bool isMute);
        public void MuteMusic(bool isMute);
        public void MuteSFX(bool isMute);

        public void RequestSound(AudioConfig config, Vector3 worldPos);
        public void StopSound(AudioConfig config);
        public void StopSoundWithFade(AudioConfig config);
    }
}
