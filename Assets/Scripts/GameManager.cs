using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> GoToPoints = new List<GameObject>();

    public static GameManager instance;

    private float timeToSurvival = 180.0f;

    [SerializeField]
    private PlayerManager player;

    private void Awake()
    {
        instance = this;
    }


    private void FixedUpdate()
    {
        if(player.PlayerState != PlayerState.DEAD)
        {
            timeToSurvival -= Time.fixedUnscaledDeltaTime;
        }
    }
    private void CallGameOver()
    {
        //TODO Implement Game Over Screen here


    }


}
