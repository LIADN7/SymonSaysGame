using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadboardManger : MonoBehaviour
{

    [SerializeField] TMP_Text[] Names;
    [SerializeField] TMP_Text[] Score;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (Tuple<string, int> playerScore in GameManager.inst.getAllBest3())
        {
            if(i< Names.Length&& i < Names.Length)
            {
                Names[i].text = playerScore.Item1;
                Score[i].text = ""+playerScore.Item2;
                i++;
            }
            
        }
    }

}
