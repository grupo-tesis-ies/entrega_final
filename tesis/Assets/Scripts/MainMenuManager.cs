using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public GameObject menuCanvas;
    public GameObject configCanvas;
    public AudioClip buttonSound;

    public Sprite soundEfxOn;
    public Sprite soundEfxOff;

    public void StartGame () {
        GameEvents.instance.StartGame ();
        menuCanvas.SetActive (false);
    }
    
    public void SwitchToConfig () {
        menuCanvas.SetActive (false);
        configCanvas.SetActive (true);
    }

    public void SwitchToMenu () {
        configCanvas.SetActive (false);
        menuCanvas.SetActive (true);
    }

    public void PlayButtonSound () {
        SoundManager.instance.PlaySingle (buttonSound);
    }

    public void TriggerMusic (Image buttonImg) {
        if (SoundManager.instance.TriggerMusic ()) {
            buttonImg.sprite = soundEfxOn;
        } else {
            buttonImg.sprite = soundEfxOff;
        }
    }
}