using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    List<Collider2D> _colliders = new List<Collider2D>();
    Collider2D _playerCollider;
    float _velocity = 5.0f;
    float _acceleration = 0.1f;

    public Run(GameObject npc, Animator anim, Transform player, Rigidbody2D rbody, List<GameObject> goToPoints) :
        base(npc, anim, player, rbody, goToPoints)
    {
        this.npc = npc;
        this.anim = anim;
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

        this.npc.transform.rotation = Quaternion.Slerp(this.npc.transform.rotation, lookatWP, _rotSpeed * Time.deltaTime);

        this.npc.transform.Translate(0, 0, _velocity * Time.deltaTime);
        _velocity += _acceleration;
    }
    private void CheckPlayer()
    {
        Collider2D currentCollider = _colliders.Find(x => x.name.Equals("DetectionRange"));
        if (currentCollider)
        {
            if (!currentCollider.IsTouching(_playerCollider))
            {
                nextState = new Patrol(npc, anim, player, _rbody, goToPoints);
                stage = EVENT.EXIT;
            }
        }
    }
}
