using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private TextMesh[] tmCooldown;
    private bool isInCooldown = false;
    private double timerValue = 0d;

    private void Start()
    {
        var textMeshGM = GameObject.FindGameObjectsWithTag("Cooldown");

        tmCooldown = new TextMesh[textMeshGM.Length];

        for (int index = 0; index < textMeshGM.Length; index++)
        {
            tmCooldown[index] = textMeshGM[index].GetComponent<TextMesh>();
        }

        Debug.Log(textMeshGM.Length);
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space) && !isInCooldown) //player press attack button
        {
            if (!FindObjectOfType<DialogueManager>().isDialogueInProcess) //if player is not in dialogue
            {
                Instantiate(Resources.Load<GameObject>("Prefab/Fireball")); //create fireball
                timerValue = FindObjectOfType<PlayerStats>().AttackCooldown;
                isInCooldown = true;

                StartCoroutine(StartCooldown());
            }
        }
	}

    private IEnumerator StartCooldown()
    {
        while (isInCooldown)
        {
            timerValue = Math.Round(timerValue, 1);
            if (timerValue == 0d)
            {
                isInCooldown = false;
                tmCooldown[0].text = tmCooldown[1].text = string.Empty;
            }
            else
            {
                timerValue -= 0.1d;
                tmCooldown[0].text = tmCooldown[1].text = timerValue.ToString() + "s";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
