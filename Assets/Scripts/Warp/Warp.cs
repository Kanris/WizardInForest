using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public Transform warpTarget;

    public AudioClip audio;
    public AudioClip sound;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            ScreenFader screenFador = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

            WarpSound();
            yield return StartCoroutine(screenFador.FadeToBlack());

            collision.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;

            LostSight();

            ChangeMusic();

            yield return StartCoroutine(screenFador.FadeToClear());
        }

    }

    private void WarpSound()
    {
        var announcer = GameObject.FindGameObjectsWithTag("HUD_Announcer")[0].GetComponent<AudioSource>();

        if (announcer != null & sound != null)
        {
            announcer.clip = sound;
            announcer.Play();
        }
    }

    private void ChangeMusic()
    {
        Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        if (camera != null && audio != null)
        {
            var audioSource = camera.GetComponent<AudioSource>();
            audioSource.clip = audio;
            audioSource.Play();
        }
    }

    private void LostSight()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.transform.parent.gameObject.GetComponent<RandomMovement>().isAttacking = false;
            }
        }
    }
}
