using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    public static TutorialController instance = null;

    public Sprite first;
    public Sprite second;
    public Sprite third;
    public Sprite fourth;
    public Sprite fifth;

    public GameObject tutorialCanvas;

    public Text startText;
    private Sprite actualPanel;
    private Sprite previousPanel;

    public Button returnBtn;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void SignInFinish () {
        tutorialCanvas.SetActive (true);
        returnBtn.gameObject.SetActive (false);
        instance.GetComponent<SpriteRenderer> ().sprite = first;
        actualPanel = first;
    }

    public void GoBack () {
        if (actualPanel.Equals (first)) {
            returnBtn.gameObject.SetActive (false);
        } else {
            returnBtn.gameObject.SetActive (true);
            actualPanel = previousPanel;
        }
        UpdatePanel ();
    }

    public void NextOne () {
        if (actualPanel.Equals (first)) {
            actualPanel = second;
            previousPanel = first;
        } else if (actualPanel.Equals (second)) {
            actualPanel = third;
            previousPanel = second;
        } else if (actualPanel.Equals (third)) {
            actualPanel = fourth;
            previousPanel = third;
        } else if (actualPanel.Equals (fourth)) {
            actualPanel = fifth;
            previousPanel = fourth;
            startText.text = "(Pulsa una vez mas para comenzar)";
        } else {
            tutorialCanvas.SetActive (false);
            instance.GetComponent<SpriteRenderer> ().enabled = false;
            GameEvents.instance.DisplayInstructionsOff ();
        }
        returnBtn.gameObject.SetActive (true);
        UpdatePanel ();
    }

    void UpdatePanel () {
        instance.GetComponent<SpriteRenderer> ().sprite = actualPanel;
    }
}