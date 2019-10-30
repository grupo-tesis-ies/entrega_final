using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour {

    public static MainCharacterController instance = null;
    public AudioClip flySoundA;
    public AudioClip flySoundB;
    private bool isMoving;
    public float force;

    private bool isIgnoringCollisions;
    private bool isImpulseUp;
    private bool isPlaying;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        this.isMoving = false;
    }

    void FixedUpdate () {
        if (isMoving) {
            instance.transform.Translate (Vector3.up * Time.deltaTime * GameConstants.BIRD_FLIGHT_SPEED);
        }
    }

    public void Launch () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_LAUNCH);
    }

    public void StartsFlying () {
        GameEvents.instance.StartsFlying ();
        InvokeRepeating ("FlySound", 0.5f, 0.95f);
        instance.isPlaying = true;
    }

    public void SetMoving (bool isMoving) {
        instance.isMoving = isMoving;
    }

    public void SetSwipe (bool swipeActive) {
        instance.GetComponent<SwipeMove> ().enabled = swipeActive;
    }

    private void OnTriggerEnter (Collider other) {
        if (!instance.isPlaying) {
            return;
        }

        if (GameConstants.TAG_OBSTACLE.Equals (other.tag) && !instance.isIgnoringCollisions) {
            if (isImpulseUp) {
                other.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody> ().AddForceAtPosition (Vector3.up * force, transform.position, ForceMode.Impulse);
                other.gameObject.GetComponent<Obstacle>().SetDropping(true);
                return;
            }
            if("berry".Equals(other.gameObject.GetComponent<Obstacle>().GetName())) {
                GetComponent<SwipeMove>().SlowDown();
            }
            StartCoroutine (AfterHit ());
            GameEvents.instance.GotHit (other.GetComponent<Obstacle>().GetName());
            instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_HIT);
        }
    }

    public void SetImpulseOn () {
        instance.GetComponent<Animator>().speed = 2;
        instance.isImpulseUp = true;
    }

    public void SetImpulseOff () {
        instance.GetComponent<Animator>().speed = 1;
        instance.isImpulseUp = false;
    }

    public void SetShieldOn () {
        instance.isIgnoringCollisions = true;
    }

    public void SetShieldOff () {
        instance.isIgnoringCollisions = false;
    }

    IEnumerator AfterHit () {
        instance.isIgnoringCollisions = true;
        yield return new WaitForSeconds (0.5f);
        instance.isIgnoringCollisions = false;
    }

    void FlySound () {
        if (!instance.isPlaying) {
            return;
        }

        SoundManager.instance.RandomizeSfx (flySoundA, flySoundB);
    }

    public void SetPlaying (bool isPlaying) {
        this.isPlaying = isPlaying;
    }
}