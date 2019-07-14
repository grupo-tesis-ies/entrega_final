using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public void Fly()
    {
        GetComponent<Animator>().SetTrigger("fly");
    }

    public void MoveFloor()
    {
        GameObject.Find("Floor").GetComponent<FloorController>().MoveDown();
    }
}
