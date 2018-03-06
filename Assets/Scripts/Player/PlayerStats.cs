using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private int MaxPlayerHealth = 4;
    [SerializeField]
    private int CurrentPlayerHealth = 4; //player tota

    public float AttackCooldown = 2.5f; //Cooldown
    public int FireballAttackValue = -1;
	
	// Update is called once per frame
	void Update () {

		if (CurrentPlayerHealth == 0)
        {
            StartCoroutine(EndGameScreen());
            CurrentPlayerHealth--;
        }
    }

    //heal or damage player
    public void ManageHealth(int health)
    {
        var isHeal = health > 0;
        var index = isHeal ? health : (health * -1);

        while (index > 0)
        {
            var imageIndex = isHeal ? CurrentPlayerHealth + 1 : CurrentPlayerHealth;
            DisplayHealth(imageIndex, isHeal);

            if (isHeal && CurrentPlayerHealth <= MaxPlayerHealth)
            {
                CurrentPlayerHealth += 1;
            }
            else
            {
                CurrentPlayerHealth -= 1;
            }

            index--;
        }

    }

    //show/hide health image
    private void DisplayHealth(int health, bool isEnabled)
    {
        var path = "Health" + CurrentPlayerHealth;
        var healthHUD = GameObject.Find(path);

        if (healthHUD != null)
        {
            healthHUD.GetComponent<Image>().enabled = isEnabled;
        }
    }

    //restart game
    private IEnumerator EndGameScreen()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isPlayerDead = true;

        var announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();
        announcer.text = "You died";

        var screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        yield return screenFader.FadeToBlack();

        yield return new WaitForSeconds(3f);

        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("HUD"));
        Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        Destroy(GameObject.FindGameObjectWithTag("EventSystem"));

        SceneManager.LoadScene("Menu");
    }

}
