using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Movement : MonoBehaviour
{
    private Rigidbody2D _rbody;
    private PlayerManager _playerManager;
    private IsometricRenderer _isoRenderer;


    #region "STATS"
    [SerializeField]
    float MAX_VELOCITY_X = 10.0f;
    [SerializeField]
    float MAX_VELOCITY_Y = 10.0f;

    [SerializeField]
    float _MIN_VELOCITY_X = 1.0f;
    [SerializeField]
    float _MIN_VELOCITY_Y = 1.0f;
    #endregion  

    int lastDirection;
    

    private void Start()
    {
        _rbody = this.GetComponent<Rigidbody2D>();
        _playerManager = this.GetComponent<PlayerManager>();
        _isoRenderer = this.GetComponent<IsometricRenderer>();
    }

    private void FixedUpdate()
    {
        CalculateMovement();

    }

    

    public void CalculateMovement()
    {
        if (_playerManager.PlayerState == PlayerState.RUNNING)
        {
            float xInput = InputController.movingAxisX;
            float yInput = InputController.movingAxisY;

            Vector2 vecAc = new Vector2(xInput, yInput);
            
            _rbody.AddForce(vecAc);

            Vector2 dir = _rbody.velocity.normalized;

            Vector2 clampedVelocity = clampVelocity(_rbody.velocity);


            clampedVelocity.x = Mathf.Sign(dir.x) != Mathf.Sign(xInput) ? _MIN_VELOCITY_X : clampedVelocity.x;
            clampedVelocity.y = Mathf.Sign(dir.y) != Mathf.Sign(yInput) ? _MIN_VELOCITY_Y : clampedVelocity.y;


            _rbody.velocity = new Vector2(clampedVelocity.x * xInput, clampedVelocity.y * yInput);

        }

        _isoRenderer.SetDirection(_rbody.velocity);
      
        
    }

    private Vector2 clampVelocity(Vector2 velocity)
    {
        Vector2 newVelocity = Vector2.zero;
        newVelocity.x = Mathf.Clamp(Mathf.Abs(velocity.x), _MIN_VELOCITY_X, MAX_VELOCITY_X);
        newVelocity.y = Mathf.Clamp(Mathf.Abs(velocity.y), _MIN_VELOCITY_Y, MAX_VELOCITY_Y);

        return newVelocity;
    }
}
