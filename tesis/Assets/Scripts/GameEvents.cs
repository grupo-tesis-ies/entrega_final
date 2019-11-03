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

    private bool isStoryMode = true;

    private bool isEasyMode = true;

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

            MainMenuManager.instance.SetHistoryModeFinished (PlayerPrefs.GetInt("storyMode") == 1);
        } else if (GameConstants.SCENE_GAME.Equals (activeScene)) {
            inGame = true;
            MoveFloorFrom (BACKGROUND_IDLE_SPEED, backgroundSpeed);
            MainCharacterController.instance.Launch ();
            BlackController.instance.gameObject.SetActive (false);
        } else if (GameConstants.SCENE_TIME_TRACK.Equals (activeScene)) {
            inGame = true;
            MoveFloorFrom (BACKGROUND_IDLE_SPEED, backgroundSpeed);
            MainCharacterController.instance.Launch ();
            BlackController.instance.gameObject.SetActive (false);
        }
    }

    public void StartGame (bool isStoryMode, bool isEasyMode) {
        instance.isStoryMode = isStoryMode;
        instance.isEasyMode = isEasyMode;
        if (isStoryMode) {
            CamController.instance.TriggerSignIn ();
            Invoke ("DisplayInstructions", 3f);
        } else {
            GoToGame ();
        }
    }

    void DisplayInstructions () {
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
            if (isStoryMode) {
                Invoke ("LoadGameScene", INVOKE_TIME_LOAD_GAME_SCENE);
            } else if (isEasyMode) {
                Invoke ("LoadEasyModeScene", INVOKE_TIME_LOAD_GAME_SCENE);
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

        Invoke ("InstantiateObs", INVOKE_TIME_INSTANTIATE_OBS);
        Invoke ("InstantiatePowerUps", INVOKE_TIME_INSTANTIATE_POWER_UPS);
        if (GameConstants.SCENE_TIME_TRACK.Equals (SceneManager.GetActiveScene ().name)) {
            TimeTrackManager.instance.StartMetersCounter ();
            Invoke ("InstantiateChrono", INVOKE_TIME_INSTANTIATE_CHRONO);
        } else {
            Invoke ("InstantiateCoin", INVOKE_TIME_INSTANTIATE_COIN);
            ScoreManager.instance.StartMetersCounter ();
        }
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
            ScoreManager.instance.SetCoinValue (1);
        } else if (stateInfo.IsTag ("impulse")) {
            MainCharacterController.instance.SetImpulseOff ();
            if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
                ScoreManager.instance.SetMetersValue (1);
            } else {
                TimeTrackManager.instance.SetMetersValue (1);
            }
            MoveFloorFrom (backgroundSpeed * 3, backgroundSpeed);
            ObjectsFactory.instance.SetSpeed (2f);
        }
        PowerUpParticles.instance.DisableEmission ();
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

        if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
            if (ScoreManager.instance.HasCoins ()) {
                GameObject.Find ("Bird Particles").GetComponent<ParticleSystem> ().Play ();
                OnGameSounds.instance.PlayLoseCoin ();
            }
        }

        if (obstacleName == "branch" || obstacleName == "thorn") {
            OnGameSounds.instance.PlayBranchHit ();
        } else if (obstacleName == "berry") {
            OnGameSounds.instance.PlayBerriesHit ();
        } else if (obstacleName == "cam") {
            OnGameSounds.instance.PlayCamHit ();
        }

        if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
            ScoreManager.instance.Hit ();
        }
    }

    public void Reached200 () {
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
            PlayerPrefs.SetInt("storyMode", 1);
            PlayerPrefs.Save();

            BlackController.instance.gameObject.SetActive (true);
            BlackController.instance.FadeIn ();
            MainCharacterController.instance.SetPlaying (false);
            Invoke ("PlayFinishClip", 2f);
        }
    }

    public void Reached100 () {
        inGame = false;
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ReportScore (TimeTrackManager.instance.GetChronCount (),
                GPGSIds.leaderboard_duracin_en_modo_endless,
                (bool success) => {
                    Debug.Log ("Coins Increment: " + success);
                });
        }

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
            ScoreManager.instance.SetCoinValue (2);
            // magneto
        } else if (powerUpName.Equals ("impulseUp")) {
            PowerUpParticles.instance.SetImpulseOn ();
            MainCharacterController.instance.SetImpulseOn ();
            if (GameConstants.SCENE_GAME.Equals (SceneManager.GetActiveScene ().name)) {
                ScoreManager.instance.SetMetersValue (3);
            } else {
                TimeTrackManager.instance.SetMetersValue (3);
            }

            MoveFloorFrom (backgroundSpeed, backgroundSpeed * 3);
            ObjectsFactory.instance.SetSpeed (4f);
        }

        BirdBodyController.instance.TriggerPowerUp (powerUpName);
        OnGameSounds.instance.PlayGotPowerUp ();
    }

    public void ThornHit () {
        ScoreManager.instance.SetMetersValue (0);
        MoveFloorFrom (backgroundSpeed, backgroundSpeed / 5);
        ObjectsFactory.instance.SetSpeed (2f / 10f);
        ObjectsFactory.instance.StopProducing ();
        Invoke ("ThornOff", 2f);
    }

    public void ThornOff () {
        ObjectsFactory.instance.Produce ();
        ScoreManager.instance.SetMetersValue (1);
        MoveFloorFrom (backgroundSpeed / 5, backgroundSpeed);
        ObjectsFactory.instance.SetSpeed (2f);
    }

    public void ChronoTriggered () {
        OnGameSounds.instance.PlayGotCoin ();
        TimeTrackManager.instance.AddChrono ();
    }
}