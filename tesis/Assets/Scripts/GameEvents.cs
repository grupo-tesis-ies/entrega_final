using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

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
    private const float INVOKE_TIME_INSTANTIATE_CHRONO = 8f;

    private bool inGame;

    public string gameMode;

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
            TitleBoard.instance.TriggerTitleBoard ();
            OnMenuSounds.instance.PlayBoard ();
            BlackController.instance.gameObject.SetActive (false);

            MainMenuManager.instance.SetHistoryModeFinished (PlayerPrefs.GetInt ("storyMode") == 1);
        } else if (GameConstants.SCENE_GAME.Equals (activeScene)) {
            inGame = true;
            MoveFloorFrom (BACKGROUND_IDLE_SPEED, GameParameters.instance.GetBackgroundSpeed ());
            MainCharacterController.instance.Launch ();
            BlackController.instance.gameObject.SetActive (false);
        } else if (GameConstants.SCENE_TIME_TRACK.Equals (activeScene)) {
            inGame = true;
            MoveFloorFrom (BACKGROUND_IDLE_SPEED, GameParameters.instance.GetBackgroundSpeed ());
            MainCharacterController.instance.Launch ();
            BlackController.instance.gameObject.SetActive (false);
        }
    }

    public void StartGame (string gameMode) {
        instance.gameMode = gameMode;
        if ("story".Equals (gameMode)) {
            CamController.instance.TriggerSignIn ();
        } else {
            GoToGame ();
        }
    }

    public void DisplayInstructionsOff () {
        CamController.instance.TriggerSignOut ();
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
            if ("story".Equals (gameMode)) {
                Invoke ("LoadGameScene", INVOKE_TIME_LOAD_GAME_SCENE);
            } else if ("easy".Equals (gameMode)) {
                Invoke ("LoadEasyModeScene", INVOKE_TIME_LOAD_GAME_SCENE);
            } else if ("medium".Equals (gameMode)) {
                Invoke ("LoadMediumModeScene", INVOKE_TIME_LOAD_GAME_SCENE);
            } else if ("hard".Equals (gameMode)) {
                Invoke ("LoadHardModeScene", INVOKE_TIME_LOAD_GAME_SCENE);
            }
        } else if (GameConstants.SCENE_GAME.Equals (activeScene) || GameConstants.SCENE_TIME_TRACK.Equals (activeScene)) {
            StartCoroutine (StopCharacter (STOP_CHARACTER_TIME));
        }
    }

    void FadeIn () {
        BlackController.instance.FadeIn ();
    }

    void LoadGameScene () {
        SceneManager.LoadScene (GameConstants.SCENE_GAME);
    }

    void LoadEasyModeScene () {
        SceneManager.LoadScene (GameConstants.SCENE_TIME_TRACK);
    }

    public void TriggerTitleWords () {
        TitleWords.instance.TriggerWords ();
    }

    IEnumerator StopCharacter (int seconds) {
        yield return new WaitForSeconds (seconds);
        MainCharacterController.instance.SetMoving (false);
        MainCharacterController.instance.SetSwipe (true);

        ObjectsFactory.instance.SetSpeed (GameParameters.instance.GetObjectsSpeed ());
        Invoke ("InstantiateObs", INVOKE_TIME_INSTANTIATE_OBS);
        Invoke ("InstantiatePowerUps", INVOKE_TIME_INSTANTIATE_POWER_UPS);
        if (GameConstants.SCENE_TIME_TRACK.Equals (SceneManager.GetActiveScene ().name)) {
            Invoke ("InstantiateCoin", INVOKE_TIME_INSTANTIATE_COIN);
            Invoke ("InstantiateChrono", INVOKE_TIME_INSTANTIATE_CHRONO);
        } else {
            Invoke ("InstantiateCoin", INVOKE_TIME_INSTANTIATE_COIN);
        }
        ScoreManager.instance.StartCounting ();
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

    void InstantiateChrono () {
        ObjectsFactory.instance.InstantiateChrono ();
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
            ScoreManager.instance.SetCoinMultiplier (1);
        } else if (stateInfo.IsTag ("impulse")) {
            MainCharacterController.instance.SetImpulseOff ();
            ScoreManager.instance.SetMetersMultiplier (1);

            MoveFloorFrom (GameParameters.instance.GetBackgroundSpeed () * 3, GameParameters.instance.GetBackgroundSpeed ());
            ObjectsFactory.instance.SetSpeed (GameParameters.instance.GetObjectsSpeed ());
        }
        PowerUpParticles.instance.DisableEmission ();
    }

    public void ZoomOffFinish () {
        // despegue ?? OnMenuSounds.instance.PlayFlyLaunch();
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

        if (obstacleName == "branch" || obstacleName == "thorn") {
            OnGameSounds.instance.PlayBranchHit ();
        } else if (obstacleName == "berry") {
            OnGameSounds.instance.PlayBerriesHit ();
        } else if (obstacleName == "cam") {
            OnGameSounds.instance.PlayCamHit ();
        }

        ScoreManager.instance.Hit ();
    }

    public void Finished () {
        if ("story".Equals (gameMode)) {
            FinishedStory ();
        } else {
            FinishedTrack ();
        }
    }

    public void FinishedStory () {
        if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
            inGame = false;
            if (PlayGamesPlatform.Instance.localUser.authenticated) {
                PlayGamesPlatform.Instance.ReportProgress (
                    GPGSIds.achievement_finalizar_modo_historia,
                    100.0f, (bool success) => {
                        Debug.Log ("History mode finished: " +
                            success);
                    });

                PlayGamesPlatform.Instance.ReportScore (ScoreManager.instance.GetCoinsCount (),
                    GPGSIds.leaderboard_cantidad_de_monedas_test,
                    (bool success) => {
                        Debug.Log ("Coins Increment: " + success);
                    });
            }
            PlayerPrefs.SetInt ("storyMode", 1);
            UpdateCoins ();

            BlackController.instance.gameObject.SetActive (true);
            BlackController.instance.FadeIn ();
            MainCharacterController.instance.SetPlaying (false);
            Invoke ("PlayFinishClip", 2f);
        }
    }

    void UpdateCoins () {
        int coins = PlayerPrefs.GetInt ("coinsCount", 0);
        PlayerPrefs.SetInt ("coinsCount", coins + ScoreManager.instance.GetCoinsCount ());
        PlayerPrefs.Save ();
    }

    public void FinishedTrack () {
        inGame = false;
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ReportScore (ScoreManager.instance.GetChronoCount (),
                GPGSIds.leaderboard_duracin_en_modo_endless,
                (bool success) => {
                    Debug.Log ("Coins Increment: " + success);
                });
        }

        UpdateCoins ();
        BlackController.instance.gameObject.SetActive (true);
        BlackController.instance.FadeIn ();
        MainCharacterController.instance.SetPlaying (false);
        Invoke ("PlayFinishClip", 2f);
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
            PowerUpParticles.instance.SetShieldOn ();
            MainCharacterController.instance.SetShieldOn ();
        } else if (powerUpName.Equals ("x2Up")) {
            PowerUpParticles.instance.SetX2On ();
            ScoreManager.instance.SetCoinMultiplier (GameParameters.instance.GetCoinsPowerUpMultiplier ());
        } else if (powerUpName.Equals ("impulseUp")) {
            PowerUpParticles.instance.SetImpulseOn ();
            MainCharacterController.instance.SetImpulseOn ();
            ScoreManager.instance.SetMetersMultiplier ((int) GameParameters.instance.GetImpulseSpeedMultiplier ());

            MoveFloorFrom (GameParameters.instance.GetBackgroundSpeed (), GameParameters.instance.GetBackgroundSpeed () * GameParameters.instance.GetImpulseSpeedMultiplier ());
            ObjectsFactory.instance.SetSpeed (GameParameters.instance.GetObjectsSpeed () * GameParameters.instance.GetImpulseSpeedMultiplier ());
        }

        OnGameSounds.instance.PlayGotPowerUp ();
        BirdBodyController.instance.TriggerPowerUp (powerUpName);
    }

    public void ThornHit () {
        ScoreManager.instance.SetMetersMultiplier (0);
        MoveFloorFrom (GameParameters.instance.GetBackgroundSpeed (), GameParameters.instance.GetBackgroundSpeed () / GameParameters.instance.GetThornSpeedMultiplier ());
        ObjectsFactory.instance.SetSpeed (GameParameters.instance.GetObjectsSpeed () / GameParameters.instance.GetThornSpeedMultiplier ());
        ObjectsFactory.instance.StopProducing ();
        SwipeMove.instance.SetSuperSlowed ();
        Invoke ("ThornOff", 2f);
    }

    public void ThornOff () {
        ObjectsFactory.instance.Produce ();
        ScoreManager.instance.SetMetersMultiplier (1);
        MoveFloorFrom (GameParameters.instance.GetBackgroundSpeed () / GameParameters.instance.GetThornSpeedMultiplier (), GameParameters.instance.GetBackgroundSpeed ());
        SwipeMove.instance.ReturnSpeed ();
        ObjectsFactory.instance.SetSpeed (GameParameters.instance.GetObjectsSpeed ());
    }

    public void ChronoTriggered () {
        OnGameSounds.instance.PlayGotCoin ();
        ScoreManager.instance.AddChrono ();
    }

    public string GetGameMode () {
        return gameMode;
    }
}