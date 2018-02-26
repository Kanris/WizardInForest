using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private int health = 5;

    private Text healthBar;
    private int nextXPosition = 0;
	// Use this for initialization
	void Start ()
    {
        healthBar = GameObject.FindGameObjectWithTag("HUD_Health").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

		if (health == 0)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player.isPlayerDead = true;

            var announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();
            announcer.text = "You died";

            var screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
            StartCoroutine(screenFader.FadeToBlack());
        }

        UpdateHealthText();


    }

    public void ManageHealth(int health)
    {
        this.health += health;
    }

    private void UpdateHealthText()
    {
        healthBar.text = "Health: " + health;
    }

}
