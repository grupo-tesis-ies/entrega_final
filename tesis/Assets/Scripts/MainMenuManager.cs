using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public GameObject menuCanvas;
    public GameObject configCanvas;
    public GameObject modeCanvas;
    public GameObject titleParent;
    public GameObject difficultyCanvas;
    public AudioClip buttonSound;

    public Sprite soundEfxOn;
    public Sprite soundEfxOff;

    public Button endlessButton;

    private bool hasFinishedHistory;

    public void SelectGameMode () {
        if (hasFinishedHistory) {
            SwitchToSelectMode ();
        } else {
            StartGame ();
        }
    }

    public void SwitchToSelectMode () {
        menuCanvas.SetActive (false);
        difficultyCanvas.SetActive (false);
        modeCanvas.SetActive (true);
    }

    public void SwitchToDifficulty () {
        modeCanvas.SetActive (false);
        difficultyCanvas.SetActive (true);
    }

    public void StartGame () {
        GameEvents.instance.StartGame (true, true);
        menuCanvas.SetActive (false);
        difficultyCanvas.SetActive (false);
        modeCanvas.SetActive (false);
        titleParent.SetActive (false);
    }

    public void SwitchToConfig () {
        menuCanvas.SetActive (false);
        titleParent.SetActive (false);
        configCanvas.SetActive (true);
    }

    public void SwitchToMenu () {
        modeCanvas.SetActive (false);
        configCanvas.SetActive (false);
        menuCanvas.SetActive (true);
        titleParent.SetActive (true);
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

    public void TriggeredWords () {
        menuCanvas.SetActive (true);
    }

    public void SetHistoryModeFinished (bool hasFinished) {
        this.hasFinishedHistory = hasFinished;
        endlessButton.interactable = hasFinished;
    }

    public void StartEasyMode () {
        GameEvents.instance.StartGame (false, true);
        menuCanvas.SetActive (false);
        difficultyCanvas.SetActive (false);
        modeCanvas.SetActive (false);
        titleParent.SetActive (false);
    }

    public void StartMediumMode () {

    }

    public void StartHardMode () {

    }
}