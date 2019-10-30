using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour {
    public static GameEvents instance = null;

    private float backgroundSpeed = 1.4f;
    private float previousSpeed;
    private const float BACKGROUND_SPEED_TRANSITION = 1f;
    private const float BACKGROUND_IDLE_SPEED = 0f;
    private const int STOP_CHARACTER_TIME = 1;

    // Invoke Times
    private const float INVOKE_TIME_FADE_IN = 1.8f;
    private const float INVOKE_TIME_LOAD_GAME_SCENE = 1.5f;
    private const float INVOKE_TIME_MOVE_CHARACTER = 2.5f;
    private const float INVOKE_TIME_INSTANTIATE_OBS = 0.8f;
    private const float INVOKE_TIME_INSTANTIATE_COIN = 0.5f;
    private const float INVOKE_TIME_INSTANTIATE_POWER_UPS = 5f;

    private bool inGame;

    private bool isStoryMode = true;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    public void FadeFinish () {
        string activeScene = SceneManager.GetActiveScene ().name;
        if (GameConstants.SCENE_SPLASH.Equals (activeScene)) {
            Invoke ("FadeIn", 1f);
            Invoke ("GoToMenu", 3f);
        } else if (GameConstants.SCENE_MENU.Equals (activeScene)) {
            OnMenuSounds.instance.PlayBoard ();
            TitleBoard.instance.TriggerTitleBoard ();
            BlackController.instance.gameObject.SetActive (false);
        } else if (GameConstants.SCENE_GAME.Equals (activeScene)) {
            inGame = true;
            MoveFloorFrom (BACKGROUND_IDLE_SPEED, backgroundSpeed);
            MainCharacterController.instance.Launch ();
            BlackController.instance.gameObject.SetActive (false);
        }
    }

    public void StartGame () {
        if (isStoryMode) {
            CamController.instance.TriggerSignIn ();
            Invoke ("DisplayInstructions", 3f);
        } else {
            GoToGame ();
        }
    }

    void DisplayInstructions () {
        CamController.instance.TriggerSignOut();
        Invoke ("GoToGame", 1.5f);
    }

    void GoToGame () {
        CamController.instance.TriggerZoomOut ();
        BlackController.instance.gameObject.SetActive (true);
    }

    public void StartsFlying () {
        MainCharacterController.instance.SetMoving (true);
        string activeScene = SceneManager.GetActiveScene ().name;
        if (GameConstants.SCENE_MENU.Equals (activeScene)) {
            Invoke ("FadeIn", INVOKE_TIME_FADE_IN);
            Invoke ("LoadGameScene", INVOKE_TIME_LOAD_GAME_SCENE);
        } else if (GameConstants.SCENE_GAME.Equals (activeScene)) {
            StartCoroutine (StopCharacter (STOP_CHARACTER_TIME));
        }
    }

    void FadeIn () {
        BlackController.instance.FadeIn ();
    }

    void LoadGameScene () {
        SceneManager.LoadScene (GameConstants.SCENE_GAME);
    }

    public void TriggerTitleWords () {
        TitleWords.instance.TriggerWords ();
    }

    IEnumerator StopCharacter (int seconds) {
        yield return new WaitForSeconds (seconds);
        MainCharacterController.instance.SetMoving (false);
        MainCharacterController.instance.SetSwipe (true);
        ScoreManager.instance.StartMetersCounter ();
        Invoke ("InstantiateObs", INVOKE_TIME_INSTANTIATE_OBS);
        Invoke ("InstantiateCoin", INVOKE_TIME_INSTANTIATE_COIN);
        Invoke ("InstantiatePowerUps", INVOKE_TIME_INSTANTIATE_POWER_UPS);
    }

    void InstantiateObs () {
        ObjectsFactory.instance.InstantiateObs ();
    }

    void InstantiateCoin () {
        ObjectsFactory.instance.InstantiateCoin ();
    }

    void InstantiatePowerUps () {
        ObjectsFactory.instance.InstantiatePowerUps ();
    }

    void MoveFloorFrom (float previousSpeed, float newSpeed) {
        if (previousSpeed < newSpeed) {
            ScrollerController.instance.SetSpeed (Mathf.Lerp (previousSpeed, newSpeed, BACKGROUND_SPEED_TRANSITION));
            OffsetScrollerController.instance.SetSpeed (Mathf.Lerp (previousSpeed, newSpeed, BACKGROUND_SPEED_TRANSITION));
        } else {
            ScrollerController.instance.SetSpeed (newSpeed);
            OffsetScrollerController.instance.SetSpeed (newSpeed);
        }
    }

    public void BirdExitsState (AnimatorStateInfo stateInfo) {
        if (stateInfo.IsTag ("shield")) {
            MainCharacterController.instance.SetShieldOff ();
        } else if (stateInfo.IsTag ("x2")) {
            ScoreManager.instance.SetCoinValue (1);
        } else if (stateInfo.IsTag ("impulse")) {
            MainCharacterController.instance.SetImpulseOff ();
            ScoreManager.instance.SetMetersValue (1);
            MoveFloorFrom (backgroundSpeed * 3, backgroundSpeed);
            ObjectsFactory.instance.SetSpeed (2f);
        }
    }

    public void ZoomOffFinish () {
        MainCharacterController.instance.Launch ();
    }

    public void CoinTriggered () {
        if (!inGame) {
            return;
        }

        OnGameSounds.instance.PlayGotCoin ();
        ScoreManager.instance.AddCoin ();
    }

    public void GotHit (string obstacleName) {
        if (!inGame) {
            return;
        }

        if (ScoreManager.instance.HasCoins ()) {
            GameObject.Find ("Bird Particles").GetComponent<ParticleSystem> ().Play ();
            OnGameSounds.instance.PlayLoseCoin ();
        }

        if (obstacleName == "branch") {
            OnGameSounds.instance.PlayBranchHit ();
        } else if (obstacleName == "berry") {
            OnGameSounds.instance.PlayBerriesHit ();
        } else if (obstacleName == "cam") {
            OnGameSounds.instance.PlayCamHit ();
        }

        ScoreManager.instance.Hit ();
    }

    public void Reached200 () {
        if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
            inGame = false;
            BlackController.instance.gameObject.SetActive (true);
            BlackController.instance.FadeIn ();
            MainCharacterController.instance.SetPlaying (false);
            Invoke ("PlayFinishClip", 2f);
        }
    }

    void PlayFinishClip () {
        OnGameSounds.instance.PlayLevelCompleted ();
        Invoke ("GoToMenu", 4f);
    }

    void GoToMenu () {
        SceneManager.LoadScene (GameConstants.SCENE_MENU);
    }

    public void PowerUpTriggered (string powerUpName) {
        if (!inGame) {
            return;
        }
        if (powerUpName.Equals ("shieldUp")) {
            MainCharacterController.instance.SetShieldOn ();
        } else if (powerUpName.Equals ("x2Up")) {
            ScoreManager.instance.SetCoinValue (2);
            // magneto
        } else if (powerUpName.Equals ("impulseUp")) {
            MainCharacterController.instance.SetImpulseOn ();
            ScoreManager.instance.SetMetersValue (3);
            MoveFloorFrom (backgroundSpeed, backgroundSpeed * 3);
            ObjectsFactory.instance.SetSpeed (4f);
        }

        BirdBodyController.instance.TriggerPowerUp (powerUpName);
        OnGameSounds.instance.PlayGotPowerUp ();
    }
}