using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBoard : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();    
    }

    public void TriggerTitleBoard()
    {
        animator.SetTrigger(GameConstants.ANIMATION_TITLE_BOARD_DISPLAY);
    }

    public void TriggerWords()
    {
        GameEvents.instance.TriggerTitleWords();
    }
}
