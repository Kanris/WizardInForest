using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromRock : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var playerHealthHud = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

            playerHealthHud.ManageHealth(-1);

            Debug.Log("Hit");
        }
    }
}
