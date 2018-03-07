using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    private GameObject loadingScene; //loading canvas
    [SerializeField]
    private Slider loadingProgress; //loading slider

    private AsyncOperation operation; //load operation

    private void Update()
    {
        if (operation != null) //if operation is not null
        {
            if (operation.isDone && WarpToScene.isWarping) //if operation is done and player warped
            {
                loadingScene.SetActive(false); //hide loading canbas

                WarpToScene.isWarping = false; //player is warped
            }
        }
    }

    //loading scene async
    public IEnumerator LoadingSceneAsync(string sceneName)
    {
        WarpToScene.isWarping = true; //player is warping
        operation = SceneManager.LoadSceneAsync(sceneName); //start loading new scene

        loadingScene.SetActive(true); //active loading canvas

        //while scene loading
        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);

            //show loading progress
            loadingProgress.value = progress;

            yield return null;
        }
    }
}
