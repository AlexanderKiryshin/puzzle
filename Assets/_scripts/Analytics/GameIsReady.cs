using MirraGames.SDK;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIsReady : MonoBehaviour
{
    [SerializeField] string sceneName;
    void Start()
    {
        StartCoroutine(GameIsReadyCoroutine());
    }

    private IEnumerator GameIsReadyCoroutine()
    {
        yield return new WaitUntil(() => MirraSDK.IsInitialized);
        MirraSDK.Analytics.GameIsReady();

        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += (AsyncOperation obj) =>
        {
            MirraSDK.Analytics.GameplayStart();
        };
    }
}
