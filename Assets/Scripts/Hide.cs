using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{

    PlayerManager _currentManager;
    bool _ableToHide = false;
    Transform _currentTarget = null;

    float _secondsToWait = 0.0f;
    [SerializeField]
    float maxSecondsToWait = 4.0f;

    [SerializeField]
    BarGame hideBar;

    // Start is called before the first frame update
    void Start()
    {
        _currentManager = this.GetComponent<PlayerManager>();
        hideBar.onMiniGameLose += DisableHideGame;
       
    }

    private void FixedUpdate()
    {
        HideAction();
    }

    private void HideAction()
    {
        if (InputController.hidingControl)
        {
            if (_ableToHide)
            {
                if(_secondsToWait > maxSecondsToWait)
                {
                    ChangeCurrentState();
                    _secondsToWait = 0.0f;
                }
            }
        }
        _secondsToWait += Time.fixedDeltaTime;
    }

    private void ChangeCurrentState()
    {
        if (_currentManager.PlayerState == PlayerState.RUNNING)
        {   
            EnableHideGame();
        }
        else
        {
            DisableHideGame();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Place"))
        {
            _ableToHide = true;
            _currentTarget = collision.gameObject.transform.Find("HidingSpot");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Place"))
        {
            _ableToHide = false;
            
        }
    }

    private void EnableHideGame()
    {
        _currentManager.PlayerState = PlayerState.HIDING;
        this.transform.position = _currentTarget.position;
        hideBar.gameObject.SetActive(true);
    }

    private void DisableHideGame()
    {
        hideBar.gameObject.SetActive(false);
        _currentManager.PlayerState = PlayerState.RUNNING;
    }
}
