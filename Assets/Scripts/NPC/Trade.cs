using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour {

    [SerializeField]
    private TextMesh[] traderAnswer;

    [SerializeField]
    [TextArea(2, 10)]
    private string sentence;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.isGamePaused)
        {
            AnswerToPlayer(this.sentence);
        }
    }

    private void AnswerToPlayer(string sentence)
    {
        traderAnswer[0].text = traderAnswer[1].text = sentence; //displaye trader message
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        ShowInteractionButton(true, collision); //show interaction button
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShowInteractionButton(false, collision); //hide all interactions with trader
    }

    private void ShowInteractionButton(bool show, Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //if player is near trader
        {
            var hudInteractionButton = FindObjectOfType<PlayerInteractionButtons>();

            if (show)
            {
                hudInteractionButton.ShowInteractionButton(PlayerInteractionButtons.Buttons.E); //show button e
            }
            else
            {
                hudInteractionButton.HideInteractionButton(); //hide interaction button
                AnswerToPlayer(string.Empty); //clear message dialoge
            }
        }
    }
}
