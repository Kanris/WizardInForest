using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task {

    private int id; //task id
    private string task; //current task
    private List<string> sentences;
    private List<string> log; //task log

    public Task(int id, string objective, string name, string[] sentence)
    {
        this.id = id; //add task id
        this.task = objective; //add current task objective

        this.log = new List<string>(); 
        this.log.Add(objective); //add task to the log

        this.sentences = new List<string>();
        this.sentences.Add(name + ":");
        foreach (var item in sentence)
            this.sentences.Add(item); //add dialogue sentence to the log

        this.sentences.Add("------"); //add seperator
    }

    public void UpdateID(int id)
    {
        this.id = id;
    }

    public int GetID()
    {
        return id;
    }

    public void UpdateObjective(string objective, string name, string[] sentence)
    {
        log.Add(objective);

        this.sentences.Add(name + ":");

        foreach (var item in sentence)
            this.sentences.Add(item);

        this.sentences.Add("------");

        this.task = objective;
    }

    public string GetObjective()
    {
        return task;
    }

    public List<string> GetLog()
    {
        return log;
    }

    public List<string> GetSentences()
    {
        return sentences;
    }
}
