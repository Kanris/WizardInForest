using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskManagement : MonoBehaviour {

    private Text txtAnnouncer; //hud announcer
    private Text txtObjectives; //hud objectives

    public List<Task> currentTasks = new List<Task>(); //list of current task
    public List<Task> completedTasks = new List<Task>(); //list of completed task

    private AudioSource asAnnouncer; //task sound

    private GameObject journal; //journal gameobject
    private Text taskLog; //task log
    private GameObject buttonsGrid;
    private List<GameObject> journalbuttons;
    private bool isJournalOpen = false; //is journal is open
    private float nextYPos = 90;

    // Use this for initialization
    void Start () {
        txtObjectives = GameObject.FindGameObjectWithTag("HUD_Objectives").GetComponent<Text>(); //get hud objectvies
        txtAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //get hud announcer
        asAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>(); //get hud audio source

        journal = GameObject.FindGameObjectWithTag("Journal"); //get journal gameobject
        taskLog = GameObject.FindGameObjectWithTag("TaskLog").GetComponent<Text>(); //get task log
        buttonsGrid = GameObject.FindGameObjectWithTag("ButtonGrid"); //get grid for buttons
        journalbuttons = new List<GameObject>(); //list of added buttons
        journal.SetActive(false); //disable journal
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !PauseMenu.isGamePaused) //if pressed j open/close journal and game is not paused
        {
            StartCoroutine(OpenCloseJournal()); //open/close journal
        }
    }

    //open or close journal
    private IEnumerator OpenCloseJournal()
    {
        isJournalOpen = !isJournalOpen; //if journal is close - open; if journal is open - close
        journal.SetActive(isJournalOpen);

        var fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>(); //get fader

        taskLog.text = string.Empty; //clear task log

        yield return isJournalOpen ? fader.FadeToBlack() : fader.FadeToClear(); //if journal is opening - fade to black; if journal is closing - fade to clear
    }

    //load task log to journal
    public void LoadTaskLog(Text buttonText)
    {
        var taskText = buttonText.text; //get button text
        var tasks = currentTasks.Find(x => x.GetLog().Contains(taskText)); //find log in current task

        var sentences = tasks.GetSentences();

        taskLog.text = string.Empty; //empty task log

        foreach (var sentence in sentences)
        {
            taskLog.text += sentence + "\n";
        }

        taskLog.text += "\nTask:\n";

        foreach (var item in tasks.GetLog()) //show log in task log
        {
            taskLog.text += item + "\n";
        }
    }

    //add/update task
    public IEnumerator AddUpdateTask(TaskInfo taskInfo, string name, string[] sentences)
    {
        var indexInCompletedTask = IsTaskAdded(completedTasks, taskInfo.taskID, taskInfo.task); //search task in completed task list

        if (indexInCompletedTask == -1) //task is not in completed task
        {
            var index = IsTaskAdded(currentTasks, taskInfo.taskID, taskInfo.task); //search task in current task list
            var announcerMessage = string.Empty; //announcer message

            if (index >= 0) //if task is already in the current task list
            {
                announcerMessage = "Task <" + currentTasks[index].GetObjective() + ">updated!"; //form update announcer message
                currentTasks[index].UpdateObjective(taskInfo.task, name, sentences); //updated existed task

                ShowCurrentTasks(); //updated current task list in the HUD
            }
            else if (index == -1 && !taskInfo.isTaskUpdate) //if task is not in the current task list
            {
                var newTask = new Task(taskInfo.taskID, taskInfo.task, name, sentences); //form new task
                currentTasks.Add(newTask); //add new task to the current list task

                announcerMessage = "Task added!"; //form add announcer message
                ShowCurrentTask(currentTasks.Count - 1); //update current task list in the HUD

                AddTaskInJournal(taskInfo.task); //add task button to journal
            }

            yield return ShowAnnouncerMessage(announcerMessage); //show announcer message for player
        }
    }

    //add task button to journal
    private void AddTaskInJournal(string task)
    {
        var button = Instantiate(Resources.Load<GameObject>("Prefab/TaskButton"));

        var buttonText = button.transform.GetChild(0).GetComponent<Text>(); //get button text
        buttonText.text = task; //set button text with task 

        button.transform.SetParent(buttonsGrid.transform); //add button to buttonsGrid
        button.GetComponent<RectTransform>().localPosition = new Vector3(0, nextYPos, 0); //set button position
        button.GetComponent<Button>().onClick.AddListener(() => LoadTaskLog(buttonText)); //add on click listener

        journalbuttons.Add(button); //add button to list

        nextYPos -= 30; //next button position
    }

    //show announcer message for player
    private IEnumerator ShowAnnouncerMessage(string announcerMessage)
    {
        txtAnnouncer.text = announcerMessage; //show announcer message text

        yield return new WaitForSeconds(5f); //leave message for 5 seconds

        txtAnnouncer.text = string.Empty; //clear announcer message text
    }

    //search task in the lists
    public int IsTaskAdded(List<Task> list, int id, string task = "")
    {
        var item = list.Find(x => x.GetID() == id); //find item with id

        if (item == null) //item with id doesn't found
            return -1;

        if (item.GetObjective() != task) //if current task is not equal to new task
        {
            if (!item.GetLog().Contains(task)) //if log doesn't contain new task
            {
                return list.IndexOf(item); //return index of this item
            }
        }

        return -2; //task is current or in the log
    }

    //task complete
    public IEnumerator TaskComplete(int id)
    {
        var completedTask = currentTasks.Find(x => x.GetID() == id); //find task with ID in current task

        if (completedTask != null) //if task is found
        {
            currentTasks.Remove(completedTask); //remove task from current task list
            completedTasks.Add(completedTask); //add task in complete task list

            var announcerMessage = "Task <" + completedTask.GetObjective() + ">\n complete!"; //form announcer message about task completion
            ShowCurrentTasks(); //update current task HUD

            DeleteJournalButton(completedTask);

            yield return ShowAnnouncerMessage(announcerMessage); //show announcer message

        }
    }
    //delete button from grid
    private void DeleteJournalButton(Task completedTask)
    {
        foreach (var button in journalbuttons) //find button in list
        {
            var buttonText = button.transform.GetChild(0).GetComponent<Text>().text; //get button text
            if (completedTask.GetLog()[0] == buttonText) //if button text is equal to completed task
            {
                Destroy(button); //disable button
                break;
            }
        }
    }

    //Update whole task list in HUD
    private void ShowCurrentTasks()
    {
        txtObjectives.text = string.Empty; //clear HUD task list

        //add tasks from currentTasks to HUD task list
        foreach (var task in currentTasks)
        {
            txtObjectives.text += task.GetObjective() + "\n";
        }
    }

    //add task to task list in HUD
    private void ShowCurrentTask(int index)
    {
        txtObjectives.text += currentTasks[index].GetObjective() + "\n"; //append task to task list in the HUD

        if (asAnnouncer != null)
        {
            //get task sound
            var path = "Sound/bookFlip1";
            var sound = Resources.Load<AudioClip>(path);

            //play task added sound
            if (sound != null)
            {
                asAnnouncer.clip = sound;
                asAnnouncer.Play();
            }
        }
    }
}
