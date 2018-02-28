using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private SpriteRenderer npcDialogueImage;

    private Queue<string> sentences;
    private bool isDialogueInProcess = false;
    private Image playerButton;
    private TextMesh[] textMesh = new TextMesh[2];

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
        playerButton = GameObject.FindGameObjectWithTag("Player_Buttons").GetComponent<Image>();
    }

    private void Update()
    {
        if (isDialogueInProcess)
        {
            ShowPlayerInteractionButton("button_Space");
        }

        if (isDialogueInProcess && Input.GetKeyDown("space"))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue, TextMesh[] textMesh, SpriteRenderer npcDialogueImage)
    {
        sentences.Clear();
        this.npcDialogueImage = npcDialogueImage;
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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        textMesh[0].text = textMesh[1].text = string.Empty;

        foreach (var letter in sentence.ToCharArray())
        {
            if (isDialogueInProcess)
            {
                textMesh[0].text = textMesh[1].text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void EndDialogue(TextMesh[] textMesh)
    {
        if (isDialogueInProcess) textMesh[0].text = textMesh[1].text = string.Empty;
        if (npcDialogueImage != null) npcDialogueImage.enabled = true;

        isDialogueInProcess = false;
        playerButton.enabled = false;

        ShowPlayerInteractionButton("button_E");
    }

    private void ShowPlayerInteractionButton(string button)
    {
        playerButton.enabled = true;
        var path = "Button/" + button;
        var image = Resources.Load<Sprite>(path);

        playerButton.sprite = image;
    }

}
