using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameSdk.Core.Toolbox;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class FeedbackPlayer : MonoBehaviour
    {
        [SerializeField]
        private FeedbackPlaybackType _playbackType = FeedbackPlaybackType.PARALLEL;

        [SerializeReference, SerializeReferenceDropdown(typeof(IFeedbackData))]
        private IFeedbackData[] _feedbacks = Array.Empty<IFeedbackData>();

        public async UniTask PlayFeedbacks(CancellationToken cancellationToken = default, params object[] parameters)
        {
            if (_feedbacks is { Length: > 0 })
            {
                await FeedbackManager.PlayFeedback(_feedbacks, _playbackType, cancellationToken, parameters);
            }
        }
    }
}
