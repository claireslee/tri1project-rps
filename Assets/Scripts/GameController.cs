using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum HandSigns
{
    NONE,
    ROCK,
    PAPER,
    SCISSORS
}

public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite rockSprite, paperSprite, scissorsSprite;
    [SerializeField] private Image playerChoiceImage, opponentChoiceImage;

    [SerializeField] private TextMeshProUGUI infoText;

    public GameObject TitleScreen;
    public GameObject PlayerChoiceHandler;
    public GameObject Animation;

    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    private int roundCounter;

    private int playerScore;
    private int opponentScore;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI opponentScoreText;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI winnerText;

    public Button restartButton;

    private HandSigns playerChoice = HandSigns.NONE;
    private HandSigns opponentChoice = HandSigns.NONE;

    private Animation animation;

    delegate void MyDelegate();
    MyDelegate myDelegate;

    void Awake()
    {
        PlayerChoiceHandler.gameObject.SetActive(false);
        Animation.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        animation = GetComponent<Animation>();
    }

    // set choice as whatever user chooses (selectedChoice from inputcontroller = parameter handSigns)
    public void SetChoices(HandSigns handSigns)
    {
        roundCounter--;
        if (roundCounter == 0)
        {
            PlayerChoiceHandler.gameObject.SetActive(false);
            Animation.gameObject.SetActive(false);
            infoText.gameObject.SetActive(false);
            finalScoreText.gameObject.SetActive(false);
        }

        switch (handSigns)
        {
            case HandSigns.ROCK:
                playerChoiceImage.sprite = rockSprite;
                playerChoice = HandSigns.ROCK;
                break;

            case HandSigns.PAPER:
                playerChoiceImage.sprite = paperSprite;
                playerChoice = HandSigns.PAPER;
                break;

            case HandSigns.SCISSORS:
                playerChoiceImage.sprite = scissorsSprite;
                playerChoice = HandSigns.SCISSORS;
                break;
        }

        SetOpponentChoice();
        DetermineWinner();
    }

    void SetOpponentChoice() {
        int randomSign = Random.Range(0, 3);

        switch(randomSign) {
            case 0:
                opponentChoice = HandSigns.ROCK;
                opponentChoiceImage.sprite = rockSprite;
                break;

            case 1:
                opponentChoice = HandSigns.PAPER;
                opponentChoiceImage.sprite = paperSprite;
                break;

            case 2:
                opponentChoice = HandSigns.SCISSORS;
                opponentChoiceImage.sprite = scissorsSprite;
                break;
        }
    }

    void DetermineWinner() {
        // draw
        myDelegate = UpdateScore;
        if (playerChoice == opponentChoice)
        {
            infoText.text = "It's a TIE!";
            playerScore++;
            opponentScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // player won
        if (playerChoice == HandSigns.PAPER && opponentChoice == HandSigns.ROCK)
        {
            infoText.text = "You WIN!";
            playerScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // opponent won
        if (opponentChoice == HandSigns.PAPER && playerChoice == HandSigns.ROCK)
        {
            infoText.text = "You LOSE!";
            opponentScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // player won
        if (playerChoice == HandSigns.ROCK && opponentChoice == HandSigns.SCISSORS)
        {
            infoText.text = "You WIN!";
            playerScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // opponent won
        if (opponentChoice == HandSigns.ROCK && playerChoice == HandSigns.SCISSORS)
        {
            infoText.text = "You LOSE!";
            opponentScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // player won
        if (playerChoice == HandSigns.SCISSORS && opponentChoice == HandSigns.PAPER)
        {
            infoText.text = "You WIN!";
            playerScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }

        // opponent won
        if (opponentChoice == HandSigns.SCISSORS && playerChoice == HandSigns.PAPER)
        {
            infoText.text = "You LOSE!";
            opponentScore++;
            StartCoroutine(DisplayWinnerAndRestart());
            myDelegate();
            return;
        }
    }

    IEnumerator DisplayWinnerAndRestart()
    {
        if (roundCounter < 2)
        {
            yield return new WaitForSeconds(2f);
            infoText.gameObject.SetActive(false);
            PlayerChoiceHandler.gameObject.SetActive(false);
            Animation.gameObject.SetActive(false);
            finalScoreText.gameObject.SetActive(false);
            roundCounter = 0;
        }

        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        infoText.gameObject.SetActive(false);


        if (roundCounter == 0)
        {
            myDelegate = GameOver;
            myDelegate();
        }

        animation.ResetAnimations();
    }

    public void UpdateScore()
    {
        playerScoreText.text = " " + playerScore;
        opponentScoreText.text = " " + opponentScore;
    }

    public void GameOver()
    {
        finalScoreText.text = "Final Score: " + playerScore;
        finalScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        if (playerScore > opponentScore)
        {
            winnerText.text = "Winner is player!";
            winnerText.gameObject.SetActive(true);
        }

        if (playerScore < opponentScore)
        {
            winnerText.text = "Winner is opponent!";
            winnerText.gameObject.SetActive(true);
        }

        if (playerScore == opponentScore)
        {
            winnerText.text = "No winner! Tie!";
            winnerText.gameObject.SetActive(true);
        }

        restartButton.onClick.AddListener(RestartGame);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int rounds)
    {
        isGameActive = true;
        
        roundCounter = rounds + 1;

        TitleScreen.gameObject.SetActive(false);
        PlayerChoiceHandler.gameObject.SetActive(true);
        Animation.gameObject.SetActive(true);

        UpdateScore();
    }
}