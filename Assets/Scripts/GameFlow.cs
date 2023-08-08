
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    [SerializeField] protected TMP_Text scoreText;
    [SerializeField] protected TMP_Text secondsText;
    [SerializeField] protected TMP_Text playerNameText;
    [SerializeField] protected GameObject[] Buttons;
    private LinkedList<int> gameSequence;
    private LinkedList<int> gamePlay;
    private bool isOnPlay=true; // 0 = Play sequence, 1 = player turn, 2 = in progress of play
    private float gameSpeed = 3f;

    private float SECONDS_TO_WAIT = 0.5f;
    void Start()
    {
        playerNameText.text= GameManager.inst.getPlayerName();
        GameManager.inst.resetScore();
        for (int i = GameManager.inst.getNumberOfButtons(); i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);

        }
        this.gameSequence=new LinkedList<int>();
        gameSpeed-=GameManager.inst.getGameSpeed();
        for (int i = 0; i < GameManager.inst.getNumberOfButtons(); i++)
        {
            this.Buttons[i].GetComponent<RotateAnimation>().multSpeed(GameManager.inst.getGameSpeed());

        }

        StartCoroutine(playNewSequence(SECONDS_TO_WAIT));
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void playerTurn(int i)
    {
        if (!this.isOnPlay)
        {

            StartCoroutine(callPlayerTurn(i));
        }
    }

    IEnumerator callPlayerTurn(int i)
    {
        if ( gamePlay.Count > 0 && i == gamePlay.First.Value)
        {
            this.isOnPlay = true;
            gamePlay.RemoveFirst();
            this.scoreText.text = "" + GameManager.inst.addScore();
            yield return StartCoroutine(startAnimation(i));
            this.isOnPlay = false;
            if (gamePlay.Count == 0)
            {
                StartCoroutine(playNewSequence(SECONDS_TO_WAIT));
            }
        }
        else if(i != gamePlay.First.Value)
        {
            gameOver();
        }
        

        // The sequence has finished. I add any additional code here if needed.
    }


    IEnumerator playNewSequence(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        this.isOnPlay = true;
        this.gameSequence.AddLast(getNextRand());
        StartCoroutine(PlayAllSequence());

    }

    IEnumerator PlayAllSequence()
    {
        if (GameManager.inst.getIsRepeat())
        {
            foreach (int num in gameSequence)
            {
                yield return StartCoroutine(startAnimation(num));
            }
        }
        else
        {
            yield return StartCoroutine(startAnimation(gameSequence.Last.Value));
        }
        gamePlay = new LinkedList<int>(gameSequence);
        this.isOnPlay = false;
    }

    IEnumerator startAnimation(int i)
    {
        this.Buttons[i].GetComponent<RotateAnimation>().setRotateAnim(true);
        yield return new WaitForSeconds(this.gameSpeed);
        this.Buttons[i].GetComponent<RotateAnimation>().setRotateAnim(false);
        this.Buttons[i].GetComponent<RotateAnimation>().ResetRotationToZero();




    }


    private int getNextRand()
    {

        int max =  GameManager.inst.getNumberOfButtons();
        return UnityEngine.Random.Range(0, max) ;
    }


    IEnumerator StartCountdown()
    {
        int timeRemains = GameManager.inst.getGameTime();

        while (timeRemains > 0)
        {
            secondsText.text = timeRemains.ToString();
            yield return new WaitForSeconds(1);
            timeRemains--;
        }

        // Countdown finished, show 0 seconds
        secondsText.text = "0";
        gameOver();
    }


    private void gameOver()
    {
        GameManager.inst.addToLeaderBoard();
        SceneManager.LoadScene("LeadBoard");

    }

    // Add to sorted leaderboard 



}
