using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

    private Animator animator;
    private bool isFading = false;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();

	}

    public IEnumerator FadeToClear()
    {
        isFading = true;

        animator.SetTrigger("fadeIn");

        while (isFading)
        {
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        isFading = true;

        animator.SetTrigger("fadeOut");

        while (isFading)
        {
            yield return null;
        }
    }
    
    public void AnimationComplete()
    {
        isFading = false;
    }
}
