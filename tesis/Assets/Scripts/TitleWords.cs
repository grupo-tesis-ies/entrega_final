using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWords : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void TriggerWords()
    {
        animator.SetTrigger(GameConstants.ANIMATION_TITLE_WORDS_DISPLAY);
    }
}
