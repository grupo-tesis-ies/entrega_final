using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance = null;

    // Sounds
    public AudioClip boardClip;
    public AudioClip hitClip;
    public AudioClip loseCoinClip;
    public AudioClip coinClip;
    public AudioClip finishClip;

    // On Game
    public ScoreManager scoreManager;
    public ObjectsFactory objectsFactory;
    public MainCharacterController mainCharacterController;
    public BlackController blackController;
    public CamController camController;
    public TitleBoard titleBoard;
    public TitleWords titleWords;

    // Background
    public ScrollerController backgroundScroller;
    public ScrollerController backgroundOffsetScroller;
    private float backgroundSpeed = 0.7f;
    private const float BACKGROUND_SPEED_TRANSITION = 0.5f;
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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        camController.TriggerZoomOut();
        blackController.gameObject.SetActive(true);
    }

    public void StartsFlying()
    {
        mainCharacterController.SetMoving(true);
        string activeScene = SceneManager.GetActiveScene().name;
        if (GameConstants.SCENE_MENU.Equals(activeScene))
        {
            Invoke("FadeIn", INVOKE_TIME_FADE_IN);
            Invoke("LoadGameScene", INVOKE_TIME_LOAD_GAME_SCENE);
        }
        else if (GameConstants.SCENE_GAME.Equals(activeScene))
        {
            StartCoroutine(StopCharacter(STOP_CHARACTER_TIME));
        }

    }

    void FadeIn()
    {
        blackController.FadeIn();
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(GameConstants.SCENE_GAME);
    }

    public void FadeFinish()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (GameConstants.SCENE_MENU.Equals(activeScene))
        {
            SoundManager.instance.PlaySingle(boardClip);
            titleBoard.TriggerTitleBoard();
        }
        else if (GameConstants.SCENE_GAME.Equals(activeScene))
        {
            inGame = true;
            StartMovingFloor();
            mainCharacterController.Launch();
        }

        blackController.gameObject.SetActive(false);
    }

    public void TriggerTitleWords()
    {
        titleWords.TriggerWords();
    }

    IEnumerator StopCharacter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        mainCharacterController.SetMoving(false);
        mainCharacterController.SetSwipe(true);
        scoreManager.StartMetersCounter();
        Invoke("InstantiateObs", INVOKE_TIME_INSTANTIATE_OBS);
        Invoke("InstantiateCoin", INVOKE_TIME_INSTANTIATE_COIN);
        Invoke("InstantiatePowerUps", INVOKE_TIME_INSTANTIATE_POWER_UPS);
    }

    void InstantiateObs()
    {
        objectsFactory.InstantiateObs();
    }

    void InstantiateCoin()
    {
        objectsFactory.InstantiateCoin();
    }

    void InstantiatePowerUps()
    {
        objectsFactory.InstantiatePowerUps();
    }

    void StartMovingFloor()
    {
        //backgroundSpeed = 0.7f;
        backgroundScroller.SetSpeed(Mathf.Lerp(BACKGROUND_IDLE_SPEED, backgroundSpeed, BACKGROUND_SPEED_TRANSITION));
        backgroundOffsetScroller.SetSpeed(Mathf.Lerp(BACKGROUND_IDLE_SPEED, backgroundSpeed, BACKGROUND_SPEED_TRANSITION));
    }

    public void SetBackgroundSpeed(float backgroundSpeed)
    {
        this.backgroundSpeed = backgroundSpeed;
    }

    public void BirdEntersState(AnimatorStateInfo stateInfo)
    {
        if (stateInfo.IsTag("shield"))
        {
            mainCharacterController.SetShieldOn();
        }
        else if (stateInfo.IsTag("x2"))
        {
            scoreManager.SetCoinValue(2);
        }
        else if (stateInfo.IsTag("impulse"))
        {
            mainCharacterController.SetImpulseOn();
            scoreManager.SetMetersValue(3);
            SetBackgroundSpeed(backgroundSpeed * 3);
        }
    }

    public void BirdExitsState(AnimatorStateInfo stateInfo)
    {
        if (stateInfo.IsTag("shield"))
        {
            mainCharacterController.SetShieldOff();
        }
        else if (stateInfo.IsTag("x2"))
        {
            scoreManager.SetCoinValue(1);
        }
        else if (stateInfo.IsTag("impulse"))
        {
            mainCharacterController.SetImpulseOff();
            scoreManager.SetMetersValue(1);
            SetBackgroundSpeed(backgroundSpeed / 3);
        }
    }

    public void ZoomOffFinish()
    {
        mainCharacterController.Launch();
    }

    public void CoinTriggered()
    {
        if(!inGame)
        {
            return;
        }

        SoundManager.instance.PlayPowerUp(coinClip);
        scoreManager.AddCoin();
    }

    public void GotHit()
    {
        if (!inGame)
        {
            return;
        }

        if (scoreManager.HasCoins())
        {
            GameObject.Find("Bird Particles").GetComponent<ParticleSystem>().Play();
            SoundManager.instance.PlayPowerUp(loseCoinClip);
        }

        SoundManager.instance.PlaySingle(hitClip);
        scoreManager.Hit();
    }

    public void Reached200()
    {
        if (GameConstants.SCENE_GAME.Equals(SceneManager.GetActiveScene().name))
        {
            inGame = false;
            blackController.gameObject.SetActive(true);
            blackController.FadeIn();
            mainCharacterController.SetPlaying(false);
            Invoke("PlayFinishClip", 2f);
        }
    }

    void PlayFinishClip()
    {
        SoundManager.instance.PlaySingle(finishClip);
        SceneManager.LoadScene(GameConstants.SCENE_MENU);
    }
}
