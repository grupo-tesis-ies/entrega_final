using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    public AudioClip flySoundA;
    public AudioClip flySoundB;
    private bool isMoving;
    public float force;

    private Animator animator;
    private bool isIgnoringCollisions;
    private bool isImpulseUp;
    private bool isPlaying;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.isMoving = false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.up * Time.deltaTime * GameConstants.BIRD_FLIGHT_SPEED);
        }
    }

    public void Launch()
    {
        animator.SetTrigger(GameConstants.ANIMATION_LAUNCH);
    }

    public void StartsFlying()
    {
        GameEvents.instance.StartsFlying();
        InvokeRepeating("FlySound", 0.5f, 0.9f);
        isPlaying = true;
    }

    public void SetMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    public void SetSwipe(bool swipeActive)
    {
        GetComponent<SwipeMove>().enabled = swipeActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isPlaying)
        {
            return;
        }

        if(GameConstants.TAG_OBSTACLE.Equals(other.tag) && !isIgnoringCollisions)
        {
            if(isImpulseUp)
            {
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.up * force, transform.position, ForceMode.Impulse);
                return;
            }
            StartCoroutine(AfterHit());
            GameEvents.instance.GotHit();
            animator.SetTrigger(GameConstants.ANIMATION_HIT);
        }
    }

    public void SetImpulseOn()
    {
        animator.speed = 2;
        isImpulseUp = true;
    }

    public void SetImpulseOff()
	{
		animator.speed = 1;
        isImpulseUp = false;
    }

    public void SetShieldOn()
    {
        isIgnoringCollisions = true;
    }

	public void SetShieldOff()
	{
        isIgnoringCollisions = false;
	}

    public void SetX2Off()
	{

	}

    IEnumerator AfterHit()
    {
        isIgnoringCollisions = true;
        yield return new WaitForSeconds(0.5f);
        isIgnoringCollisions = false;
    }

    void FlySound()
    {
        if(!isPlaying)
        {
            return;
        }

        SoundManager.instance.RandomizeSfx(flySoundA, flySoundB);
    }

    public void SetPlaying(bool isPlaying)
    {
        this.isPlaying = isPlaying;
    }
}
