using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private AssetReference _scene;

    private void Start()
    {
        Addressables.LoadSceneAsync(_scene);
    }
}