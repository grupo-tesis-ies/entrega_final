using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    private bool isMoving = false;
    public float movingSpeed = 2f;
    public BlackController blackController;
    private float backgroundSpeed;
    private bool isShieldUp = false;

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
        backgroundSpeed = 0.7f;
        GameObject.Find("Background").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(0, backgroundSpeed, 0.5f));
        GameObject.Find("Background Offset").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(0, backgroundSpeed, 0.5f));
    }

    void MoveCamera()
    {
        GetComponent<SwipeMove>().enabled = true;
    }

    void StartGame()
    {
        blackController.FadeIn();
        Invoke("ChangeLevel", 1.5f);
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle" && !isShieldUp)
        {
            GetComponent<Animator>().SetTrigger("hit");
            if (GameObject.Find("Score Manager").GetComponent<ScoreManager>().HasCoins())
            {
                GameObject.Find("Bird Particles").GetComponent<ParticleSystem>().Play();
            }
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().Hit();
        }
    }

    public void SetMoving(bool isMoving)
    {
        isMoving = false;
    }

    public void SetPowerUp(string powerUp)
    {
        GameObject.Find("Bird").GetComponent<Animator>().SetTrigger(powerUp);
        if(powerUp == "shieldUp")
        {
            isShieldUp = true;
        }
        if (powerUp == "impulseUp")
        {
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().TurnOnDoubleMeters();
            SetBackgroundSpeed(1.4f);
        }
        if (powerUp == "x2Up")
        {
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().TurnOnDoubleCoins();
        }
    }

    public void SetPowerUpOff(string powerUp)
    {
        if (powerUp == "shield")
        {
            isShieldUp = false;
        }
        if (powerUp == "impulse")
        {
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().TurnOffDoubleMeters();
            SetBackgroundSpeed(0.7f);
        }
        if (powerUp == "x2")
        {
            GameObject.Find("Score Manager").GetComponent<ScoreManager>().TurnOffDoubleCoins();
        }
    }

    public void SetBackgroundSpeed(float speed)
    {
        GameObject.Find("Background").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(backgroundSpeed, speed, 0.5f));
        GameObject.Find("Background Offset").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(backgroundSpeed, speed, 0.5f));
        backgroundSpeed = speed;
    }
}
