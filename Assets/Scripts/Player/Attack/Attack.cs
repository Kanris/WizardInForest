using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space)) //player press attack button
        {
            if (!FindObjectOfType<DialogueManager>().isDialogueInProcess) //if player is not in dialogue
            {
                Instantiate(Resources.Load<GameObject>("Prefab/Fireball")); //create fireball
            }
        }
	}
}
