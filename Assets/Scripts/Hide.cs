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
    float _speed = 5.0f;

    [SerializeField]
    BarGame hideBar;

    private IsometricRenderer _isoRenderer;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _currentManager = this.GetComponent<PlayerManager>();
        _isoRenderer = this.GetComponent<IsometricRenderer>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        hideBar.onMiniGameLose += DisableHideGame;
        hideBar.onMiniGameLose += SwitchToRunState;
    }

    private void OnDestroy()
    {
        hideBar.onMiniGameLose -= DisableHideGame;
        hideBar.onMiniGameLose -= SwitchToRunState;

    }

    private void Update()
    {
        GoToSpot();
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
            SwitchToHideState();
            _isoRenderer.SetDirection(CalcDirection());
            EnableHideGame();
        }
        else
        {
            SwitchToRunState();
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
        //this.transform.position = _currentTarget.position;
        hideBar.gameObject.SetActive(true);
    }

    private void DisableHideGame()
    {
        hideBar.gameObject.SetActive(false);
    }

    private void SwitchToHideState()
    {
        _currentManager.PlayerState = PlayerState.HIDING;
    }

    private void SwitchToRunState()
    {
        _currentManager.PlayerState = PlayerState.RUNNING;
    }

    private Vector2 CalcDirection()
    {
        if (_currentTarget)
        {
            Vector2 result = this._currentTarget.position - this.transform.position;
            return result.normalized;
        }
        return Vector2.zero;
    }

    private void GoToSpot()
    {
        if (_currentManager.PlayerState == PlayerState.HIDING)
        {
            if(Vector3.Distance(this.transform.position, this._currentTarget.position) > 1f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this._currentTarget.position, Time.deltaTime * _speed);

            }
            else
            {
                if (this._spriteRenderer.enabled)
                {
                    this._spriteRenderer.enabled = false;
                }
            }
        }
        else if (_currentManager.PlayerState == PlayerState.RUNNING) {
            if (!this._spriteRenderer.enabled)
            {
                this._spriteRenderer.enabled = true;
            }
        }
    }
}
