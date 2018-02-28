using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManagement : MonoBehaviour {

    private Text announcer;
    private Text objectives;

    public List<string> currentTasks = new List<string>();
    public List<string> completedTasks = new List<string>();

    private AudioSource announcerAudio;

    // Use this for initialization
    void Start () {
        objectives = GameObject.FindGameObjectWithTag("HUD_Objectives").GetComponent<Text>();
        announcer = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<Text>(); //Helper.GetObject<Text>("Announcer");	
        announcerAudio = GameObject.FindGameObjectWithTag("HUD_Announcer").GetComponent<AudioSource>(); //Helper.GetObject<AudioSource>("Announcer");
    }

    public IEnumerator AddTask(string task)
    {
        currentTasks.Add(task);
        ShowCurrentTask(task);

        announcer.text = "Task added!";

        yield return new WaitForSeconds(5f);

        announcer.text = string.Empty;
    }

    public IEnumerator TaskComplete(string task)
    {
        completedTasks.Add(task);
        currentTasks.Remove(task);

        task = task.Remove(0, 1);

        announcer.text = "Task <" + task + ">\n complete!";
        ShowCurrentTasks();

        yield return new WaitForSeconds(5f);

        announcer.text = string.Empty;
    }

    private void ShowCurrentTasks()
    {
        objectives.text = string.Empty;

        foreach (var task in currentTasks)
        {
            objectives.text += task + "\n";
        }
    }

    private void ShowCurrentTask(string task)
    {
        objectives.text += task + "\n";

        if (announcerAudio != null)
        {
            var path = "Sound/bookFlip1";
            var sound = Resources.Load<AudioClip>(path);

            if (sound != null)
            {
                announcerAudio.clip = sound;
                announcerAudio.Play();
            }
        }
    }
}
