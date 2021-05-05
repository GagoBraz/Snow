using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class IsometricRenderer : MonoBehaviour
{
    private int _lastDirection = 0;

    public readonly string[] staticDirections = { "Player_Static_NW", "Player_Static_NL", "Player_Static_SL", "Player_Static_SL", "Player_Static_SW", "Player_Static_SW", "Player_Static_SW", "Player_Static_NW" };
    public readonly string[] runDirections = { "Player_Run_NW", "Player_Run_NL", "Player_Run_SL", "Player_Run_SL", "Player_Run_SW", "Player_Run_SW", "Player_Run_SW", "Player_Run_NW" };

    private Animator _animator;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }


    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;


        _animator.SetFloat("Speed", direction.magnitude*0.4f);

        if (direction.magnitude < 0.1f)
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            _lastDirection = DirectionToIndex(direction, 8);
        }

        _animator.Play(directionArray[_lastDirection]);
    }




    private static int DirectionToIndex(Vector2 direction, int sliceCount)
    {

        Vector2 normDir = direction.normalized;
        float step = 360f / sliceCount;

        float halfStep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfStep;

        if(angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

}
