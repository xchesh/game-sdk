using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Common.Toolbox
{
    public static class AnimationExtensions
    {
        /// <summary>
        /// The hash of the motion time parameter.
        /// </summary>
        private static readonly int MotionTime = Animator.StringToHash("MotionTime");

        /// <summary>
        /// Play an animation with a callback at a specific moment.
        /// </summary>
        /// <param name="animator">The animator to play the animation on. </param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="normalizedEventMoment">The normalized time of the animation to invoke the callback at.</param>
        /// <param name="onEventCallback">The callback to invoke at the specified moment.</param>
        public static async UniTask DoMotionTime(
            this Animator animator,
            float duration,
            float normalizedEventMoment = 0.5f,
            Action onEventCallback = null)
        {
            var timer = 0f;
            var isInvoked = false;

            while (timer <= duration)
            {
                timer += Time.deltaTime;
                var normalizedTime = timer / duration;

                animator.SetFloat(MotionTime, normalizedTime);

                if (!isInvoked && normalizedTime >= normalizedEventMoment)
                {
                    isInvoked = true;
                    onEventCallback?.Invoke();
                }

                await UniTask.Yield();
            }
        }
    }
}
