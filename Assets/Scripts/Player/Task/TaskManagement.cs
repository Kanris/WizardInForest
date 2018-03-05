using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManagement : MonoBehaviour {

    private Text txtAnnouncer; //hud announcer
    private Text txtObjectives; //hud objectives

    public List<Task> currentTasks = new List<Task>(); //list of current task
    public List<Task> completedTasks = new List<Task>(); //list of completed task

    private AudioSource asAnnouncer; //task sound

    // Use this for initialization
    void Start () {
        txtObjectives = GameObject.FindGameObjectWithTag("HUD_Objectives").GetComponent<Text>(); //get hud objectvies
        txtAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //get hud announcer
        asAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>(); //get hud audio source
    }

    //add/update task
    public IEnumerator AddUpdateTask(TaskInfo taskInfo)
    {
        var indexInCompletedTask = IsTaskAdded(completedTasks, taskInfo.taskID, taskInfo.task); //search task in completed task list

        if (indexInCompletedTask == -1) //task is not in completed task
        {
            var index = IsTaskAdded(currentTasks, taskInfo.taskID, taskInfo.task); //search task in current task list
            var announcerMessage = string.Empty; //announcer message

            if (index >= 0) //if task is already in the current task list
            {
                announcerMessage = "Task <" + currentTasks[index].GetObjective() + ">updated!"; //form update announcer message
                currentTasks[index].UpdateObjective(taskInfo.task); //updated existed task

                ShowCurrentTasks(); //updated current task list in the HUD
            }
            else if (index == -1 && !taskInfo.isTaskUpdate) //if task is not in the current task list
            {
                var newTask = new Task(taskInfo.taskID, taskInfo.task); //form new task
                currentTasks.Add(newTask); //add new task to the current list task

                announcerMessage = "Task added!"; //form add announcer message
                ShowCurrentTask(currentTasks.Count - 1); //update current task list in the HUD
            }

            yield return ShowAnnouncerMessage(announcerMessage); //show announcer message for player
        }
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

            yield return ShowAnnouncerMessage(announcerMessage); //show announcer message
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
