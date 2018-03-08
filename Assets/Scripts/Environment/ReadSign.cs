using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour {

    [TextArea]
    public string signText = string.Empty; //text to show

    public SpriteRenderer image = null; //task image

    private TaskManagement taskManagement; //task management system
    [SerializeField]
    private TaskInfo taskInfo; //task description
    private Text announcer; //hud announcer

    private bool isPlayerNearSign = false; //is player near sign

    private void Start()
    {
        taskManagement = FindObjectOfType<TaskManagement>(); //get taskmanagement
        announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //get hud announcer

        if (taskInfo.task != string.Empty && 
            (taskManagement.IsTaskAdded(taskManagement.currentTasks, taskInfo.taskID) >= 0 ||
            taskManagement.IsTaskAdded(taskManagement.completedTasks, taskInfo.taskID) >= 0)) //if task added/completed
        {
            image.enabled = false; //hide task image
        }
    }

    private void Update()
    {
        if (isPlayerNearSign) //if player is near sign
        {
            if (Input.GetKeyDown(KeyCode.E)) //if player pressed E button
            {
                FindObjectOfType<PlayerAnswer>().DisplayPlayerAnswer(signText); //display message
                StartCoroutine(ReadTask()); //show task
            }
        }
    }

    //Task is read
    private IEnumerator ReadTask()
    {
        if (image != null) //if there is task image
        {
            if (image.enabled) //if task is not read
            {
                image.enabled = false; //hide task image
                yield return taskManagement.AddUpdateTask(taskInfo, "Player", new string[] { signText }); //add task to HUD
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //if player is near sign
        {
            FindObjectOfType<PlayerInteractionButtons>().ShowInteractionButton(PlayerInteractionButtons.Buttons.E); //show interaction buttons
            isPlayerNearSign = true; //player is near sign
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //if player leave sign
        {
            FindObjectOfType<PlayerInteractionButtons>().HideInteractionButton(); //hide interaction buttons
            FindObjectOfType<PlayerAnswer>().HidePlayerAnswer(); //hide player answer

            isPlayerNearSign = false;
        } 
    }
}
