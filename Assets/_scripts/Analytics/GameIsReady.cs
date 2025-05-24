using MirraGames.SDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIsReady : MonoBehaviour
{
    [SerializeField] string sceneName;
    void Start()
    {
        MirraSDK.Analytics.GameIsReady();
        
        var operation=SceneManager.LoadSceneAsync(sceneName);
        operation.completed += (AsyncOperation obj) =>
        {
            MirraSDK.Analytics.GameplayStart();
        };
    }
}
