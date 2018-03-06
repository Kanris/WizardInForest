using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    private GameObject loadingScene;
    [SerializeField]
    private Slider loadingProgress;

    private AsyncOperation operation;

    private void Update()
    {
        if (operation != null)
        {
            if (operation.isDone && WarpToScene.isWarping)
            {
                Debug.Log("Hide loading scene");

                loadingScene.SetActive(false);

                WarpToScene.isWarping = false;
            }
        }
    }

    public IEnumerator LoadingSceneAsync(string sceneName)
    {
        WarpToScene.isWarping = true;
        operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingProgress.value = progress;

            yield return null;
        }
    }
}
