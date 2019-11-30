using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishControl : MonoBehaviour {

    public static GameFinishControl instance = null;

    public GameObject finishCanvas;

    public Text scoreText;

    public Text bonusText;

    public Image firstCoin;

    public Image secondCoin;

    public Image thirdCoin;

    public GameObject hud;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void FinishedStoryMode () {
        hud.SetActive (false);
        Invoke ("FinishedStoryGame", 7f);
    }

    public void FinishedTimeMode () {
        hud.SetActive (false);
        Invoke ("FinishedTimeTrackGame", 7f);
    }

    void FinishedStoryGame () {
        int coins = ScoreManager.instance.GetCoinsCount ();
        if (coins >= 30) {
            thirdCoin.gameObject.SetActive(true);
        }
        if (coins >= 20) {
            secondCoin.gameObject.SetActive(true);
        }
        if (coins >= 10) {
            firstCoin.gameObject.SetActive(true);
        }
        scoreText.text = coins.ToString ();
        finishCanvas.SetActive (true);
    }

    void FinishedTimeTrackGame () {
        int coins = ScoreManager.instance.GetCoinsCount ();
        int chrono = ScoreManager.instance.GetChronoCount ();
        if (coins + chrono >= 30) {
            thirdCoin.gameObject.SetActive(true);
        }
        if (coins + chrono >= 20) {
            secondCoin.gameObject.SetActive(true);
        }
        if (coins + chrono >= 10) {
            firstCoin.gameObject.SetActive(true);
        }
        scoreText.text = coins.ToString ();
        bonusText.text = chrono.ToString ();
        finishCanvas.SetActive (true);
    }
}