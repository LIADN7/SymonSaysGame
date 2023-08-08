using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpClick : MonoBehaviour
{
    [SerializeField] protected GameObject XButton;
    [SerializeField] protected GameObject HelpButton;
    [SerializeField] protected GameObject LeadButton;
    [SerializeField] protected GameObject CanvasMain;
    [SerializeField] protected GameObject CanvasHelp;

    void Start()
    {
        bool flag = false;
        HelpButton.SetActive(!flag);
        CanvasMain.SetActive(!flag);
        LeadButton.SetActive(!flag);
        XButton.SetActive(flag);
        CanvasHelp.SetActive(flag);
    }

    public void setHelpPage()
    {

        bool flag = HelpButton.activeSelf; 
        HelpButton.SetActive(!flag);
        CanvasMain.SetActive(!flag);
        LeadButton.SetActive(!flag);
        XButton.SetActive(flag);
        CanvasHelp.SetActive(flag);
    }

}
