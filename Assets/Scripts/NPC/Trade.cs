using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour {

    [SerializeField]
    [TextArea(2, 10)]
    private string sentence;

    private TextMesh[] traderAnswer;

    private bool isPlayerNear = false;

    [SerializeField]
    private GameObject inventory;

    private void Start()
    {
        InitializeAnswerTextMeshes();
    }

    private void InitializeAnswerTextMeshes()
    {
        traderAnswer = new TextMesh[gameObject.transform.childCount];

        for (int index = 0; index < traderAnswer.Length; index++)
        {
            traderAnswer[index] = gameObject.transform.GetChild(index).GetComponent<TextMesh>();
        }
    }

    private void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.isGamePaused)
            {
                DisplaySentence(sentence); //displaye trader message
                ShowHideInventory();
            }
        }
    }

    private void DisplaySentence(string sentence)
    {
        traderAnswer[0].text = traderAnswer[1].text = sentence; //displaye trader message
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractionWithPlayer(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractionWithPlayer(false, collision);
    }

    private void ShowHideInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    private void InteractionWithPlayer(bool show, Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //if player is near trader
        {
            isPlayerNear = show;
            var hudInteractionButton = FindObjectOfType<PlayerInteractionButtons>();

            if (show)
            {
                hudInteractionButton.ShowInteractionButton(PlayerInteractionButtons.Buttons.E);
            }
            else
            {
                hudInteractionButton.HideInteractionButton(); //hide interaction button
                DisplaySentence(string.Empty);
				ShowHideInventory ();
            }
        }
    }
}
