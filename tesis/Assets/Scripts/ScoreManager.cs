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
    private bool isDoubleScore = false;
    private bool isDoubleMeters = false;

    private void Start()
    {
        score = 0;
        UpdateScore();
    }

    public void AddCoin()
    {
        if(isDoubleScore)
        {
            score = score +2;
        } else
        {
            score++;
        }

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
            if(isDoubleMeters) {
                meterCounter += Time.deltaTime * speed * 2;
            } else
            {
                meterCounter += Time.deltaTime * speed;
            }
            
            int seconds = (int) meterCounter;
            metersText.text = seconds.ToString("D4") + " m";
        }
    }

    public void TurnOnDoubleCoins()
    {
        isDoubleScore = true;
    }

    public void TurnOffDoubleCoins()
    {
        isDoubleScore = false;
    }

    public void TurnOnDoubleMeters()
    {
        isDoubleMeters = true;
    }

    public void TurnOffDoubleMeters()
    {
        isDoubleMeters = false;
    }

    public void Hit()
    {
        score = score - 15;
        if(score < 0)
        {
            score = 0;
        }
        UpdateScore();
    }

    public bool HasCoins()
    {
        return score > 0;
    }
}
