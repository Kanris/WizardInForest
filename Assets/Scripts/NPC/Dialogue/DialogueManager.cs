using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Queue<string> sentences;
    private bool isDialogueInProcess = false;
    private Image playerButton;
    private TextMesh textMesh;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();

        playerButton = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>();
    }

    private void Update()
    {
        if (isDialogueInProcess)
        {
            playerButton.enabled = true;
            var path = "Button/button_Space";
            var image = Resources.Load<Sprite>(path);

            playerButton.sprite = image;
        }

        if (isDialogueInProcess && Input.GetKeyDown("space"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, TextMesh textMesh)
    {
        Debug.Log("Starting conversation with + " + dialogue.name);

        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        this.textMesh = textMesh;

        isDialogueInProcess = true;
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(textMesh);
            return;
        }
        
        var sentence = sentences.Dequeue();
        //this.textMesh.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        textMesh.text = "";

        foreach (var letter in sentence.ToCharArray())
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue(TextMesh textMesh)
    {
        isDialogueInProcess = false;
        playerButton.enabled = false;

        textMesh.text = string.Empty;
    }

}
