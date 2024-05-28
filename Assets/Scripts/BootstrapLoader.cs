using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(CanvasGroup))]
public class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _scene;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField, Space(10)] private Transform _loader;
    [SerializeField] private TweenSettings<Vector3> _loaderAnimation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Tween.LocalEulerAngles(_loader, _loaderAnimation);

        LoadScene().Forget();
    }

    private async UniTaskVoid LoadScene()
    {
#if UNITY_EDITOR
        await UniTask.Delay(Random.Range(1500, 3000));
#endif
        await Addressables.LoadSceneAsync(_scene);
        await Tween.Alpha(_canvasGroup, 1, 0, 0.5f);

        Destroy(gameObject);
    }
}