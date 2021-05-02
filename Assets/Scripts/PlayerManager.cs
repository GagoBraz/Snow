using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector]
    public PlayerState PlayerState = PlayerState.RUNNING;


    public void CallGameOver()
    {
        Debug.Log("Game over!");
        PlayerState = PlayerState.DEAD;
    }
}
