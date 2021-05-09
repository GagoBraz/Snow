using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    List<Collider2D> _colliders = new List<Collider2D>();
    Collider2D _playerCollider;
    float _velocity = 3.0f;
    float _acceleration = 0.0001f;
    Quaternion _currentQuartenion = Quaternion.identity;


    public Run(GameObject npc, IsometricWolfRenderer wolfRenderer, Transform player, Rigidbody2D rbody, List<GameObject> goToPoints) :
        base(npc, wolfRenderer, player, rbody, goToPoints)
    {
        this.npc = npc;
        this._wolfRenderer = wolfRenderer;
        this.player = player;
        this._rbody = rbody;
        this.goToPoints = goToPoints;
    }


    private void GetColliders()
    {
        this._rbody.GetAttachedColliders(_colliders);
        this._playerCollider = this.player.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        base.Enter();
        GetColliders();
    }


    public override void Update()
    {
        MoveToPlayer();
        CheckPlayer();
    }

    private void MoveToPlayer()
    {
        Quaternion lookatWP = Quaternion.LookRotation(this.player.transform.position - this.npc.transform.position);
        _currentQuartenion = Quaternion.Slerp(_currentQuartenion, lookatWP, _rotSpeed * Time.fixedDeltaTime);

        Vector2 translation = _currentQuartenion * new Vector3(0, 0, _velocity * Time.fixedDeltaTime);

        this._wolfRenderer.SetDirection(translation);
        this.npc.transform.Translate(translation);
        _velocity += _acceleration;
    }


    private void CheckPlayer()
    {
        Collider2D currentCollider = _colliders.Find(x => x.name.Equals("DetectionRange"));
        if (currentCollider)
        {
            if (!currentCollider.IsTouching(_playerCollider) || _playerManager.PlayerState == PlayerState.HIDING)
            {
                nextState = new Patrol(npc, _wolfRenderer, player, _rbody, goToPoints);
                stage = EVENT.EXIT;
            }
        }

        Collider2D biteCollider = _colliders.Find(x => x.name.Equals("BiteRange"));
        if (biteCollider)
        {
            if(biteCollider.IsTouching(_playerCollider) && _playerManager.PlayerState == PlayerState.RUNNING)
            {
                _playerManager.CallGameOver();
                nextState = new Patrol(npc, _wolfRenderer, player, _rbody, goToPoints);
                stage = EVENT.EXIT;
            }
        }
    }
}
