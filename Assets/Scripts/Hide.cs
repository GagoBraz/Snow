using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{

    PlayerManager _currentManager;
    bool _ableToHide = false;
    Transform _currentTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        _currentManager = this.GetComponent<PlayerManager>();
       
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
                ChangeCurrentState();       
            }
        }
    }

    private void ChangeCurrentState()
    {
        if (_currentManager.PlayerState == PlayerState.RUNNING)
        {
            _currentManager.PlayerState = PlayerState.HIDING;
           
            this.transform.position = _currentTarget.position;
        }
        else
        {
            _currentManager.PlayerState = PlayerState.RUNNING;
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
}
