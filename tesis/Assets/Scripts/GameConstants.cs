using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {
    //Animations
    public const string ANIMATION_BLACK_FADE_IN = "FadeIn";
    public const string ANIMATION_BLACK_FADE_OUT = "FadeOut";
    public const string ANIMATION_LAUNCH = "Launch";
    public const string ANIMATION_ZOOM_OUT = "ZoomOut";
    public const string ANIMATION_SIGN_IN = "SignIn";

    public const string ANIMATION_SIGN_OUT = "SignOut";
    public const string ANIMATION_TITLE_BOARD_DISPLAY = "TitleDisplay";
    public const string ANIMATION_TITLE_WORDS_DISPLAY = "Words";
    public const string ANIMATION_HIT = "Hit";

    //Tags
    public const string TAG_PLAYER = "Player";
    public const string TAG_OBSTACLE = "Obstacle";
    public const string TAG_COIN = "Coin";

    //Game
    public const float OBJECTS_SPAWN_HEIGHT = 2.5f;

    public const float OBJECTS_OFFSET_TILE_SIZE = 4f;

    //Scenes
    public const string SCENE_MENU = "Menu";
    public const string SCENE_GAME = "HistoryMode";
    public const string SCENE_SPLASH = "Splash";
    public const string SCENE_TIME_TRACK = "TimeTrack";

    public const float BIRD_FLIGHT_SPEED = 2f;
}