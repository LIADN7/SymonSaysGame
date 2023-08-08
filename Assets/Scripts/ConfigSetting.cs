using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// The `ConfigSetting` class handles configuration settings for a game and provides methods to load game settings from XML or JSON files.
public class ConfigSetting : MonoBehaviour
{

    private String PATH = Application.streamingAssetsPath+"/";
    [SerializeField] private TMP_Dropdown fileType;
    [SerializeField] private TMP_Text configText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private GameObject SpeechBalloon;
    private string fileTypeName="json";

    void Start()
    {
        fileType.onValueChanged.AddListener(FormatDropdownValueChanged);
        this.SpeechBalloon.SetActive(false);
    }

    private void FormatDropdownValueChanged(int index)
    {
        this.fileTypeName = fileType.options[index].text;
    }

    // Gets the configuration setting based on the selected file type (XML or JSON).
    public void getConfigSetting()
    {

        if (playerNameText.text.Length > 1)
        {
            if (this.fileTypeName.Equals("xml"))
            {
                getXMLConfigSetting();
            }
            else if (this.fileTypeName.Equals("json"))
            {
                getJsonConfigSetting();
            }
        }
        else
        {
            StartCoroutine(setSpeechBalloon());
        }

    }

    private void getXMLConfigSetting()
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            string confName = configText.text.ToString();
            confName = confName.Substring(0, confName.Length - 1) + ".xml";
            xmlDoc.Load(PATH+confName);

            GameSettings setting = new GameSettings();

            XmlNode root = xmlDoc.SelectSingleNode("data");
            setting.NumberOfButtons = Convert.ToInt32(root.SelectSingleNode("NumberOfButtons").InnerText);
            setting.StepPoints = Convert.ToInt32(root.SelectSingleNode("StepPoints").InnerText);
            setting.GameTime = Convert.ToInt32(root.SelectSingleNode("GameTime").InnerText);
            setting.IsRepeat = Convert.ToBoolean(root.SelectSingleNode("IsRepeat").InnerText);
            setting.GameSpeed = Convert.ToSingle(root.SelectSingleNode("GameSpeed").InnerText);

            List<string> errors = RunTests(setting);
            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    Debug.LogError(error);
                }
            }
            else
            {
                Debug.Log("All XML tests passed successfully.");

                setSetting(setting);


            }

        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("An error occurred: \nThe file was not found\n" + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: \nThe file was not configured correctly\n" + e.Message);
        }
    }



    private void getJsonConfigSetting()
    {
        // for example confName = "GameConfigLevel1"
        string confName = configText.text.ToString();
        confName = confName.Substring(0, confName.Length-1)+".json";
        try
        {
            string jsonData = File.ReadAllText(PATH + confName);
            GameSettings setting = JsonConvert.DeserializeObject<GameSettings>(jsonData);
            List<string> errors = RunTests(setting);
            if (errors.Count > 0)
            {
                foreach (string error in errors)
                {
                    Debug.LogError(error);
                }
            }
            else
            {
                Debug.Log("All JSON tests passed successfully.");

                setSetting(setting);


            }

        }
        catch (FileNotFoundException e)
        {
            Debug.LogError("An error occurred: \nThe file was not found\n" + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: \nThe file was not configured correctly\n" + e.Message);
        }


    }

    private void setSetting(GameSettings setting)
    {
        GameManager.inst.setSetting(setting);
        GameManager.inst.setPlayerName(playerNameText.text);
        SceneManager.LoadScene("Game");
    }


    private List<string> RunTests(GameSettings settings)
    {
        List<string> errors = new List<string>();

        if (settings.NumberOfButtons < 2 || settings.NumberOfButtons > 6)
        {
            errors.Add("Invalid NumberOfButtons value. \nThe number of buttons need to be between 2 to 6");
        }

        if (settings.StepPoints < 0 || settings.StepPoints > 50)
        {
            errors.Add("Invalid StepPoints value. \nThe step points need to be between 0 to 50");
        }

        if (settings.GameTime <= 10|| settings.GameTime > 10000)
        {
            errors.Add("Invalid GameTime value. \nThe game time need to be more then 10");
        }

        if (settings.GameSpeed < 1 || settings.GameSpeed > 2.5)
        {
            errors.Add("Invalid GameSpeed value. \nThe speed need to be between 1 to 2.5");
        }
        return errors;

    }

    public void getBonusConfig()
    {
        if(playerNameText.text.Length > 1)
        {

            GameSettings setting = new GameSettings();
            setting.StepPoints = 0;
            setting.GameSpeed = 2f;
            setting.IsRepeat = true;
            setting.GameTime = 200;
            setting.NumberOfButtons = 6;
            GameManager.inst.setSetting(setting);
            GameManager.inst.setPlayerName(playerNameText.text);
            SceneManager.LoadScene("BonusGame");
        }
        else
        {
            StartCoroutine(setSpeechBalloon());
        }
    }


    IEnumerator setSpeechBalloon()
    {

        this.SpeechBalloon.SetActive(true);
        yield return new WaitForSeconds(4);
        this.SpeechBalloon.SetActive(false);
    }
}
