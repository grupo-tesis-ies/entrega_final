using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();    
    }

    void TriggerZoomOffFinish()
    {
        GameEvents.instance.ZoomOffFinish();
    }

    public void TriggerZoomOut()
    {
        animator.SetTrigger(GameConstants.ANIMATION_ZOOM_OUT);
    }
}
