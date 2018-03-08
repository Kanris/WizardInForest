using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScene;
    public Slider slider;

    public void PlayGame()
    {
        string scene = "Limbo";
        StartCoroutine(LoadingSceneAsync(scene));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadingSceneAsync (string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }

}
