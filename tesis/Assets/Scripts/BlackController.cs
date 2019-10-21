using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        FadeOut();
    }

    void FadeFinish()
    {
        GameEvents.instance.FadeFinish();
    }

    public void FadeOut()
    {
        animator.SetTrigger(GameConstants.ANIMATION_BLACK_FADE_OUT);
    }

    public void FadeIn()
    {
        animator.SetTrigger(GameConstants.ANIMATION_BLACK_FADE_IN);
    }
}
