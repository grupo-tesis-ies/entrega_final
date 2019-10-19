using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Player
    public GameObject mainCharacter;

    // Background
    private float backgroundSpeed;
    private const float BACKGROUND_SPEED_TRANSITION = 0.5f;
    private const float BACKGROUND_IDLE_SPEED = 0f;
    private const int STOP_CHARACTER_TIME = 2;

    public void StartStory()
    {
        mainCharacter.GetComponent<Animator>().SetTrigger("fly");
        StartMovingFloor();
        Invoke("MoveCharacter", 2.5f);
    }

    public void SetBackgroundSpeed(float backgroundSpeed)
    {
        this.backgroundSpeed = backgroundSpeed;
    }

    void StartMovingFloor()
    {
        //backgroundSpeed = 0.7f;
        GameObject.Find("Background").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(BACKGROUND_IDLE_SPEED, backgroundSpeed, BACKGROUND_SPEED_TRANSITION));
        GameObject.Find("Background Offset").GetComponent<ScrollerController>().SetSpeed(Mathf.Lerp(BACKGROUND_IDLE_SPEED, backgroundSpeed, BACKGROUND_SPEED_TRANSITION));
    }

    void MoveCharacter()
    {
        mainCharacter.GetComponent<MainCharacterController>().SetMoving(true);
        StartCoroutine(StopCharacter(STOP_CHARACTER_TIME));
    }

    IEnumerator StopCharacter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        mainCharacter.GetComponent<MainCharacterController>().SetMoving(false);
    }

    public void FadeFinish()
    {
        MenuManager menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        menuManager.StartMenu();
    }

    public void ZoomOffFinish()
    {
        MainCharacterController controller = GameObject.Find("MainCharacter").GetComponent<MainCharacterController>();
        controller.Fly();
    }

    public void CoinTriggered()
    {
        GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddCoin();
    }
}
