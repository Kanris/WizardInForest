using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnswer : MonoBehaviour {

    private TextMesh[] tmPlayerAnswer;

	// Use this for initialization
	void Start () {

        InitializeTMPlayerAnswer();

    }

    private void InitializeTMPlayerAnswer()
    {
        tmPlayerAnswer = new TextMesh[gameObject.transform.childCount];

        for (int index = 0; index < tmPlayerAnswer.Length; index++)
        {
            tmPlayerAnswer[index] = gameObject.transform.GetChild(index).GetComponent<TextMesh>();
        }
    }

    public void DisplayPlayerAnswer(string sentence)
    {
        tmPlayerAnswer[0].text = tmPlayerAnswer[1].text = sentence;
    }

    public void HidePlayerAnswer()
    {
        tmPlayerAnswer[0].text = tmPlayerAnswer[1].text = string.Empty;
    }

}
