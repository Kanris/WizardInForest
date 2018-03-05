using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour {

    [SerializeField]
    private AudioClip hitSound; //on hit sound
    [SerializeField]
    private float attackSpeed = 1.5f; //attack speed

    public bool isNearPlayer = false; //is enemy near the player

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger &&
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isAttacking) //if player a
        {
            isNearPlayer = true;
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isNearPlayer;
            yield return HitPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isNearPlayer;
        }
    }

    private void PlayHitSound()
    {
        var announcerAudio = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>();
        announcerAudio.clip = hitSound;
        announcerAudio.Play();
    }

    private IEnumerator HitPlayer()
    {
        var playerHealthHud = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        while (isNearPlayer)
        {
            yield return new WaitForSeconds(attackSpeed);
            if (isNearPlayer)
            {
                playerHealthHud.ManageHealth(-1);
                PlayHitSound();
            }
        }
    }
}
