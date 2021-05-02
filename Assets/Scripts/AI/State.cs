using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        PATROL, RUN
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;

    protected EVENT stage;

    protected GameObject npc;
    protected Animator anim;

    protected Transform player;
    protected State nextState;

    protected Rigidbody2D _rbody;
    protected PlayerManager _playerManager;


    protected List<GameObject> goToPoints = new List<GameObject>();



    protected float _idleVelocity = 1.5f;

    protected float _rotSpeed = 100f;


    public State(GameObject npc, Animator anim, Transform player, Rigidbody2D rbody, List<GameObject> goToPoints)
    {
        this.npc = npc;
        this.anim = anim;
        this.stage = EVENT.ENTER;
        this.player = player;
        this._rbody = rbody;
        this.goToPoints = goToPoints;
        this._playerManager = player.GetComponent<PlayerManager>();
    }


    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

}
