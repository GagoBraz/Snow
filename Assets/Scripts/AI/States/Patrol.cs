using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{


    GameObject _pointToGo;
    List<Collider2D> _colliders = new List<Collider2D>();
    Collider2D _playerCollider;

    Quaternion _currentQuartenion = Quaternion.identity;
    float _angle; 
    public Patrol(GameObject npc, IsometricWolfRenderer wolfRenderer, Transform player, Rigidbody2D rbody, List<GameObject> goToPoints):
        base(npc, wolfRenderer, player, rbody, goToPoints)
    {
        this.npc = npc;
        this._wolfRenderer = wolfRenderer;
        this.player = player;
        this._rbody = rbody;
        this.goToPoints = goToPoints;
    }


    public override void Enter()
    {
        base.Enter();
        GetColliders();
        DefinePointToGo();
    }

    public override void Update()
    {
        Movement();
        CheckDistance();
        CheckForPlayer();
    }


    private void Movement()
    {
        Quaternion lookatWP = Quaternion.LookRotation(this._pointToGo.transform.position - this.npc.transform.position);
         _currentQuartenion = Quaternion.Slerp(_currentQuartenion, lookatWP, _rotSpeed * Time.fixedDeltaTime);

        Vector2 translation = _currentQuartenion * new Vector3(0, 0, _idleVelocity * Time.fixedDeltaTime);

        this._wolfRenderer.SetDirection(translation);
        this.npc.transform.Translate(translation);
    }
    private void DefinePointToGo()
    {
        _pointToGo = this.goToPoints[Random.Range(0, goToPoints.Count)];
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(this.npc.transform.position, _pointToGo.transform.position);
        if(distance < 1f)
        {
            DefinePointToGo();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D currentCollider = _colliders.Find(x => x.name.Equals("DetectionRange"));
        if (currentCollider)
        {
            if (currentCollider.IsTouching(_playerCollider) && _playerManager.PlayerState == PlayerState.RUNNING)
            {
                nextState = new Run(npc, _wolfRenderer, player, _rbody, goToPoints);
                stage = EVENT.EXIT;
            }
        }
    }

    private void GetColliders()
    {
        this._rbody.GetAttachedColliders(_colliders);
        this._playerCollider = this.player.GetComponent<Collider2D>();
    }
}
