using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Animation animation;
    private GameController gameController;

    private string playersChoice;

    void Awake()
    {
        animation = GetComponent<Animation>();
        gameController = GetComponent<GameController>();
    }

    public void GetChoice()
    {
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        HandSigns selectedChoice = HandSigns.NONE;

        switch(choiceName)
        {
            case "Rock":
                selectedChoice = HandSigns.ROCK;
                break;

            case "Paper":
                selectedChoice = HandSigns.PAPER;
                break;

            case "Scissors":
                selectedChoice = HandSigns.SCISSORS;
                break;
        }

        gameController.SetChoices(selectedChoice);
        animation.PlayerMadeChoice();
    }
}
