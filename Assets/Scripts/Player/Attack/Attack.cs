using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private TextMesh[] tmCooldown; //cooldown textmeshes
    private bool isInCooldown = false; //is player need to wait, before throw another fireball
    private double timerValue = 0d; //cooldown value

    private void Start()
    {
        var textMeshGM = GameObject.FindGameObjectsWithTag("Cooldown"); //get all objects with cooldown tag

        tmCooldown = new TextMesh[textMeshGM.Length]; //create textmesh array

        for (int index = 0; index < textMeshGM.Length; index++)
        {
            tmCooldown[index] = textMeshGM[index].GetComponent<TextMesh>(); //assign textmesh array with cooldown textmesshes
        }
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space) && !isInCooldown) //player press attack button
        {
            if (!FindObjectOfType<DialogueManager>().isDialogueInProcess) //if player is not in dialogue
            {
                Instantiate(Resources.Load<GameObject>("Prefab/Fireball")); //create fireball
                timerValue = FindObjectOfType<PlayerStats>().AttackCooldown; //get player attack cooldown
                isInCooldown = true; //player can't create another fireball

                StartCoroutine(StartCooldown()); //start countdown
            }
        }
	}

    //display cooldown to the player
    private IEnumerator StartCooldown()
    {
        while (isInCooldown) //while fireball is in cooldown
        {
            timerValue = Math.Round(timerValue, 1);
            if (timerValue == 0d) //if time is up
            {
                isInCooldown = false; //player can throw another fireball
                tmCooldown[0].text = tmCooldown[1].text = string.Empty; //empty cooldown textmeshes
            }
            else
            {
                timerValue -= 0.1d; //another 0.1s 
                tmCooldown[0].text = tmCooldown[1].text = timerValue.ToString() + "s"; //show cooldown value
                yield return new WaitForSeconds(0.1f); //wait for 0.1s
            }
        }
    }
}
