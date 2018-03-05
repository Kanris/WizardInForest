using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!FindObjectOfType<DialogueManager>().isDialogueInProcess)
            {
                Instantiate(Resources.Load<GameObject>("Prefab/Fireball"));
                Debug.Log("create fireball");
            }
        }

	}
}
