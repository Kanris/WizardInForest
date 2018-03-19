using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDAnnouncer : MonoBehaviour {

    private Text hudText;

	// Use this for initialization
	void Start () {

        hudText = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();

    }
	
    public IEnumerator DisplayAnnounce(string text)
    {
        if (hudText)
        {
            hudText.text = text;

            yield return new WaitForSeconds(1f);

            hudText.text = string.Empty;
        }
    }

    public IEnumerator DisplayAnnounce(string text, float time)
    {
        if (hudText)
        {
            hudText.text = text;

            yield return new WaitForSeconds(time);

            hudText.text = string.Empty;
        }
    }
}
