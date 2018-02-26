using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rBody;
    private Animator anim;
    private AudioSource audio;

    public bool isPlayerDead = false;
	// Use this for initialization
	void Start () {

        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!isPlayerDead)
        {
            var movmentVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movmentVector != Vector2.zero)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("inputX", movmentVector.x);
                anim.SetFloat("inputY", movmentVector.y);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            //Debug.Log(movmentVector.x + "||" + movmentVector.y);

            FootStepsSound();
            rBody.MovePosition(rBody.position + movmentVector * Time.deltaTime);
        }


	}

    void FootStepsSound()
    {
        bool isWalking = anim.GetBool("isWalking");

        if (isWalking && !audio.isPlaying)
        {
            audio.Play();
        }
        else if (!isWalking && audio.isPlaying)
        {
            audio.Stop();
        }
    }
}
