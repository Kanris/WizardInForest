using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFromRock : MonoBehaviour {

    [SerializeField]
    private AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            PlayHitSound();
            HitPlayer();
        }
    }

    private void PlayHitSound()
    {
        var announcerAudio = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>();
        announcerAudio.clip = hitSound;
        announcerAudio.Play();
    }

    private void HitPlayer()
    {
        var playerHealthHud = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerHealthHud.ManageHealth(-1);
    }
}
