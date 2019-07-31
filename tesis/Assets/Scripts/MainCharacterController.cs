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
        GameObject.Find("Background").GetComponent<ScrollerController>().SetSpeed(1f);
        GameObject.Find("Background Offset").GetComponent<ScrollerController>().SetSpeed(1f);
        Invoke("MoveCamera", 3f);
        Invoke("StartGame", 4f);
    }

    void MoveCamera()
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().orthographic = true;
        GetComponent<SwipeMove>().enabled = true;
    }

    void StartGame()
    {
        GameObject.Find("Obstacles Generator").GetComponent<ObstaclesGenerator>().StartPlaying();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }
}
