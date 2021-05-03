using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public List<GameObject> GoToPoints = new List<GameObject>();

    public static GameManager instance;

    private double timeToSurvival = 180.0f;

    public Wave wave = Wave.FIRST;

    public UnityAction waveChange;

    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private EnemySpawner spawner;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject gameWin;


    [SerializeField]
    TMP_Text timerText;
    [SerializeField]
    TMP_Text waveText;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        waveChange += ChangeWaveText;
    }


    private void FixedUpdate()
    {
        if(player.PlayerState != PlayerState.DEAD && timeToSurvival > 0)
        {
            timeToSurvival -= Time.fixedUnscaledDeltaTime;
            if (timerText)
            {
                var ts = TimeSpan.FromSeconds(timeToSurvival);
                timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            }
           
        }
        if(timeToSurvival <= 0)
        {
            CallWin();    
        }
    }
    public void CallGameOver()
    {
        Invoke("CallGameOverScreen", 0.5f);
    }

    public void CallWin()
    {
        gameWin.SetActive(true);
    }

    private void CallGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    private void ChangeWaveText()
    {
        switch (wave)
        {
            case Wave.FIRST:
                waveText.text = "Wave: First"; 
                break;
            case Wave.SECOND:
                waveText.text = "Wave: Second";
                break;
            case Wave.THIRD:
                waveText.text = "Wave: Third";
                break;
        }
    }


    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        timeToSurvival = 180.0f;
        wave = Wave.FIRST;
        player.RestartStats();
        spawner.RestartSpawner();

        
    }
}
