using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape)) //escape button pressed
        {
            if (isGamePaused) //if is game already paused
            {
                ResumeGame(); //resume game
            }
            else //if game is not paused
            {
                PauseGame(); //pause game
            }
        }

        if (!Application.isFocused) //if game is not in focus
        { 
            PauseGame(); //pause game
        }
	}

    //resume game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); //hide menu
        Time.timeScale = 1f; //unfreeze game
        isGamePaused = false; //game is not paused
    }

    //pause game
    private void PauseGame()
    {
        pauseMenuUI.SetActive(true); //show menu
        Time.timeScale = 0f; //freeze game
        isGamePaused = true; //game is paused
    }

    //load main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f; //unfreeze game
        isGamePaused = false;
        //destroy all DontDestroyOnLoad
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("HUD"));
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));

        //Load main menu 
        SceneManager.LoadScene("Menu");
    }

    //quit game button pressed
    public void QuitGame()
    {
        Application.Quit(); //quit game
    }
}
