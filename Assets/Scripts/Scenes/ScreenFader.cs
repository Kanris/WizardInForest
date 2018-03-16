using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

    private Animator animator; //hud animator
    public static bool isFading = false; //is animation in progress

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>(); //get hud animator

    }

    //fade from black to clear
    public IEnumerator FadeToClear()
    {
        isFading = true; //animation is in progress

        animator.SetTrigger("fadeIn"); //start fadeIn animation

        //wait until animation is complete
        while (isFading)
        {
            yield return null;
        }
    }

    //fade from clear to black
    public IEnumerator FadeToBlack()
    {
        isFading = true; //animation is in progress

        animator.SetTrigger("fadeOut"); //start fadeOut animation

        //wait until animation is complete
        while (isFading)
        {
            yield return null;
        }
    }
    
    //animation complete
    public void AnimationComplete()
    {
        isFading = false; //stop animation
    }
}
