using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {
    public GameObject pausePanel;

    public void Pause () {
        Time.timeScale = 0;
        OnGameSounds.instance.PlayButton ();
        pausePanel.SetActive (true);
    }

    public void UnPause () {
        pausePanel.SetActive (false);
        OnGameSounds.instance.PlayButton ();
        Time.timeScale = 1;
    }

    public void GoBackToMenu () {
        Time.timeScale = 1;
        OnGameSounds.instance.PlayButton ();
        SceneManager.LoadScene (GameConstants.SCENE_MENU);
    }

    public void Retry () {
        Time.timeScale = 1;
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}