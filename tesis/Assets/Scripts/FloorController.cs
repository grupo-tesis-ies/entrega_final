using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public float floorSpeed;
    private bool scrollingDown = false;

    public void MoveDown()
    {
        scrollingDown = true;
        StartCoroutine(DestroyThis());
    }

    void Update()
    {
        if(scrollingDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * floorSpeed);
        }
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
