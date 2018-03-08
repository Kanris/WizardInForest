using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolTalk : MonoBehaviour
{
    private TextMesh[] soliderAnswer;

    [SerializeField]
    private Phrases phrases;

    public delegate IEnumerator PTrigger();

    private void Start()
    {
        InitializeSoliderAnswerTM();
    }

    private void InitializeSoliderAnswerTM()
    {
        soliderAnswer = new TextMesh[gameObject.transform.childCount];

        for (int index = 0; index < soliderAnswer.Length; index++)
        {
            soliderAnswer[index] = gameObject.transform.GetChild(index).GetComponent<TextMesh>();
        }
    }

    //what to do when trigger triggered xD
    public IEnumerator Talk(bool isPlayer, PTrigger pTrigger)
    {
        var phrases = isPlayer ? this.phrases.phrasesToPlayer : this.phrases.phrasesPatrol;

        soliderAnswer[0].text = soliderAnswer[1].text = GetPhrase(phrases);

        yield return pTrigger(); //wait

        ClearText();
    }

    private string GetPhrase(string[] phrases)
    {
        int randPhraseIndex = Random.Range(0, phrases.Length); //get random phrase index

        if (soliderAnswer != null) return phrases[randPhraseIndex]; //display phrase

        return string.Empty;
    }

    private void ClearText()
    {
        if (!GetComponent<AIMovement>().isPlayerNearBy & soliderAnswer != null)
            soliderAnswer[0].text = soliderAnswer[1].text = ""; //clear text if player is not nearby
    }
}
