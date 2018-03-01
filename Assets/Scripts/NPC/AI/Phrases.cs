using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phrases {

    [TextArea(2, 10)]
    public string[] phrasesPatrol = { "What a day what \n a lovely day!", "Interesting am I always \n walking between \n two points?", string.Empty };

    [TextArea(2, 10)]
    public string[] phrasesToPlayer = { "Oh, do you want to get through?\n Well, you'll have to wait ...", "...", "I'm stopping you? \n What a pity...", "Thank you for reminding me \n to eat mushrooms." };

}
