using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task {

    private int id;

    [TextArea(3, 10)]
    private string objective;

    private List<string> log;

    public Task(int id, string objective)
    {
        this.id = id;
        this.objective = objective;

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
        log.Add(this.objective);

        this.objective = objective;
    }

    public string GetObjective()
    {
        return objective;
    }

    public List<string> GetLog()
    {
        return log;
    }
}
