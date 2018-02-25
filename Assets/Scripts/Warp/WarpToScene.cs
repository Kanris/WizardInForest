using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpToScene : MonoBehaviour {

    [SerializeField]
    private string sceneName;
    [SerializeField]
    private AudioClip audio;

    [SerializeField]
    private float x = 0f;
    [SerializeField]
    private float y = 0f;


    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            ScreenFader screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

            yield return screenFader.FadeToBlack();

            var player = GameObject.FindGameObjectWithTag("Player");
            var camera = GameObject.FindGameObjectWithTag("MainCamera");

            DontDestroyOnLoad(player);
            DontDestroyOnLoad(camera);
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("HUD"));

            MovePlayerToPosition(player, camera);

            ChangeSceneMusic(camera);

            SceneManager.LoadScene(sceneName);

            yield return screenFader.FadeToClear();
        }
    }

    private void MovePlayerToPosition(GameObject player, GameObject camera)
    {
        if (player != null)
        {
            var warpPosition = new Vector2(x, y);
            player.GetComponent<Rigidbody2D>().transform.position = warpPosition;
            camera.transform.position = warpPosition;
        }
    }

    private void ChangeSceneMusic(GameObject camera)
    {
        if (camera != null)
        {
            camera.GetComponent<AudioSource>().clip = audio;
            camera.GetComponent<AudioSource>().Play();
        }
    }
}
