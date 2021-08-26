using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject buster;
    public GameObject[] Viruses;
        
    private Vector2 spawnPlace;
    private float spawnRate = 1.5f;
    private float spawnTime = 0.0f;
    private float[] xSpawnPos = { -2f, 0f, 2f };
    private float[] ySpawnPos = { -4f, -1f, 2f };
    private int virusCounter = 0;
    private int destroyCouner = 0;
    private int score = 0;
    private int maxVirusesIndex = 1;
    private bool BusterOnTheField = false;
    
    void Start()
    {
        Buster.BusterActivateEvent += ChangeBusterFlag;
        Green.DuplicateActionEvent += IncreaseCounter;
        Green.DestroyActionEvent += DecreaseCounter;
        Red.DestroyActionEvent += DecreaseCounter;
        Blue.DestroyActionEvent += DecreaseCounter;
    }
    
    void FixedUpdate()
    {
        if (Time.time > spawnTime && virusCounter < 10)
        {
            spawnTime = Time.time + spawnRate;
            float x = xSpawnPos[Random.Range(0, xSpawnPos.Length)];
            float y = xSpawnPos[Random.Range(0, ySpawnPos.Length)];
            spawnPlace = new Vector2(x, y);
            int index = Random.Range(0, maxVirusesIndex);
            Instantiate(Viruses[index], spawnPlace, Quaternion.identity);
            virusCounter++;
        }

        if (virusCounter > 9)
        {
            if (PlayerPrefs.GetInt("Score") < score)
            {
                PlayerPrefs.SetInt("Score", score);
            }
            
            PlayerPrefs.SetInt("CurrentScore", 0);
            SceneManager.LoadScene(0);
        }

        if (destroyCouner == 10)
        {
            if (maxVirusesIndex != 4)
            {
                maxVirusesIndex++;
            }

            spawnRate -= 0.2f;
            destroyCouner = 0;

            if (!BusterOnTheField)
            {
                Instantiate(buster, new Vector2(-2f, 4f), Quaternion.identity);
                BusterOnTheField = true;
            }
        }
    }

    void ChangeBusterFlag()
    {
        BusterOnTheField = false;
    }
    void DecreaseCounter()
    {
        virusCounter--;
        destroyCouner++;
        PlayerPrefs.SetInt("CurrentScore", ++score);
    }

    void IncreaseCounter()
    {
        virusCounter++;
    }

    private void OnDestroy()
    {
        Buster.BusterActivateEvent -= ChangeBusterFlag;
        Green.DuplicateActionEvent -= IncreaseCounter;
        Green.DestroyActionEvent -= DecreaseCounter;
        Red.DestroyActionEvent -= DecreaseCounter;
        Blue.DestroyActionEvent -= DecreaseCounter;
    }
}
