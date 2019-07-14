using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public Animator titleAnimator;
    public Animator cameraAnimator;
    public Canvas menuCanvas;

    public void StartMenu(GameObject blackImage)
	{
		blackImage.SetActive(false);
		titleAnimator.SetTrigger("titleDisplay");
	}

    public void StartGame()
    {
        menuCanvas.gameObject.SetActive(false);
        cameraAnimator.SetTrigger("start");
    }
}
