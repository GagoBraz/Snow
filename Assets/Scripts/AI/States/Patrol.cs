using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{


    GameObject _pointToGo;
    List<Collider2D> _colliders = new List<Collider2D>();
    Collider2D _playerCollider;
    float _angle; 
    public Patrol(GameObject npc, Animator anim, Transform player, Rigidbody2D rbody, List<GameObject> goToPoints):
        base(npc, anim, player, rbody, goToPoints)
    {
        this.npc = npc;
        this.anim = anim;
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
        var rotation = Quaternion.Slerp(this.npc.transform.rotation, lookatWP, _rotSpeed * Time.deltaTime);


        this.npc.transform.rotation = rotation;
        

        this.npc.transform.Translate(0, 0, _idleVelocity * Time.deltaTime);
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
                nextState = new Run(npc, anim, player, _rbody, goToPoints);
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
