using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject menuCanvas;
    public GameObject configCanvas;
    public AudioClip buttonSound;
    
    
    public void StartGame () {
        GameEvents.instance.StartGame ();
        menuCanvas.SetActive (false);
    }

    public void GoBackToMenu () {
        Time.timeScale = 1;
        SceneManager.LoadScene (GameConstants.SCENE_MENU);
    }

    public void Pause () {
        Time.timeScale = 0;
        //pausePanel.SetActive (true);
    }

    public void UnPause () {
        //pausePanel.SetActive (false);
        Time.timeScale = 1;
    }

    public void SwitchToConfig () {
        menuCanvas.SetActive (false);
        configCanvas.SetActive (true);
    }

    public void SwitchToMenu () {
        configCanvas.SetActive (false);
        menuCanvas.SetActive (true);
    }

    public void PlayButtonSound() {
        SoundManager.instance.PlaySingle(buttonSound);
    }
}