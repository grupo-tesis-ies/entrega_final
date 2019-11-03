using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour {
    public GameObject pausePanel;

    public void Pause () {
        Time.timeScale = 0;
        pausePanel.SetActive (true);
    }

    public void UnPause () {
        pausePanel.SetActive (false);
        Time.timeScale = 1;
    }

    public void GoBackToMenu () {
        Time.timeScale = 1;
        SceneManager.LoadScene (GameConstants.SCENE_MENU);
    }
}