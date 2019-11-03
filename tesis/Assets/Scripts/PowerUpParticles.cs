using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpParticles : MonoBehaviour {

    public static PowerUpParticles instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DisableEmission();
    }

    public void SetShieldOn () {
        EnableEmission(Color.blue);
    }

    public void SetImpulseOn () {
        EnableEmission(Color.red);
    }

    public void SetX2On () {
        EnableEmission(Color.yellow);
    }

    public void DisableEmission () {
        var emission = instance.GetComponent<ParticleSystem> ().emission;
        emission.enabled = false;
    }

    private void EnableEmission(Color color) {
        var emission = instance.GetComponent<ParticleSystem> ().emission;
        emission.enabled = true;
        var mainModule = instance.GetComponent<ParticleSystem> ().main;
        mainModule.startColor = color;
    }
}