using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    public class CameraShakeStrategy : IFeedbackStrategy<CameraShakeData>
    {
        public async UniTask Execute(CameraShakeData data, CancellationToken cancellationToken, params object[] parameters)
        {
            var camera = parameters.OfType<Camera>().FirstOrDefault() ?? Camera.main;

            if (camera is null)
            {
                Debug.LogWarning("Camera not provided and Camera.main is null");

                return;
            }

            var originalPos = camera.transform.localPosition;
            var elapsedTime = 0f;

            try
            {
                while (elapsedTime < data.Duration && !cancellationToken.IsCancellationRequested)
                {
                    var x = Random.Range(-1f, 1f) * data.Intensity;
                    var y = Random.Range(-1f, 1f) * data.Intensity;

                    camera.transform.localPosition = new Vector3(x, y, originalPos.z);

                    elapsedTime += Time.deltaTime;
                    await UniTask.Yield(cancellationToken);
                }
            }
            finally
            {
                camera.transform.localPosition = originalPos;
            }
        }
    }
}
