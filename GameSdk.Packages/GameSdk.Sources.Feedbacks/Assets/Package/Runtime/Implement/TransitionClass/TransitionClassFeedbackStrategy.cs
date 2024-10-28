using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameSdk.Sources.Feedbacks
{
    public class TransitionClassFeedbackStrategy : IFeedbackStrategy<TransitionClassFeedbackData>
    {
        public async UniTask Execute(TransitionClassFeedbackData data, CancellationToken cancellationToken, params object[] parameters)
        {
            if (parameters.Length == 0 || !(parameters[0] is VisualElement visualElement))
            {
                Debug.LogWarning("VisualElement must be provided as the first parameter.");
                return;
            }

            var taskCompletionSource = AutoResetUniTaskCompletionSource.Create();

            visualElement.RemoveFromClassList(data.TransitionClass);
            visualElement.RegisterCallback<TransitionEndEvent>(OnTransitionEndEvent);
            visualElement.schedule.Execute(() => { visualElement.AddToClassList(data.TransitionClass); }).ExecuteLater(0);

            try
            {
                await taskCompletionSource.Task.AttachExternalCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                visualElement.RemoveFromClassList(data.TransitionClass);
            }

            return;

            void OnTransitionEndEvent(TransitionEndEvent evt)
            {
                if (evt.stylePropertyNames.Contains(data.TransitionClass) is false)
                {
                    return;
                }

                visualElement.UnregisterCallback<TransitionEndEvent>(OnTransitionEndEvent);
                taskCompletionSource.TrySetResult();
            }
        }
    }
}
