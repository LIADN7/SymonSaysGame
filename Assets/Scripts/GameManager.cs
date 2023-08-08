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
    

    public static GameManager inst;
    private GameSettings setting;
    private Tuple<string, int>[] playerScores = new Tuple<string, int>[]
    {
            Tuple.Create("", 0),
            Tuple.Create("", 0),
            Tuple.Create("", 0),
        };
    private string playerName;
    private int score=0;


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

            inst = this;
            // Default setting
            this.setting = new GameSettings();
            this.setting.NumberOfButtons = 4;
            this.setting.GameTime = 60;
            this.setting.StepPoints = 1;
            this.setting.IsRepeat = false;
            this.setting.GameSpeed = 2f;
            this.playerName = "Liad Nagi";
        

    }




    public void setSetting(GameSettings setting)
    {
        this.setting = setting;   
    }


    public int getNumberOfButtons() {
        return this.setting.NumberOfButtons;
    }

    public int getStepPoints()
    {
        return this.setting.StepPoints;
    }
    public int getGameTime()
    {
        return this.setting.GameTime;
    }
    public bool getIsRepeat()
    {
        return this.setting.IsRepeat;
    }
    public float getGameSpeed()
    {
        return this.setting.GameSpeed;
    }

    public int getScore()
    {
        return this.score;
    }


    public int addScore()
    {
        this.score+=this.setting.StepPoints;
        return this.score;
    }

    public void resetScore()
    {
        this.score =0;
    }

    public string getPlayerName()
    {
        return this.playerName;
    }


    public void setPlayerName(string s)
    {
        this.playerName = s;
    }

    public Tuple<string, int>[] getAllBest3()
    {
        return playerScores;
    }
    public void addToLeaderBoard()
    {
        Tuple<string, int> temp = Tuple.Create(this.playerName, this.score);

        for(int i=0;i< GameManager.inst.getAllBest3().Length;i++)
        {
            if(temp.Item2> GameManager.inst.getAllBest3()[i].Item2)
            {
                Tuple<string, int> temp2 = Tuple.Create(GameManager.inst.getAllBest3()[i].Item1, GameManager.inst.getAllBest3()[i].Item2);
                GameManager.inst.getAllBest3()[i] = temp;
                temp = temp2;
            }
        }





    }
}
