using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuEvents : MonoBehaviour {

    public Sprite signedIn;

    public Sprite signedOff;

    public Image googlePlayButton;

    public Sprite achievementsOn;

    public Sprite achievementsOff;

    public Image achievementsButton;

    void Start () {
        GameObject startButton = GameObject.Find ("startButton");
        EventSystem.current.firstSelectedGameObject = startButton;

        // Create client configuration
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ()
            .Build ();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance (config);
        PlayGamesPlatform.Activate ();

        PlayGamesPlatform.Instance.Authenticate (SignInCallback, true);
    }

    public void SignIn () {
        if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate (SignInCallback, false);
        } else {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut ();
            googlePlayButton.sprite = signedOff;
            achievementsButton.sprite = achievementsOff;
            //  authStatus.text = "";
        }
    }

    public void SignInCallback (bool success) {
        if (success) {
            Debug.Log ("Signed in!");
            googlePlayButton.sprite = signedIn;
            achievementsButton.sprite = achievementsOn;
        } else {
            Debug.Log ("Could not Sign In!");
            googlePlayButton.sprite = signedOff;
            achievementsButton.sprite = achievementsOff;
        }
    }

    public void ShowAchievements () {
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowAchievementsUI ();
        } else {
            Debug.Log ("Cannot show Achievements, not logged in");
        }
    }

    public void ShowLeaderboards() {
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else {
          Debug.Log("Cannot show leaderxboard: not authenticated");
        }
    }
}