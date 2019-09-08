using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    private bool isMoving = false;
    public float movingSpeed = 2f;
    public BlackController blackController;

    public void Fly()
    {
        GetComponent<Animator>().SetTrigger("fly");
    }

    public void MoveFloor()
    {
        if(SceneManager.GetActiveScene().name != "Game")
        {
            Invoke("StartGame", 1.8f);
            isMoving = true;
        } else
        {
            GameObject.Find("Obstacles Generator").GetComponent<ObstaclesGenerator>().StartPlaying();
            GetComponent<SwipeMove>().enabled = true;
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().StartMetersCounter();
        }
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);
        }
    }

    public void StartMovingFloor()
    {
        GameObject.Find("Background").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(0, 0.7f, 0.5f));
        GameObject.Find("Background Offset").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(0, 0.7f, 0.5f));
    }

    void MoveCamera()
    {
        GetComponent<SwipeMove>().enabled = true;
    }

    void StartGame()
    {
        blackController.FadeIn();
        Invoke("ChangeLevel", 1.5f);
//        GameObject.Find("Obstacles Generator").GetComponent<ObstaclesGenerator>().StartPlaying();
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            GetComponent<Animator>().SetTrigger("hit");
        }
    }

    public void TranslatePigeon()
    {
        isMoving = true;
        Invoke("SetMoving", 1f);
    }

    void SetMoving()
    {
        isMoving = false;
    }
}
