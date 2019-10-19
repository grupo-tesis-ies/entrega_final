using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameEvents gameEvents;

    void TriggerZoomOffFinish()
    {
        gameEvents.ZoomOffFinish();
    }
}
