using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region player_stats
    public float ConfidenceMotion { get; private set; } = 1f;
    [HideInInspector]
    public PlayerState PlayerState = PlayerState.RUNNING;
    #endregion

    private float _confidence_bias = 0.0001f;

    private void FixedUpdate()
    {
        CheckState();
    }

    public void CheckState()
    {
        switch (PlayerState)
        {
            case PlayerState.RUNNING:
                IncreaseConfidence();
                break;
            case PlayerState.HIDING:
                DecreaseConfidence();
                break;
        }
    }

    public void CallGameOver()
    {
        Debug.Log("Game over!");
        PlayerState = PlayerState.DEAD;
    }

    private void IncreaseConfidence()
    {
        ConfidenceMotion += _confidence_bias * 2 * Time.fixedDeltaTime;
        ConfidenceMotion = Mathf.Clamp(ConfidenceMotion, 0.2f, 1);
    }


    private void DecreaseConfidence()
    {
        ConfidenceMotion -= _confidence_bias * Time.fixedDeltaTime;
        ConfidenceMotion = Mathf.Clamp(ConfidenceMotion, 0.2f, 1);
    }
}
