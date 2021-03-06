﻿using System.Collections;
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

        SetDefaulAnimation();
    }

	// Update is called once per frame
	void Update () {

        if (!WarpToScene.isWarping && !ScreenFader.isFading) //if player not warping
        {
            if (!isPlayerDead && !PauseMenu.isGamePaused) //if playes is not dead and game is not paused
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

                FootStepsSound();
                rBody.MovePosition(rBody.position + movmentVector * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        } else
        {
            anim.SetBool("isWalking", false);
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

    private void SetDefaulAnimation()
    {
        anim.SetFloat("inputX", 0);
        anim.SetFloat("inputY", -1);
        anim.SetBool("isWalking", false);
    }
}
