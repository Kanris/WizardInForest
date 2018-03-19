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
            yield return FindObjectOfType<HUDAnnouncer>().DisplayAnnounce(announcerText, 2f);

            Destroy(this);
        }
    }
}
