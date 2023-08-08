using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource BG;
    [SerializeField] private AudioSource lastCall;
    [SerializeField] private GameObject flowObj;
    [SerializeField] private GameObject timerObj;
    private bool flag=true;


    void Update()
    { 
        if(flag && flowObj.GetComponent<GameFlow>().getTimeRemains() < 10)
        {
            lastCall.Play();
            lastCall.loop = true;
            timerObj.GetComponent<BlinkEffect>().StartBlinkEffect();
            flag = false;
        }
        
    }
}
