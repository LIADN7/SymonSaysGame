
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusGameFlow : MonoBehaviour
{
    // Serialized fields for UI elements and variables
    [SerializeField] protected TMP_Text scoreText; // Text element to display the player's score
    [SerializeField] protected TMP_Text secondsText; // Text element to display remaining seconds
    [SerializeField] protected TMP_Text playerNameText; // Text element to display the player's name
    [SerializeField] protected GameObject[] Buttons; // Array of GameObjects representing buttons
    private LinkedList<int> gameSequence; // Linked list to store the game sequence
    private LinkedList<int> gamePlay; // Linked list to store the current player's input sequence
    private bool isOnPlay = true; // 
    private float gameSpeed = 3f; // Speed of button rotation animation
    private float SECONDS_TO_WAIT = 0.5f; // Time to wait before starting a new sequence
    private int timeRemains=160;


    void Start()
    {
        playerNameText.text = GameManager.inst.getPlayerName(); // Set the player's name on UI
        GameManager.inst.resetScore(); // Reset the player's score
        this.gameSequence = new LinkedList<int>(); // Initialize the game sequence linked list
        gameSpeed -= GameManager.inst.getGameSpeed(); // Adjust game speed based on configuration
        for (int i = 0; i < GameManager.inst.getNumberOfButtons(); i++)
        {
            this.Buttons[i].GetComponent<RotateAnimation>().multSpeed(GameManager.inst.getGameSpeed()); // Adjust rotation speed of buttons
        }
        StartCoroutine(playNewSequence(SECONDS_TO_WAIT)); // Start playing a new sequence
        StartCoroutine(StartCountdown()); // Start the countdown timer
    }



    // Called when a button is clicked during player's turn
    public void playerTurn(int i)
    {
        if (!this.isOnPlay)
        {
            StartCoroutine(callPlayerTurn(i)); // Start player's turn coroutine
        }
    }

    // Coroutine to handle player's turn
    IEnumerator callPlayerTurn(int i)
    {
        if (gamePlay.Count > 0 && i == gamePlay.First.Value)
        {
            this.isOnPlay = true;
            gamePlay.RemoveFirst(); // Remove the correct input from the sequence
            this.scoreText.text = "" + GameManager.inst.addScore(); // Update the score UI
            yield return StartCoroutine(startAnimation(i)); // Start button animation
            this.isOnPlay = false;
            if (gamePlay.Count == 0)
            {
                StartCoroutine(gameOver());
            }
        }
        else if(i != gamePlay.First.Value)
        {
            StartCoroutine(gameOver()); // Game over if wrong input
        }
        
    }


    // Coroutine to play a new sequence
    IEnumerator playNewSequence(float waitSec)
    {
        this.isOnPlay = true;
        yield return new WaitForSeconds(waitSec);
        createSongSequence(); // Create song the sequence
        
        StartCoroutine(PlayAllSequence()); // Start playing the entire sequence
    }
    

    private void createSongSequence()
    {
        // pi - ka - chu
        this.gameSequence.AddLast(0);
        this.gameSequence.AddLast(1);
        this.gameSequence.AddLast(2);

        // pi - ka - chu
        this.gameSequence.AddLast(0);
        this.gameSequence.AddLast(1);
        this.gameSequence.AddLast(2);

        // pi - ka
        this.gameSequence.AddLast(0);
        this.gameSequence.AddLast(1);

        // pi - ka
        this.gameSequence.AddLast(0);
        this.gameSequence.AddLast(1);

        // pi - ka - chu
        this.gameSequence.AddLast(0);
        this.gameSequence.AddLast(1);
        this.gameSequence.AddLast(2);

        // pikachu
        this.gameSequence.AddLast(5);

        // pika pikachu
        this.gameSequence.AddLast(3);
        this.gameSequence.AddLast(5);

        // pika pikachu
        this.gameSequence.AddLast(3);
        this.gameSequence.AddLast(5);

        // pikaaaaa pikachu
        this.gameSequence.AddLast(4);
        this.gameSequence.AddLast(5);
    }


    // Coroutine to play the entire game sequence
    IEnumerator PlayAllSequence()
    {
        if (GameManager.inst.getIsRepeat())
        {
            foreach (int num in gameSequence)
            {
                yield return StartCoroutine(startAnimation(num)); // Start button animation for each input in the sequence
            }
        }
        else
        {
            yield return StartCoroutine(startAnimation(gameSequence.Last.Value)); // Start button animation for the last input in the sequence
        }
        gamePlay = new LinkedList<int>(gameSequence); // Store the sequence for player's turn
        this.isOnPlay = false;
    }

    // Coroutine to animate button rotation
    IEnumerator startAnimation(int i)
    {
        this.Buttons[i].GetComponent<RotateAnimation>().setRotateAnim(true);
        yield return new WaitForSeconds(this.gameSpeed);
        this.Buttons[i].GetComponent<RotateAnimation>().setRotateAnim(false);
        this.Buttons[i].GetComponent<RotateAnimation>().ResetRotationToZero();




    }


    // Coroutine to start the countdown timer
    IEnumerator StartCountdown()
    {
        this.timeRemains = GameManager.inst.getGameTime();

        while (timeRemains > 0)
        {
            secondsText.text = timeRemains.ToString(); // Update remaining seconds UI
            yield return new WaitForSeconds(1);
            timeRemains--;
        }

        // Countdown finished, show 0 seconds
        secondsText.text = "0";
        StartCoroutine(gameOver()); // End the game when the countdown timer finishes
    }

    public int getTimeRemains()
    {
        return this.timeRemains;
    }
    IEnumerator gameOver()
    {
        GameManager.inst.addToLeaderBoard();
        for (int i = 0; i < GameManager.inst.getNumberOfButtons(); i++)
        {
            this.Buttons[i].SetActive(false); // Hide all the buttons
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LeadBoard");

    }


}
