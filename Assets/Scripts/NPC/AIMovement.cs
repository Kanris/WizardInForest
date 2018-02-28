using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AIMovement : MonoBehaviour {

    private Rigidbody2D aiBody;
    private Animator animation;

    private Transform currentPatrolPoint;
    public int currentPatrolIndex = 1;

    private bool isWaiting = false;
    private bool isPlayerNearBy = false;

    private delegate IEnumerator PTrigger();

    public Transform[] patrolPoints;
    public float speed = 0.1f;

    public TextMesh[] text = new TextMesh[2];
    private string[] phrases = { "what a day what \n a lovely day!", "Interesting am I always \n walking between \n two points?" };//, string.Empty };
    private string[] phrasesToPlayer = { "Oh, do you want to get through?\n Well, you'll have to wait ...", "...", "I'm stopping you? \n What a pity...", "Thank you for reminding me \n to eat mushrooms." };

    [SerializeField]
    private float animX = 0f, animY = 0f;

    // Use this for initialization
    void Start () {
        aiBody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();

        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    private Vector2 AnimateNPC()
    {
        var movementVector = Vector2.MoveTowards(new Vector2(aiBody.transform.position.x, aiBody.transform.position.y),
    new Vector2(currentPatrolPoint.position.x, currentPatrolPoint.position.y), speed * Time.deltaTime); //where to move

        animation.SetBool("isWalking", true); //is npc walking
        animation.SetFloat("posX", animX); //where he need to look in x
        animation.SetFloat("posY", animY); //where he need to look in y

        return movementVector;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isWaiting && !isPlayerNearBy) //if npc is not waiting on patrol point and player is not nearby
        {
            var movementVector = AnimateNPC(); //animate pc movement

            aiBody.transform.position = movementVector; //move npc
            //Debug.Log(currentPatrolIndex + "|" + currentPatrolPoint.position.x + "||" + currentPatrolPoint.position.y + "|" + aiBody.transform.position.x + "||" +aiBody.transform.position.y);
        }
        else //if npc is waiting on patrol point or player is nearby
        {
            animation.SetBool("isWalking", false); //disable walking animation
        }

    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        //if npc is near patrol point or near player
        if (collision.isTrigger && !isPlayerNearBy)
        {
            switch (collision.tag)
            {
                case "Patrol": //is patrol point
                    yield return TriggerLogic(phrases, PatrolTrigger);
                break;

                case "Player": //is player
                    yield return TriggerLogic(phrasesToPlayer, PlayerNearby);
                break;
            }
        }
    }
    //what to do when trigger triggered xD
    private IEnumerator TriggerLogic(string[] phrases, PTrigger pTrigger)
    {
        int randPhraseIndex = Random.Range(0, phrases.Length); //get random phrase index

        if (text != null) text[0].text = text[1].text = phrases[randPhraseIndex]; //display phrase

        yield return pTrigger(); //wait

        if (!isPlayerNearBy & text != null) text[0].text = text[1].text = ""; //clear text if player is not nearby
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //if player is not near the NPC
        {
            isPlayerNearBy = false; //allow to move
            
            yield return new WaitForSeconds(3); //wait for 3 seconds
        }
    }

    //start patrol
    private IEnumerator PatrolTrigger()
    {
        isWaiting = true; //waiting trigger

        //animation.SetFloat("posY", currentPatrolIndex == 0 ? 1 : -1); //where NPC has to look
        GetNextPatrolPoint(); //get next point where to walk

        animX *= -1;
        animY *= -1;

        yield return new WaitForSeconds(GetRandomTime(2, 15)); //stay for 

        isWaiting = false; //disable waiting trigger 
    }

    private float GetRandomTime(int min, int max)
    {
        return Random.Range(min, max);
    }

    //get next point where to walk
    private void GetNextPatrolPoint()
    {
        currentPatrolIndex++;

        if (currentPatrolIndex == patrolPoints.Length) currentPatrolIndex = 0;

        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    private IEnumerator PlayerNearby()
    {
        isPlayerNearBy = true;

        while (isPlayerNearBy)
        {
            yield return null;
        }
    }
}
