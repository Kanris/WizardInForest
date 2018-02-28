using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public TextMesh[] answerBox;
    public SpriteRenderer image;

    [TextArea]
    public string task = string.Empty;
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

                if (!string.IsNullOrEmpty(task))
                {
                    StartCoroutine(TaskComplete());
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

    private IEnumerator TaskComplete()
    {
        if (taskManagement.currentTasks.Contains(task))
        {
            yield return taskManagement.TaskComplete(task);
        }
    }

}
