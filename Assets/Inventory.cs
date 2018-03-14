using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private GameObject inventory;
    private bool isActive = false;

	// Use this for initialization
	void Start () {

        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(isActive);

    }
	
	// Update is called once per frame
	void Update () {

        if (!WarpToScene.isWarping && !ScreenFader.isFading) //if player not warping
        {
            if (!PauseMenu.isGamePaused) //game is not paused
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    InventoryVisibility();
                }
            }
        }
	}

    private void InventoryVisibility()
    {
        isActive = !isActive;
        inventory.SetActive(isActive);
    }
}
