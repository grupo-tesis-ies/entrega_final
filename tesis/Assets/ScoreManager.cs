using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text metersText;
    public float speed;

    private float meterCounter = 0.0f;
    private bool isCounting = false;
    private int score;

    private void Start()
    {
        score = 0;
        UpdateScore();
    }

    public void AddCoin()
    {
        score++;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString("D4");
    }

    public void StartMetersCounter()
    {
        isCounting = true;
    }

    void Update()
    {
        if(isCounting)
        {
            meterCounter += Time.deltaTime * speed;
            int seconds = (int) meterCounter % 60;
            metersText.text = seconds.ToString("D4") + " m";
        }
    }
}
