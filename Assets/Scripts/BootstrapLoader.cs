using PrimeTween;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _scene;

    [SerializeField, Space(10)] private Transform _loader;
    [SerializeField] private TweenSettings<Vector3> _loaderAnimation;

    private void Start()
    {
        Tween.LocalEulerAngles(_loader, _loaderAnimation);
        
        Addressables.LoadSceneAsync(_scene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}