using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    
    Animator anim;
    private Transform _player;
    State currentState;
    private Rigidbody2D _rbody;
    private IsometricWolfRenderer _wolfRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody2D>();
        _wolfRenderer = this.GetComponent<IsometricWolfRenderer>();
        
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Patrol(this.gameObject, _wolfRenderer, _player, _rbody, GameManager.instance.GoToPoints);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
