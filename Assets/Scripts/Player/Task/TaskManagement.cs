using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManagement : MonoBehaviour {

    private Text txtAnnouncer;
    private Text txtObjectives;

    public List<Task> currentTasks = new List<Task>();
    public List<Task> completedTasks = new List<Task>();

    private AudioSource asAnnouncer;

    // Use this for initialization
    void Start () {
        txtObjectives = GameObject.FindGameObjectWithTag("HUD_Objectives").GetComponent<Text>();
        txtAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>();
        asAnnouncer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>();
    }

    public IEnumerator AddTask(int id, string task)
    {
        var indexInCompletedTask = IsTaskAdded(completedTasks, id, task);

        if (indexInCompletedTask == -1)
        {
            var index = IsTaskAdded(currentTasks, id, task);
            var announcerMessage = string.Empty;

            Debug.Log("Index:" + index);

            if (index >= 0)
            {
                announcerMessage = "Task" + currentTasks[index].GetObjective() + "updated!";
                currentTasks[index].UpdateObjective(task);

                ShowCurrentTasks();
            }
            else if (index == -1)
            {
                var newTask = new Task(id, task);
                currentTasks.Add(newTask);

                announcerMessage = "Task added!";
                ShowCurrentTask(id);
            }

            txtAnnouncer.text = announcerMessage;

            yield return new WaitForSeconds(5f);

            txtAnnouncer.text = string.Empty;
        }
    }

    public int IsTaskAdded(List<Task> list, int id, string task = "")
    {
        int index = 0;

        foreach (var item in list)
        {
            if (item.GetID() == id)
            {
                if (string.IsNullOrEmpty(task))
                {
                    return index;
                }
                else
                {
                    if (item.GetObjective() != task)
                    {
                        if (!item.GetLog().Contains(task))
                        {
                            return index;
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -2;
                    }
                }
            }

            index++;
        }

        return -1;
    }

    public IEnumerator TaskComplete(int id)
    {
        var completedTaskIndex = IsTaskAdded(currentTasks, id);

        Debug.Log("Complete index:" + completedTaskIndex);

        if (completedTaskIndex >= 0)
        {
            var completedTask = currentTasks[completedTaskIndex];

            currentTasks.RemoveAt(completedTaskIndex);
            completedTasks.Add(completedTask);

            txtAnnouncer.text = "Task <" + completedTask.GetObjective() + ">\n complete!";
            ShowCurrentTasks();

            yield return new WaitForSeconds(5f);

            txtAnnouncer.text = string.Empty;
        }
    }

    private void ShowCurrentTasks()
    {
        Debug.Log(currentTasks.Count);

        txtObjectives.text = string.Empty;

        foreach (var task in currentTasks)
        {
            txtObjectives.text += task.GetObjective() + "\n";
        }
    }

    private void ShowCurrentTask(int index)
    {
        txtObjectives.text += currentTasks[index].GetObjective() + "\n";

        if (asAnnouncer != null)
        {
            var path = "Sound/bookFlip1";
            var sound = Resources.Load<AudioClip>(path);

            if (sound != null)
            {
                asAnnouncer.clip = sound;
                asAnnouncer.Play();
            }
        }
    }
}
