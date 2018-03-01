using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolTalk : MonoBehaviour {

    [SerializeField]
    private TextMesh[] text = new TextMesh[2];
    [SerializeField]
    private string[] phrasesPatrol = { "What a day what \n a lovely day!", "Interesting am I always \n walking between \n two points?" };//, string.Empty };
    [SerializeField]
    private string[] phrasesToPlayer = { "Oh, do you want to get through?\n Well, you'll have to wait ...", "...", "I'm stopping you? \n What a pity...", "Thank you for reminding me \n to eat mushrooms." };

    public delegate IEnumerator PTrigger();

    //what to do when trigger triggered xD
    public IEnumerator Talk(bool isPlayer, PTrigger pTrigger)
    {
        var phrases = isPlayer ? phrasesToPlayer : phrasesPatrol;

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
