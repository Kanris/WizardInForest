using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFromRock : MonoBehaviour {

    [SerializeField]
    private AudioClip hitSound;
    public bool isPlayerNear = false;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger &&
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isAttacking)
        {
            isPlayerNear = true;
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isPlayerNear;
            yield return HitPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isPlayerNear;
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

        while (isPlayerNear)
        {
            yield return new WaitForSeconds(1.5f);
            if (isPlayerNear)
            {
                playerHealthHud.ManageHealth(-1);
                PlayHitSound();
            }
        }
    }
}
