using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricWolfRenderer : MonoBehaviour
{
    private readonly string[] _runDirections = { "Wolf_Run_NW", "Wolf_Run_NL", "Wolf_Run_SL", "Wolf_Run_SL", "Wolf_Run_SW", "Wolf_Run_SW", "Wolf_Run_SW", "Wolf_Run_NW" };

    private Animator _animator;

    private int _lastDirection = 0;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        _animator.SetFloat("Speed", direction.magnitude * 1f);

        directionArray = _runDirections;


        _lastDirection = DirectionToIndex(direction, 8);
        _animator.Play(directionArray[_lastDirection]);
    }

    private static int DirectionToIndex(Vector2 direction, int sliceCount)
    {

        Vector2 normDir = direction.normalized;
        float step = 360f / sliceCount;

        float halfStep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfStep;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

}
