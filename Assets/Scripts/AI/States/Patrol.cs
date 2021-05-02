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


        //Quaternion rotating = Quaternion.LookRotation(new Vector2(this._pointToGo.transform.position.x, this._pointToGo.transform.position.y) - new Vector2(this.npc.transform.position.x, this.npc.transform.position.y));



        //float newZValue = Mathf.Lerp(this.npc.transform.eulerAngles.z, rotating.eulerAngles.y, Time.fixedDeltaTime * _rotSpeed);


        //Debug.DrawRay(this.npc.transform.position, this._pointToGo.transform.position);
        //Debug.Log(rotating.eulerAngles);

        //Debug.Log(newZValue);
        //Quaternion newQuat = Quaternion.Euler(
        //this.npc.transform.eulerAngles.x, this.npc.transform.eulerAngles.y, rotating.eulerAngles.z);
        //this.npc.transform.rotation = Quaternion.Slerp(this.npc.transform.rotation, rotating, Time.fixedDeltaTime * _rotSpeed);
        //this.npc.transform.rotation = Quaternion.Euler(this.npc.transform.eulerAngles.x, this.npc.transform.eulerAngles.y,  newZValue);
        //this.npc.transform.Rotate(this._pointToGo.transform.position, 5);
        //this.npc.transform.Translate(_idleVelocity * Time.fixedDeltaTime, 0, 0, Space.Self);


        // _angle = Vector2.Angle(this.npc.transform.position, this._pointToGo.transform.position);

        //if(_angle < 180f)
        // {
        //     _angle += 1f;
        // }


        // //angle = Mathf.Clamp(angle, -45f, 45f);
        // Vector2 velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * _angle), Mathf.Sin(Mathf.Deg2Rad * _angle));
        // //velocity = Quaternion.Euler(angle, 0,0) * velocity;
        // this.npc.transform.Translate(velocity);

        Quaternion lookatWP = Quaternion.LookRotation(this._pointToGo.transform.position - this.npc.transform.position);

        this.npc.transform.rotation = Quaternion.Slerp(this.npc.transform.rotation, lookatWP, _rotSpeed * Time.deltaTime);

        this.npc.transform.Translate(0, 0, _idleVelocity * Time.deltaTime);
    }
    private void DefinePointToGo()
    {

        _pointToGo = this.goToPoints[Random.Range(0, goToPoints.Count -1)];
    }

    private void CheckDistance()
    {
        float distance = Vector2.Distance(this.npc.transform.position, _pointToGo.transform.position);
        if(distance < 3f)
        {
            Debug.Log("Change position!");
            DefinePointToGo();
        }
    }

    private void CheckForPlayer()
    {
        Collider2D currentCollider = _colliders.Find(x => x.name.Equals("DetectionRange"));
        if (currentCollider)
        {
            if (currentCollider.IsTouching(_playerCollider))
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
