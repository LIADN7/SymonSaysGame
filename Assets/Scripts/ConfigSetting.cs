using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigSetting : MonoBehaviour
{

    private String PATH = "./Assets/Scripts/ConfigFiles/json/";
    [SerializeField] private TMP_Text configText;
    [SerializeField] private TMP_Text playerNameText;
    public void getConfigSetting()
    {
        //string jsonData = File.ReadAllText(PATH+"GameConfigLevel1.json");
        //string s1 = PATH+configText.text;
        string s2 = PATH + "GameConfigLevel1.json";

        string jsonData = File.ReadAllText(s2);
        GameSettings setting = JsonConvert.DeserializeObject<GameSettings>(jsonData);
        GameManager.inst.setSetting(setting);
        GameManager.inst.setPlayerName(playerNameText.text);
        SceneManager.LoadScene("Game");

    }



}
