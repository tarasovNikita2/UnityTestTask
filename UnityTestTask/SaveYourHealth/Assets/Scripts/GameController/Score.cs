using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    void Start()
    {
        GetComponent<Text>().text = "0";
        
        Green.DestroyActionEvent += IncreaseScore;
        Red.DestroyActionEvent += IncreaseScore;
        Blue.DestroyActionEvent += IncreaseScore;
    }
    
    void IncreaseScore()
    {
        score++;
        GetComponent<Text>().text = score.ToString();
    }

    private void OnDestroy()
    {
        Green.DestroyActionEvent -= IncreaseScore;
        Red.DestroyActionEvent -= IncreaseScore;
        Blue.DestroyActionEvent -= IncreaseScore;
    }
}
