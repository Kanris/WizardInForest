using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public TextMesh[] answerBox;
    public SpriteRenderer image;

    public int taskID = -1;
    public string objective = string.Empty;
    private TaskManagement taskManagement;

    private bool isPlayerNearby;
    private bool isAnswered = false;

    public void Start()
    {
        taskManagement = GameObject.FindObjectOfType<TaskManagement>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby)
        {
            if (!ReferenceEquals(dialogue, null))
            {
                isAnswered = true;
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue, answerBox, image);

                if (taskID >= 0 && string.IsNullOrEmpty(objective))
                {
                    StartCoroutine(TaskComplete());
                } 
                else
                {
                    StartCoroutine(UpdateTask());
                }
            }

            if (isAnswered) image.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEvent(true, collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerEvent(false, collision);

        FindObjectOfType<DialogueManager>().EndDialogue(answerBox);
        ShowInteractionButton(false);

        image.enabled = true;

    }

    private void OnTriggerEvent(bool enabled, Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger)
        {
            ShowInteractionButton(enabled);
            isPlayerNearby = enabled;
        }
    }

    private void ShowInteractionButton(bool enabled)
    {
        var hudButton = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>();

        var path = "Button/button_E";
        var image = Resources.Load<Sprite>(path);

        hudButton.sprite = image;

        hudButton.enabled = enabled;
    }

    private IEnumerator UpdateTask()
    {
        yield return taskManagement.AddTask(taskID, objective);
    }

    private IEnumerator TaskComplete()
    {
        yield return taskManagement.TaskComplete(taskID);
    }

}
