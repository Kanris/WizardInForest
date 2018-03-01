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
    public bool isPlayerNearBy = false;

    public Transform[] patrolPoints;
    public float speed = 0.1f;

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
        if (collision.isTrigger && !isPlayerNearBy)
        {
            switch (collision.tag)
            {
                case "Patrol": //is patrol point
                    yield return GetComponent<AIPatrolTalk>().Talk(false, PatrolTrigger);
                    break;

                case "Player": //is player
                    yield return GetComponent<AIPatrolTalk>().Talk(true, PlayerNearby);
                    break;
            }
        }
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
