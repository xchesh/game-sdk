using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace GameSdk.Sources.Feedbacks
{
    public class SoundFeedbackStrategy : IFeedbackStrategy<SoundFeedbackData>
    {
        private static readonly ObjectPool<AudioSource> audioSourcePool = new ObjectPool<AudioSource>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true,
            10,
            100
        );

        private static AudioSource CreatePooledItem()
        {
            var audioSource = new GameObject("PooledAudioSource").AddComponent<AudioSource>();
            return audioSource;
        }

        private static void OnTakeFromPool(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(true);
        }

        private static void OnReturnedToPool(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.gameObject.SetActive(false);
        }

        private static void OnDestroyPoolObject(AudioSource audioSource)
        {
            Object.Destroy(audioSource.gameObject);
        }

        public async UniTask Execute(SoundFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            var audioSource = audioSourcePool.Get();

            audioSource.clip = data.SoundClip;
            audioSource.outputAudioMixerGroup = data.AudioMixerGroup;
            audioSource.bypassEffects = data.BypassEffects;
            audioSource.bypassListenerEffects = data.BypassListenerEffects;
            audioSource.bypassReverbZones = data.BypassReverbZones;
            audioSource.loop = data.Loop;
            audioSource.pitch = data.Pitch;
            audioSource.panStereo = data.StereoPan;
            audioSource.spatialBlend = data.SpatialBlend;
            audioSource.reverbZoneMix = data.ReverbZoneMix;
            audioSource.volume = data.Volume;

            audioSource.Play();

            if (!data.Loop)
            {
                try
                {
                    await UniTask.WaitUntil(() => !audioSource.isPlaying || cancellationToken.IsCancellationRequested, cancellationToken: cancellationToken);
                }
                finally
                {
                    audioSourcePool.Release(audioSource);
                }
            }

            await UniTask.CompletedTask;
        }
    }
}
