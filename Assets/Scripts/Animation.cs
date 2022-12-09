using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    [SerializeField] private Animator playerChoiceHandlerAnimation;
    [SerializeField] private Animator choiceAnimation;

    // play animationName
    public void ResetAnimations() {
        playerChoiceHandlerAnimation.Play("ShowHandler");
        choiceAnimation.Play("RemoveChoices");
    }

    public void PlayerMadeChoice()
    {
        playerChoiceHandlerAnimation.Play("RemoveHandler");
        choiceAnimation.Play("ShowChoices");
    }
}
