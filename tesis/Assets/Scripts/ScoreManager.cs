using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance = null;
    public Text scoreText;
    public Text metersText;

    private int score;
    private float metersCounter;
    private bool isCounting;
    private int coinValue;
    private int obstacleValue;
    private float metersValue;

    private bool isPlaying;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }
    void Start () {
        isCounting = false;
        metersCounter = 0.0f;
        score = 0;
        coinValue = 1;
        metersValue = 1;
        obstacleValue = 10;
        UpdateScore ();
        UpdateMeters ();
        isPlaying = true;
    }

    void Update () {
        if (isCounting) {
            metersCounter += Time.deltaTime * metersValue;
            UpdateMeters ();
            if ((int) metersCounter == 30 && isPlaying) {
                GameEvents.instance.Reached200 ();
                isPlaying = false;
            }
        }
    }

    void UpdateScore () {
        scoreText.text = score.ToString ("D4");
    }

    void UpdateMeters () {
        metersText.text = ((int) metersCounter).ToString ("D4") + " m";
    }

    public void StartMetersCounter () {
        isCounting = true;
    }

    public void AddCoin () {
        score += coinValue;

        UpdateScore ();
    }

    public void Hit () {
        score -= obstacleValue;
        if (score < 0) {
            score = 0;
        }
        UpdateScore ();
    }

    public bool HasCoins () {
        return score > 0;
    }

    public void SetCoinValue (int coinValue) {
        this.coinValue = coinValue;
    }

    public void SetObstacleValue (int obstacleValue) {
        this.obstacleValue = obstacleValue;
    }

    public void SetMetersValue (int metersValue) {
        this.metersValue = metersValue;
    }

    public int GetCoinsCount() {
        return score;
    }
}