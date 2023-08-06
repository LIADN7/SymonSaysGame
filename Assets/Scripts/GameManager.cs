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
    private string playerName;
    private int score=0;


    private void Awake()
    {

    }

    void Start()
    {
        if (inst == null)
        {
            Debug.Log("aaaaaaaaaaa");
            inst = this;
            // Default setting
            this.setting = new GameSettings();
            this.setting.NumberOfButtons = 4;
            this.setting.GameTime = 60;
            this.setting.StepPoints = 1;
            this.setting.IsRepeat = true;
            this.setting.GameSpeed = 1f;
            this.playerName = "Liad Nagi";
        }
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

    public string getPlayerName()
    {
        return this.playerName;
    }


    public void setPlayerName(string s)
    {
        this.playerName = s;
    }
}
