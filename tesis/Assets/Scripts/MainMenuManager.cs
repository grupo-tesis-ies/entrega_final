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
    public GameObject shopFirstCanvas;
    public GameObject shopSecondCanvas;
    public GameObject titleParent;
    public GameObject difficultyCanvas;
    public AudioClip buttonSound;

    public Sprite soundEfxOn;
    public Sprite soundEfxOff;

    public Button endlessButton;
    public Button endlessMediumButton;
    public Button endlessHardButton;

    public Text easyModeText;
    public Text mediumModeText;
    public Text hardModeText;

    public Button mediumModeButton;
    public Button hardModeButton;
    public Sprite purchasedMediumMode;
    public Sprite purchasedHardMode;

    private bool hasFinishedHistory;

    public Text coinScore;
    public Text secondScore;

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
        if(PlayerPrefs.HasKey("medium")) {
            endlessMediumButton.interactable = true;
        } else {
            endlessMediumButton.interactable = false;
        }
        if(PlayerPrefs.HasKey("hard")) {
            endlessHardButton.interactable = true;
        } else {
            endlessHardButton.interactable = false;
        }
    }

    public void StartGame () {
        GameEvents.instance.StartGame ("story");
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
        shopFirstCanvas.SetActive (false);
        shopSecondCanvas.SetActive (false);
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
        GameEvents.instance.StartGame ("easy");
        DisableCanvas ();
    }

    public void StartMediumMode () {
        GameEvents.instance.StartGame ("medium");
        DisableCanvas ();
    }

    public void StartHardMode () {
        GameEvents.instance.StartGame ("hard");
        DisableCanvas ();
    }

    void DisableCanvas () {
        menuCanvas.SetActive (false);
        difficultyCanvas.SetActive (false);
        modeCanvas.SetActive (false);
        titleParent.SetActive (false);
    }

    public void SwitchToShop () {
        menuCanvas.SetActive (false);
        titleParent.SetActive (false);
        shopFirstCanvas.SetActive (true);
        shopSecondCanvas.SetActive (false);
        int coins = PlayerPrefs.GetInt ("coinsCount", 0);
        coinScore.text = coins.ToString ("D4");
    }

    public void SwitchToSecondShop () {
        int coins = PlayerPrefs.GetInt ("coinsCount", 0);
        secondScore.text = coins.ToString ("D4");
        if (PlayerPrefs.HasKey ("medium")) {
            mediumModeButton.interactable = false;
            mediumModeButton.image.sprite = purchasedMediumMode;
            mediumModeText.text = "Adquirido";
        } else {
            if (coins >= 300) {
                mediumModeButton.interactable = true;
                mediumModeText.text = "Presiona el icono para adquirir el modo medio";
            } else {
                mediumModeButton.interactable = false;
                mediumModeText.text = "Te faltan " + (300 - coins) + " monedas para desbloquear";
            }
        }
        if (PlayerPrefs.HasKey ("hard")) {
            hardModeButton.interactable = false;
            hardModeButton.image.sprite = purchasedHardMode;
            hardModeText.text = "Adquirido";
        } else {
            if (PlayerPrefs.HasKey ("medium")) {
                if (coins >= 500) {
                    hardModeButton.interactable = true;
                    hardModeText.text = "Presiona el icono para adquirir el modo dificil";
                } else {
                    hardModeButton.interactable = false;
                    hardModeText.text = "Te faltan " + (500 - coins) + " monedas para desbloquear";
                }
            } else {
                hardModeText.text = "Necesitas adquirir el modo medio antes, para desbloquear";
            }

        }
        if (hasFinishedHistory) {
            easyModeText.text = "Adquirido";
        } else {
            easyModeText.text = "(Finaliza el modo historia para desbloquear)";
        }

        menuCanvas.SetActive (false);
        titleParent.SetActive (false);
        shopFirstCanvas.SetActive (false);
        shopSecondCanvas.SetActive (true);
    }

    public void BuySmallPouch () {
        // TBD
    }

    public void BuyMediumPouch () {
        // TBD
    }

    public void BuyBigPouch () {
        // TBD
    }

    public void BuyMediumMode () {
        mediumModeButton.interactable = false;
        mediumModeButton.image.sprite = purchasedMediumMode;
        mediumModeText.text = "Adquirido";
        PlayerPrefs.SetInt ("coinsCount", PlayerPrefs.GetInt ("coinsCount", 0) - 300);
        PlayerPrefs.SetString ("medium", "purchased");
        PlayerPrefs.Save ();
    }

    public void BuyHardMode () {
        hardModeButton.interactable = false;
        hardModeButton.image.sprite = purchasedHardMode;
        hardModeText.text = "Adquirido";
        PlayerPrefs.SetInt ("coinsCount", PlayerPrefs.GetInt ("coinsCount", 0) - 500);
        PlayerPrefs.SetString ("hard", "purchased");
        PlayerPrefs.Save ();
    }
}