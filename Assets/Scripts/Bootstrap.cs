using UnityEngine;
using UnityEngine.AddressableAssets;

[JetBrains.Annotations.UsedImplicitly]
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AssetReference _scene;
    
    private void Start()
    {
        Addressables.LoadSceneAsync(_scene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
