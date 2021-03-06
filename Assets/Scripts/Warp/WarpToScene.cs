﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarpToScene : MonoBehaviour {

    [SerializeField]
    private string sceneName; //warp destination
    [SerializeField]
    private AudioClip audio; //new scene audio

    [SerializeField]
    private float x = 0f; //warp position x
    [SerializeField]
    private float y = 0f; //warp position y

    public static bool isWarping = false; //is player warping

    private LoadingScene loadingScene; //loading canvas

    private void Start()
    {
        loadingScene = FindObjectOfType<LoadingScene>(); //find loading canvas
    }

    //warp triggered
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //player triggered
        {
            if (!isWarping) //player is not warping
            {
                SaveGameObject(); //save needed gameobject

                //SceneManager.LoadScene(sceneName); //load new scene
                yield return loadingScene.LoadingSceneAsync(sceneName); //start async loading scene and show loading canvas
            }
        }
    }

    //save gameobject to another scene
    private void SaveGameObject()
    {
        var player = GameObject.FindGameObjectWithTag("Player"); //find player
        var camera = GameObject.FindGameObjectWithTag("MainCamera"); //find camera
        var eventSystem = GameObject.FindGameObjectWithTag("EventSystem"); //find event system

        DontDestroyOnLoad(player); //save player
        DontDestroyOnLoad(camera); //save camera
        DontDestroyOnLoad(eventSystem); //save event system
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("HUD")); //save hud

        MovePlayerToPosition(player, camera); //move player and camera to new position

        ChangeSceneMusic(camera); //change music
    }

    //move player and camera to new position
    private void MovePlayerToPosition(GameObject player, GameObject camera)
    {
        if (player != null)
        {
            var warpPosition = new Vector2(x, y);
            player.GetComponent<Rigidbody2D>().transform.position = warpPosition;
            camera.transform.position = warpPosition;
        }
    }

    //change music
    private void ChangeSceneMusic(GameObject camera)
    {
        if (camera != null)
        {
            camera.GetComponent<AudioSource>().clip = audio;
            camera.GetComponent<AudioSource>().Play();
        }
    }
}
