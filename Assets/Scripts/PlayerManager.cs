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
    [SerializeField]
    private float _confidence_bias = 0.05f;

    private readonly Vector2 DEFAULT_POSITION = new Vector2(-3.33f, -1.53f);

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
        GameManager.instance.CallGameOver();
    }

    private void IncreaseConfidence()
    {
        ConfidenceMotion += _confidence_bias * 2 * Time.fixedDeltaTime;
        ConfidenceMotion = Mathf.Clamp(ConfidenceMotion, 0.01f, 1);
    }


    private void DecreaseConfidence()
    {
        ConfidenceMotion -= _confidence_bias * Time.fixedDeltaTime;
        ConfidenceMotion = Mathf.Clamp(ConfidenceMotion, 0.01f, 1);
    }

    public void RestartStats()
    {
        ConfidenceMotion = 1f;
        PlayerState = PlayerState.RUNNING;
        this.transform.position = DEFAULT_POSITION;
    }
}
