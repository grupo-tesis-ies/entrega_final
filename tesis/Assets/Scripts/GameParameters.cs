using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour {

    public static GameParameters instance = null;

    // Player Settings
    [Range (0.0f, 10.0f)]
    public float birdVerticalSpeed;

    [Range (0.0f, 10.0f)]
    public float birdSlideSpeed;

    // On Game Settings
    [Range (0.0f, 10.0f)]
    public float backgroundSpeed;

    [Range (0.0f, 10.0f)]
    public float objectsSpeed;

    [Range (0.0f, 10.0f)]
    public float impulseSpeedMultiplier;

    [Range (0.0f, 10.0f)]
    public float thornSpeedMultiplier;

    // Score
    public int metersValuePerSecond;
    public int coinsValue;
    public int coinsLostOnCollision;

    public int chronoTimeValue;
    public int coinsPowerUpMultiplier;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public float GetBirdSpeed () {
        return birdSlideSpeed;
    }

    public float GetBackgroundSpeed () {
        return backgroundSpeed;
    }

    public float GetObjectsSpeed () {
        return objectsSpeed;
    }

    public float GetImpulseSpeedMultiplier () {
        return impulseSpeedMultiplier;
    }

    public float GetThornSpeedMultiplier () {
        return thornSpeedMultiplier;
    }

    public int GetMetersValuePerSecond () {
        return metersValuePerSecond;
    }

    public int GetCoinsValue () {
        return coinsValue;
    }

    public int GetCoinsLostOnCollision () {
        return coinsLostOnCollision;
    }

    public int GetChronoTimeValue () {
        return chronoTimeValue;
    }

    public int GetCoinsPowerUpMultiplier () {
        return coinsPowerUpMultiplier;
    }
}