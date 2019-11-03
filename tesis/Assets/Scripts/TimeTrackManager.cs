using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTrackManager : MonoBehaviour {
    public static TimeTrackManager instance = null;
    public Text remainingTimeText;
    public Text metersText;

    private int remainingTime;
    private float metersCounter;
    private bool isCounting;
    private int chronValue;
    private int obstacleValue;
    private float metersValue;

    private float timeCounter;

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
        remainingTime = 80;
        chronValue = 10;
        metersValue = 1;
        obstacleValue = 10;
        UpdateScore ();
        UpdateMeters ();
        isPlaying = true;
    }

    void Update () {
        if (isCounting) {
            metersCounter += Time.deltaTime * metersValue;
            timeCounter += Time.deltaTime;
            UpdateScore ();
            UpdateMeters ();
            if ((int) metersCounter == 100 && isPlaying) {
                GameEvents.instance.Reached100 ();
                isPlaying = false;
            }
        }
    }

    void UpdateScore () {
        remainingTimeText.text = ((int) (remainingTime - timeCounter)).ToString ("D3") + "s";
    }

    void UpdateMeters () {
        metersText.text = ((int) metersCounter).ToString ("D4") + " m";
    }

    public void StartMetersCounter () {
        isCounting = true;
    }

    public void AddChrono () {
        remainingTime += chronValue;

        UpdateScore ();
    }

    public void SetChronValue (int chronValue) {
        this.chronValue = chronValue;
    }

    public void SetObstacleValue (int obstacleValue) {
        this.obstacleValue = obstacleValue;
    }

    public void SetMetersValue (int metersValue) {
        this.metersValue = metersValue;
    }

    public int GetChronCount () {
        return remainingTime;
    }
}