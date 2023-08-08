
using UnityEngine;

public class BonusGameSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource BG;
    [SerializeField] private AudioSource lastCall;
    [SerializeField] private GameObject BonusFlowObj;
    [SerializeField] private GameObject timerObj;
    private bool flag=true;


    void Update()
    { 
        if(flag && BonusFlowObj.GetComponent<BonusGameFlow>().getTimeRemains() < 10)
        {
            lastCall.Play();
            lastCall.loop = true;
            timerObj.GetComponent<BlinkEffect>().StartBlinkEffect();
            flag = false;
        }
        
    }
}
