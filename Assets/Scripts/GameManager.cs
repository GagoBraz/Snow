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
        if(player.PlayerState != PlayerState.DEAD)
        {
            timeToSurvival -= Time.fixedUnscaledDeltaTime;
            if (timerText)
            {
                var ts = TimeSpan.FromSeconds(timeToSurvival);
                timerText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            }
        }
    }
    private void CallGameOver()
    {
        //TODO Implement Game Over Screen here


    }

    private void CallRestart()
    {
        wave = Wave.FIRST;
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
}
