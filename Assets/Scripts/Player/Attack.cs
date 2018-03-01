using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private Vector3 whereToMove;
    private GameObject cloneFireball = null;
    public GameObject fireball;
    private Rigidbody2D fireballBody;

    private bool isAttacking = false;
    public bool isHit = false;
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            if (!FindObjectOfType<DialogueManager>().isDialogueInProcess)
            {
                isHit = false;

                cloneFireball = Instantiate(fireball);
                fireballBody = cloneFireball.GetComponent<Rigidbody2D>();

                whereToMove = Animate(cloneFireball);
                cloneFireball.transform.position = new Vector3(transform.position.x,
                    transform.position.y, 0);

                isAttacking = true;
            }
        }

        if (isAttacking)
        {
            if (!isHit)
            {
                fireballBody.transform.position += whereToMove * Time.deltaTime;  
            }
            else
            {
                isAttacking = false;
            }
        }

	}

    /*
     *  
        var animator = cloneFireball.GetComponent<Animator>();
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(2.5f);

        isHit = true;
        */

    private Vector3 Animate(GameObject fireball)
    {
        var animator = GetComponent<Animator>();

        var x = animator.GetFloat("inputX");
        var y = animator.GetFloat("inputY");

        var fireballAnimator = fireball.GetComponent<Animator>();

        fireballAnimator.SetFloat("posX", x);
        fireballAnimator.SetFloat("posY", y);
        fireballAnimator.SetBool("isHit", false);

        return new Vector3(x, y, 0);
    }
}
