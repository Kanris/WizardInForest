using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    private Rigidbody2D aiBody; //npc body
    private Animator aiAnimator; //npc animation

    private bool isWaiting = false; //is npc stoped
    private bool isMoveTimeIsOver = false; //is npc has to stop
    private Vector2 whereToMove = Vector2.zero; //where to move

    public bool isAttacking = false;
    public bool isNearPlayer = false;

    private float speed;

	// Use this for initialization
	void Start () {
        aiBody = GetComponent<Rigidbody2D>(); //get npc body
        aiAnimator = GetComponent<Animator>(); //get npc animation

        whereToMove = GetNextRandomPoint(); //get where to move
        StartCoroutine(Wait()); //start move logic

        speed = gameObject.transform.GetChild(0).GetComponent<AIStats>().speed;
    }
	
	// Update is called once per frame
	void Update () {

        //if enemy attacks player
        if (isAttacking)
        {
            if (isNearPlayer) //if enemy is near player
            {
                whereToMove = aiBody.transform.position; //stop moving
            }
            else //if player is not near the enemy
            {
                var player = GameObject.FindGameObjectWithTag("Player"); //find player
                if (player != null) //if player still alive
                {
                    whereToMove = player.transform.position; //get player position
                }
            }

            var movementVector = WhereToMove(whereToMove); //where to move
            aiBody.MovePosition(movementVector); //move enemy
           
            SetAnimation(movementVector.x, movementVector.y); //set animation
        }

        if (!isWaiting && !isAttacking) //if npc is not waiting
        {
            var movementVector = WhereToMove(whereToMove); //get next movement position

            if (isMoveTimeIsOver) //if time for move is over
            {
                whereToMove = GetNextRandomPoint(); //get next move point
                StartCoroutine(Wait()); //wait

            } else
            {
                aiBody.transform.position = movementVector; //move npc
            }
        }

	}
    
    private Vector2 WhereToMove(Vector2 point)//get where to move position
    {
        var movementVector = Vector2.MoveTowards(new Vector2(aiBody.transform.position.x, aiBody.transform.position.y),
    point, speed * Time.deltaTime); 

        return movementVector;

    }

    //get next movement position
    private Vector2 GetNextRandomPoint()
    {
        //get random point
        var posX = GetRandomValue();
        var posY = GetRandomValue();

        //activate animation
        SetAnimation(posX, posY);

        //next npc position
        posX = aiBody.transform.position.x + posX;
        posY = aiBody.transform.position.y + posY;

        return new Vector2(posX, posY);
    }

    //activate npc animation
    private void SetAnimation(float posX, float posY)
    {
        var x = GetAnimation(Mathf.Floor(posX));
        var y = GetAnimation(Mathf.Floor(posY));

        aiAnimator.SetFloat("posX", x);
        aiAnimator.SetFloat("posY", y);
    }

    //get value for animation
    private float GetAnimation(float pos)
    {
        var value = 0f;

        if (pos > 0) value = 1f;
        else if (pos == 0) value = 0f;
        else if (pos < 0) value = -1f;

        return value;
    }

    //stop npc movement
    private IEnumerator Wait(float minWaitTime = 4f, float maxWaitTime = 15f)
    {
        isWaiting = true; //stop movement

        var randTime = GetRandomValue(minWaitTime, maxWaitTime); //get random wait time

        yield return new WaitForSeconds(randTime); //wait

        isWaiting = false; //continue movement

        yield return Move(); //start timer for movement
    }

    //timer for movement
    private IEnumerator Move()
    {
        isMoveTimeIsOver = false; //npc can move

        yield return new WaitForSeconds(5f);

        isMoveTimeIsOver = true; //npc has to stop
    }

    //get random value in range
    private float GetRandomValue(float min = -1f, float max = 1f)
    {
        var rand = Random.Range(min, max);

        return rand;
    }
}
