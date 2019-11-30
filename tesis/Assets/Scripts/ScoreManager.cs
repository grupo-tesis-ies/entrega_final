using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance = null;

    public Text scoreText;
    public Text metersText;
    public Text remainingTimeText;

    private int score;
    private float metersCounter;
    private bool isCounting;
    private int coinValue;
    private float metersMultiplier;

    private bool isPlaying;
    private bool isHistoryMode;

    private int remainingTime;
    private float timeCounter;

    private int coinMultiplier;

    private int metersToFinish;

    private int chronoBonusCounter;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    void Start () {
        instance.isCounting = false;
        instance.metersCounter = 0.0f;
        instance.score = 0;
        instance.isPlaying = true;
        instance.timeCounter = 0;
        instance.coinMultiplier = 1;
        instance.metersMultiplier = 1;
        instance.chronoBonusCounter = 0;

        UpdateScore ();
        UpdateMeters ();

        if (!GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
            isHistoryMode = false;
            instance.remainingTime = GameParameters.instance.GetTimeByMode (GameEvents.instance.GetGameMode ());
            UpdateTimer ();
        } else {
            isHistoryMode = true;
        }

        metersToFinish = GameParameters.instance.GetMetersToFinishByMode (GameEvents.instance.GetGameMode ());
    }

    void Update () {
        if (instance.isCounting) {
            if (!isHistoryMode) {
                if ((int) chronoBonusCounter == 10) {
                    AddChrono ();
                    chronoBonusCounter = 0;
                }

                timeCounter += Time.deltaTime;
                UpdateTimer ();
            }
            if ((int) metersCounter == metersToFinish && isPlaying) {
                GameEvents.instance.Finished ();
                instance.isPlaying = false;
            }

            metersCounter += Time.deltaTime * metersMultiplier;

            UpdateMeters ();
        }
    }

    void UpdateScore () {
        scoreText.text = score.ToString ("D4");
    }

    void UpdateMeters () {
        metersText.text = ((int) metersCounter).ToString ("D4") + " m";
    }

    void UpdateTimer () {
        if (((int) (remainingTime - timeCounter)) <= 0) {
            isPlaying = false;
            isCounting = false;
            GameEvents.instance.Lose ();
        } else {
            remainingTimeText.text = ((int) (remainingTime - timeCounter)).ToString ("D3") + "s";
        }
    }

    public void StartCounting () {
        instance.isCounting = true;
    }

    public void StopCounting () {
        instance.isCounting = true;
    }

    public void AddCoin () {
        score += GameParameters.instance.GetCoinsValue () * coinMultiplier;
        chronoBonusCounter++;

        UpdateScore ();
    }

    public void AddChrono () {
        remainingTime += GameParameters.instance.GetChronoTimeValue ();

        UpdateTimer ();
    }

    public void Hit () {
        score -= GameParameters.instance.GetCoinsLostOnCollision ();
        chronoBonusCounter = 0;
        if (score < 0) {
            score = 0;
        }
        UpdateScore ();
    }

    public bool HasCoins () {
        return score > 0;
    }

    public void SetCoinMultiplier (int coinMultiplier) {
        this.coinMultiplier = coinMultiplier;
    }

    public void SetMetersMultiplier (int metersMultiplier) {
        this.metersMultiplier = metersMultiplier;
    }

    public int GetCoinsCount () {
        return score;
    }

    public int GetChronoCount () {
        return remainingTime;
    }
}