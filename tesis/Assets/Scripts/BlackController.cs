using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    void TriggerFade()
	{
		MenuManager menuManager = GameObject.Find("MenuManager").GetComponent < MenuManager > ();
		menuManager.StartMenu(gameObject);
	}
}
