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

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>(); //get hud buttons
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.isGamePaused)
        {
            AnswerToPlayer(this.sentence);
        }
    }

    private void AnswerToPlayer(string sentence)
    {
        traderAnswer[0].text = traderAnswer[1].text = sentence;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowInteractionButton(true, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ShowInteractionButton(false, collision);
    }

    private void ShowInteractionButton(bool show, Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            if (show)
            {
                var path = "Button/button_E";
                var imageE = Resources.Load<Sprite>(path);

                buttonImage.sprite = imageE;
            }
            else
            {
                AnswerToPlayer(string.Empty);
            }

            buttonImage.enabled = show;
        }
    }
}
