using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public Animator wordsAnimator;

    public void TriggerWords()
    {
        wordsAnimator.SetTrigger("words");
    }
}
