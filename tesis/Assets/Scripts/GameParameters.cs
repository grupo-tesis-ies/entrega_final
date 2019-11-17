using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour {

    public static GameParameters instance = null;

    // Player Settings
    [Range (0.0f, 10.0f)]
    public float birdSlideSpeed;

    // On Game Settings
    [Range (0.0f, 10.0f)]
    public float impulseSpeedMultiplier;

    [Range (0.0f, 10.0f)]
    public float thornSpeedMultiplier;

    // Score
    public int coinsValue;
    public int coinsLostOnCollision;

    public int coinsPowerUpMultiplier;

    [Range (0.0f, 10.0f)]
    public float story_backgroundSpeed;

    [Range (0.0f, 10.0f)]
    public float story_obstaculesSpeed;

    [Range (0.0f, 10.0f)]
    public float story_obstaclesMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float story_obstaclesMaxSpawnTime;

    [Range (0.0f, 10.0f)]
    public float story_coinsMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float story_coinsMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float story_powerUpMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float story_powerUpMaxSpawnTime;

    public int story_metersToFinish;

    public int easy_initialTime;
    public int easy_metersToFinish;

    public int easy_chronoTimeValue;

    [Range (0.0f, 10.0f)]
    public float easy_backgroundSpeed;

    [Range (0.0f, 10.0f)]
    public float easy_obstaculesSpeed;

    [Range (0.0f, 10.0f)]
    public float easy_obstaclesMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float easy_obstaclesMaxSpawnTime;

    [Range (0.0f, 10.0f)]
    public float easy_coinsMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float easy_coinsMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float easy_powerUpMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float easy_powerUpMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float easy_chronoMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float easy_chronoMaxSpawnTime;

    public int medium_initialTime;
    public int medium_metersToFinish;

    public int medium_chronoTimeValue;

    [Range (0.0f, 10.0f)]
    public float medium_backgroundSpeed;

    [Range (0.0f, 10.0f)]
    public float medium_obstaculesSpeed;

    [Range (0.0f, 10.0f)]
    public float medium_obstaclesMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float medium_obstaclesMaxSpawnTime;

    [Range (0.0f, 10.0f)]
    public float medium_coinsMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float medium_coinsMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float medium_powerUpMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float medium_powerUpMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float medium_chronoMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float medium_chronoMaxSpawnTime;

    public int hard_initialTime;
    public int hard_metersToFinish;
    public int hard_chronoTimeValue;

    [Range (0.0f, 10.0f)]
    public float hard_backgroundSpeed;

    [Range (0.0f, 10.0f)]
    public float hard_obstaculesSpeed;

    [Range (0.0f, 10.0f)]
    public float hard_obstaclesMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float hard_obstaclesMaxSpawnTime;

    [Range (0.0f, 10.0f)]
    public float hard_coinsMinSpawnTime;

    [Range (0.0f, 10.0f)]
    public float hard_coinsMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float hard_powerUpMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float hard_powerUpMaxSpawnTime;

    [Range (0.0f, 20.0f)]
    public float hard_chronoMinSpawnTime;

    [Range (0.0f, 20.0f)]
    public float hard_chronoMaxSpawnTime;

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
        return story_backgroundSpeed;
    }

    public float GetObjectsSpeed () {
        return story_obstaculesSpeed;
    }

    public float GetImpulseSpeedMultiplier () {
        return impulseSpeedMultiplier;
    }

    public float GetThornSpeedMultiplier () {
        return thornSpeedMultiplier;
    }

    public int GetCoinsValue () {
        return coinsValue;
    }

    public int GetCoinsLostOnCollision () {
        return coinsLostOnCollision;
    }

    public int GetChronoTimeValue () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("easy".Equals (mode)) {
            return easy_chronoTimeValue;
        } else if ("medium".Equals (mode)) {
            return medium_chronoTimeValue;
        } else {
            return hard_chronoTimeValue;
        }
    }

    public int GetCoinsPowerUpMultiplier () {
        return coinsPowerUpMultiplier;
    }

    public float GetObsMinSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_obstaclesMinSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_obstaclesMinSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_obstaclesMinSpawnTime;
        } else {
            return hard_obstaclesMinSpawnTime;
        }
    }

    public float GetObsMaxSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_obstaclesMaxSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_obstaclesMaxSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_obstaclesMaxSpawnTime;
        } else {
            return hard_obstaclesMaxSpawnTime;
        }
    }

    public float GetCoinsMinSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_coinsMinSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_coinsMinSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_coinsMinSpawnTime;
        } else {
            return hard_coinsMinSpawnTime;
        }
    }

    public float GetCoinsMaxSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_coinsMaxSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_coinsMaxSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_coinsMaxSpawnTime;
        } else {
            return hard_coinsMaxSpawnTime;
        }
    }

    public float GetPowerUpMinSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_powerUpMinSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_powerUpMinSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_powerUpMinSpawnTime;
        } else {
            return hard_powerUpMinSpawnTime;
        }
    }

    public float GetPowerUpMaxSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("story".Equals (mode)) {
            return story_powerUpMaxSpawnTime;
        } else if ("easy".Equals (mode)) {
            return easy_powerUpMaxSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_powerUpMaxSpawnTime;
        } else {
            return hard_powerUpMaxSpawnTime;
        }
    }

    public int GetTimeByMode (string mode) {
        if ("easy".Equals (mode)) {
            return easy_initialTime;
        } else if ("medium".Equals (mode)) {
            return medium_initialTime;
        } else {
            return hard_initialTime;
        }
    }

    public int GetMetersToFinishByMode (string mode) {
        if ("story".Equals (mode)) {
            return story_metersToFinish;
        } else if ("easy".Equals (mode)) {
            return easy_metersToFinish;
        } else if ("medium".Equals (mode)) {
            return medium_metersToFinish;
        } else {
            return hard_metersToFinish;
        }
    }

    public float GetChronoMinSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("easy".Equals (mode)) {
            return easy_chronoMinSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_chronoMinSpawnTime;
        } else {
            return hard_chronoMinSpawnTime;
        }
    }

    public float GetChronoMaxSpawnTime () {
        string mode = GameEvents.instance.GetGameMode ();
        if ("easy".Equals (mode)) {
            return easy_chronoMaxSpawnTime;
        } else if ("medium".Equals (mode)) {
            return medium_chronoMaxSpawnTime;
        } else {
            return hard_chronoMaxSpawnTime;
        }
    }
}