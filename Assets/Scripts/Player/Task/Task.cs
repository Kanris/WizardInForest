using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task {

    private int id; //task id
    private string task; //current task
    private List<string> log; //task log

    public Task(int id, string objective)
    {
        this.id = id;
        this.task = objective;

        this.log = new List<string>();
        this.log.Add(objective);
    }

    public void UpdateID(int id)
    {
        this.id = id;
    }

    public int GetID()
    {
        return id;
    }

    public void UpdateObjective(string objective)
    {
        log.Add(objective);

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
}
