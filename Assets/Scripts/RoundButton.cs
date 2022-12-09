using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundButton : MonoBehaviour
{
    private Button button;
    private GameController gameController;

    public int rounds;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameController = GameObject.Find("Game Manager").GetComponent<GameController>();
        button.onClick.AddListener(SetRound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetRound()
    {
        gameController.StartGame(rounds);
    }
}
