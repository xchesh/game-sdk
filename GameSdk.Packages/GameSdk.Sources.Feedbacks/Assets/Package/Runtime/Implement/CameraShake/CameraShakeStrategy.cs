using System;
using System.Linq;
using System.Threading;
using GameSdk.Core.Loggers;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class CameraShakeStrategy : IFeedbackStrategy<CameraShakeData>
    {
        public async Awaitable Execute(CameraShakeData data, CancellationToken cancellationToken, params object[] parameters)
        {
            var camera = parameters.OfType<Camera>().FirstOrDefault() ?? Camera.main;

            if (camera is null)
            {
                SystemLog.LogWarning(FeedbackManager.TAG, "Camera not provided and Camera.main is null");

                return;
            }

            var originalPos = camera.transform.localPosition;
            var elapsedTime = 0f;

            try
            {
                while (elapsedTime < data.Duration && !cancellationToken.IsCancellationRequested)
                {
                    var x = UnityEngine.Random.Range(-1f, 1f) * data.Intensity;
                    var y = UnityEngine.Random.Range(-1f, 1f) * data.Intensity;

                    camera.transform.localPosition = new Vector3(x, y, originalPos.z);

                    elapsedTime += Time.deltaTime;

                    try
                    {
                        await Awaitable.NextFrameAsync(cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }
            }
            finally
            {
                camera.transform.localPosition = originalPos;
            }
        }
    }
}
