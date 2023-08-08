using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    

    public static GameManager inst; // Singleton instance of the GameManager
    private GameSettings setting; // Current game settings

    // Leaderboard top 3
    private Tuple<string, int>[] playerScores = new Tuple<string, int>[]
    {
            Tuple.Create("", 0),
            Tuple.Create("", 0),
            Tuple.Create("", 0),
        };
    private string playerName; // Current player's name
    private int score = 0; // Current player's score


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject); // Destroy duplicate GameManager instances
        }
        DontDestroyOnLoad(this.gameObject); // Don't destroy this instance when loading new scenes
    }

    void Start()
    {

            inst = this;
            // Default setting
            this.setting = new GameSettings();
            this.setting.NumberOfButtons = 6;
            this.setting.GameTime = 60;
            this.setting.StepPoints = 1;
            this.setting.IsRepeat = true;
            this.setting.GameSpeed = 2f;
            this.playerName = "Liad Nagi";
        

    }

    // Set the game settings
    public void setSetting(GameSettings setting)
    {
        this.setting = setting;   
    }

    // Get the number of buttons in the game
    public int getNumberOfButtons()
    {
        return this.setting.NumberOfButtons;
    }

    // Get the points for each correct input
    public int getStepPoints()
    {
        return this.setting.StepPoints;
    }

    // Get the game time limit in seconds
    public int getGameTime()
    {
        return this.setting.GameTime;
    }

    // Check if repeating the sequence is allowed
    public bool getIsRepeat()
    {
        return this.setting.IsRepeat;
    }

    // Get the game speed
    public float getGameSpeed()
    {
        return this.setting.GameSpeed;
    }

    // Get the current player's score
    public int getScore()
    {
        return this.score;
    }

    // Add points to the player's score
    public int addScore()
    {
        this.score += this.setting.StepPoints;
        return this.score;
    }

    // Reset the player's score to zero
    public void resetScore()
    {
        this.score = 0;
    }

    // Get the current player's name
    public string getPlayerName()
    {
        return this.playerName;
    }

    // Set the current player's name
    public void setPlayerName(string s)
    {
        this.playerName = s;
    }

    // Get the top 3 scores from the player leaderboard
    public Tuple<string, int>[] getAllBest3()
    {
        return playerScores;
    }

    // Add the current player's score to the leaderboard
    public void addToLeaderBoard()
    {
        Tuple<string, int> temp = Tuple.Create(this.playerName, this.score);

        for(int i = 0; i < GameManager.inst.getAllBest3().Length; i++)
        {
            if(temp.Item2 > GameManager.inst.getAllBest3()[i].Item2)
            {
                Tuple<string, int> temp2 = Tuple.Create(GameManager.inst.getAllBest3()[i].Item1, GameManager.inst.getAllBest3()[i].Item2);
                GameManager.inst.getAllBest3()[i] = temp;
                temp = temp2;
            }
        }
    }
}
