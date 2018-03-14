using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInventoryItem : MonoBehaviour {

    [SerializeField]
    private string ItemName;

    private void Update()
    {
        if (!WarpToScene.isWarping && !ScreenFader.isFading) //if player not warping
        {
            if (!PauseMenu.isGamePaused) //game is not paused
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(FindObjectOfType<Inventory>().AddInventory(ItemName));
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            FindObjectOfType<PlayerInteractionButtons>().ShowInteractionButton(PlayerInteractionButtons.Buttons.E);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            FindObjectOfType<PlayerInteractionButtons>().HideInteractionButton();
        }
    }
}
