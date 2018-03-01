using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolTalk : MonoBehaviour
{

    [SerializeField]
    private TextMesh[] text = new TextMesh[2];

    [SerializeField]
    private Phrases phrases;

    public delegate IEnumerator PTrigger();

    //what to do when trigger triggered xD
    public IEnumerator Talk(bool isPlayer, PTrigger pTrigger)
    {
        var phrases = isPlayer ? this.phrases.phrasesToPlayer : this.phrases.phrasesPatrol;

        text[0].text = text[1].text = GetPhrase(phrases);

        yield return pTrigger(); //wait

        ClearText();
    }

    private string GetPhrase(string[] phrases)
    {
        int randPhraseIndex = Random.Range(0, phrases.Length); //get random phrase index

        if (text != null) return phrases[randPhraseIndex]; //display phrase

        return string.Empty;
    }

    private void ClearText()
    {
        if (!GetComponent<AIMovement>().isPlayerNearBy & text != null)
            text[0].text = text[1].text = ""; //clear text if player is not nearby
    }
}
