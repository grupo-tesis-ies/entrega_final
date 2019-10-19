using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    public GameEvents gameEvents;

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void FadeFinish()
    {
        gameEvents.FadeFinish();
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
