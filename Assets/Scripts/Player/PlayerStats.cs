using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public int Health = 4;
    public int fireballAtack = -1;
	
	// Update is called once per frame
	void Update () {

		if (Health == 0)
        {
            StartCoroutine(EndGameScreen());
        }
    }

    public void ManageHealth(int health)
    {
        if (health < 0)
        {
            GetHealthImage(Health, false);
            Health += health;
        } 
        else if (health > 0 && Health < 4)
        {
            Health += health;
            GetHealthImage(Health, true);

        }   
    }

    private void GetHealthImage(int health, bool isEnabled)
    {
        var path = "Health" + Health;
        var healthHUD = GameObject.Find(path).GetComponent<Image>();
        healthHUD.enabled = isEnabled;
    }

    private IEnumerator EndGameScreen()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.isPlayerDead = true;

        var announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();
        announcer.text = "You died";

        var screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        yield return screenFader.FadeToBlack();

        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

}
