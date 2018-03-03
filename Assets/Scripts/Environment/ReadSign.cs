using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour {

    private Image buttonImage;
    private TextMesh[] displayText = new TextMesh[2];
    [TextArea]
    public string signText = string.Empty;

    public SpriteRenderer image = null;

    private TaskManagement taskManagement;
    public int taskID = -1;
    public string task = string.Empty;
    public bool isTaskUpdate = false;

    private Text announcer;

    private bool isPlayerNearSign = false;

    public AudioSource audio = null;
    private bool isPlayed = false;

    private static bool isNextTriggerTriggered = false;

    private void Start()
    {
        taskManagement = GameObject.FindObjectOfType<TaskManagement>();
        announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //Helper.GetObject<Text>("Announcer");

        buttonImage = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>(); //Helper.GetObject<Image>("Player Button Image");
        displayText[0] = GameObject.FindGameObjectsWithTag("Player_Answer")[0].GetComponent<TextMesh>(); //Helper.GetObject<TextMesh>("Player's answer");
        displayText[1] = GameObject.FindGameObjectsWithTag("Player_Answer")[1].GetComponent<TextMesh>();

        if (task != string.Empty && 
            (taskManagement.IsTaskAdded(taskManagement.currentTasks, taskID) >= 0 ||
            taskManagement.IsTaskAdded(taskManagement.completedTasks, taskID) >= 0))
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearSign)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerAnswer();
                StartCoroutine(Task());
            }
        }
    }

    private void PlayerAnswer()
    {
        if (displayText != null)
        {
            signText = signText.Replace("<br>", "\n");
            displayText[0].text = displayText[1].text = signText;
        }
    }

    private IEnumerator Task()
    {
        if (image != null)
        {
            if (image.enabled)
            {
                image.enabled = false;

                yield return taskManagement.AddTask(taskID, task, isTaskUpdate);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && audio != null && !isPlayed)
        {
            audio.Play();
            isPlayed = true;
        }

        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            ShowInteractionButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && !isNextTriggerTriggered)
        {
            HideInteraction();
        }
    }

    private void ShowInteractionButton()
    {
        if (buttonImage != null)
        {
            var path = "Button/button_E";
            var image = Resources.Load<Sprite>(path);

            buttonImage.sprite = image;
            buttonImage.enabled = true;
            isPlayerNearSign = true;
        }
    }

    private void HideInteraction()
    {
        if (buttonImage != null)
        {
            buttonImage.enabled = false;
        }

        if (displayText != null)
        {
            displayText[0].text = displayText[1].text = string.Empty;
        }

        isPlayerNearSign = false;
    }
}
