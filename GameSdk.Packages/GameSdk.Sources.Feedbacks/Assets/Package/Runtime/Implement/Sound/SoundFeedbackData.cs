using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Scripting;

namespace GameSdk.Sources.Feedbacks
{
    [Serializable]
    public class SoundFeedbackData : IFeedbackData
    {
        [SerializeField] private AudioClip _soundClip;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private bool _bypassEffects;
        [SerializeField] private bool _bypassListenerEffects;
        [SerializeField] private bool _bypassReverbZones;
        [SerializeField] private bool _loop;
        [SerializeField] private float _pitch;
        [SerializeField] private float _stereoPan;
        [SerializeField] private float _spatialBlend;
        [SerializeField] private float _reverbZoneMix;
        [SerializeField] private float _volume;

        public AudioClip SoundClip => _soundClip;
        public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
        public bool BypassEffects => _bypassEffects;
        public bool BypassListenerEffects => _bypassListenerEffects;
        public bool BypassReverbZones => _bypassReverbZones;
        public bool Loop => _loop;
        public float Pitch => _pitch;
        public float StereoPan => _stereoPan;
        public float SpatialBlend => _spatialBlend;
        public float ReverbZoneMix => _reverbZoneMix;
        public float Volume => _volume;

        [RequiredMember]
        public SoundFeedbackData()
        {
        }

        [RequiredMember]
        public SoundFeedbackData(
            AudioClip soundClip,
            AudioMixerGroup audioMixerGroup,
            bool bypassEffects,
            bool bypassListenerEffects,
            bool bypassReverbZones,
            bool loop,
            float pitch,
            float stereoPan,
            float spatialBlend,
            float reverbZoneMix,
            float volume)
        {
            _soundClip = soundClip;
            _audioMixerGroup = audioMixerGroup;
            _bypassEffects = bypassEffects;
            _bypassListenerEffects = bypassListenerEffects;
            _bypassReverbZones = bypassReverbZones;
            _loop = loop;
            _pitch = pitch;
            _stereoPan = stereoPan;
            _spatialBlend = spatialBlend;
            _reverbZoneMix = reverbZoneMix;
            _volume = volume;
        }
    }
}
