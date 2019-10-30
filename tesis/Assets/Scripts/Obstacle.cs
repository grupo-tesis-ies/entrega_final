using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public string obsName;

    private bool isDropping;

    public string GetName () {
        return obsName;
    }

    public void SetDropping(bool isDropping) {
        this.isDropping = isDropping;
    }

    public bool IsDropping() {
        return isDropping;
    }
}