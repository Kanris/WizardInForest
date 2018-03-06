using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReadSign : MonoBehaviour {

    private Image buttonImage; //player interaction button image
    private TextMesh[] displayText = new TextMesh[2]; //answer boxes
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

        buttonImage = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>(); //get hud buttons
        displayText[0] = GameObject.FindGameObjectsWithTag("Player_Answer")[0].GetComponent<TextMesh>(); //get answer boxes
        displayText[1] = GameObject.FindGameObjectsWithTag("Player_Answer")[1].GetComponent<TextMesh>();

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
                PlayerAnswer(); //display message
                StartCoroutine(ReadTask()); //show task
            }
        }
    }

    //display message
    private void PlayerAnswer()
    {
        displayText[0].text = displayText[1].text = signText;
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
            ShowInteractionButton(); //show interaction buttons
            isPlayerNearSign = true; //player is near sign
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger) //if player leave sign
        {
            HideInteractionButton(); //hide interaction buttons
            isPlayerNearSign = false;
        } 
    }

    //show intercation buttons for player
    private void ShowInteractionButton()
    {
        if (buttonImage != null) //if hud buttons found
        {
            //load E button image
            var path = "Button/button_E";
            var image = Resources.Load<Sprite>(path);
 
            buttonImage.sprite = image; //assign E button image to HUD
            buttonImage.enabled = true; //show interaction button
        }
    }

    private void HideInteractionButton()
    {
        buttonImage.enabled = false; //hide hud interaction button
        displayText[0].text = displayText[1].text = string.Empty; //clear answer boxes
    }
}
