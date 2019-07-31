using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    void TriggerFly()
    {
        MainCharacterController controller = GameObject.Find("MainCharacter").GetComponent<MainCharacterController>();
        controller.Fly();
    }
}
