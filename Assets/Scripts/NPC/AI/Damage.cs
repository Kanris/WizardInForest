using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour {

    [SerializeField]
    private AudioClip hitSound; //on hit sound
    [SerializeField]
    private float attackSpeed = 1.5f; //attack speed
    [SerializeField]
    private int attackValue = -1; //attack value

    public bool isNearPlayer = false; //is enemy near the player

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger &&
            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isAttacking) //if enemy is near player
        {
            isNearPlayer = true; //enemy is near player

            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isNearPlayer; //stop enemy movement

            yield return HitPlayer(); //start hiting player
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //player leave enemy attack range
        {
            isNearPlayer = false; //player is not near the enemy

            gameObject.transform.parent.
                gameObject.GetComponent<RandomMovement>().isNearPlayer = isNearPlayer; //move to player
        }
    }

    //play hiting sound
    private void PlayHitSound() 
    {
        var announcerAudio = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>(); //get hud audio source
        announcerAudio.clip = hitSound; //set hiting sound 
        announcerAudio.Play(); //play hit sound
    }

    //hit player
    private IEnumerator HitPlayer()
    {
        var playerHealthHud = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); //get player stats

        while (isNearPlayer) //while player in enemy attack range
        {
            yield return new WaitForSeconds(attackSpeed); 

            if (isNearPlayer) //if player still is near enemy
            {
                playerHealthHud.ManageHealth(attackValue); //hit player
                PlayHitSound(); //play hit sound
            }
        }
    }
}
