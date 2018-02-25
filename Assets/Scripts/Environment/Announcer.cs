using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Announcer : MonoBehaviour {

    [SerializeField]
    private string announcerText = string.Empty;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var hudAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();
            hudAnnouncer.text = announcerText;

            yield return new WaitForSeconds(2f);

            hudAnnouncer.text = string.Empty;

            Destroy(this);
        }
    }
}
