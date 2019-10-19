using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator titleAnimator;
    public Animator cameraAnimator;
    public GameObject menuCanvas;
    public GameObject blackImage;
    public MainCharacterController mainCharacter;
    public GameObject pausePanel;
    public GameEvents gameEvents;

    public GameObject configCanvas;

    private void Start()
    {
        blackImage.GetComponent<BlackController>().FadeOut();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            gameEvents.StartStory();
        }
    }

    public void StartMenu()
    {
        Invoke("SetBlackInactive", 2f);
        if (SceneManager.GetActiveScene().name != "Game")
        {
            titleAnimator.SetTrigger("titleDisplay");
        }
    }

    void SetBlackInactive()
    {
        blackImage.SetActive(false);
    }

    public void StartGame()
    {
        menuCanvas.SetActive(false);
        cameraAnimator.SetTrigger("start");
        blackImage.SetActive(true);
    }

    public void GoBackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UnPause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SwitchToConfig()
    {
        menuCanvas.SetActive(false);
        configCanvas.SetActive(true);
    }

    public void SwitchToMenu()
    {
        configCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
}
