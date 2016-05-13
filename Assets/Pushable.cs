﻿using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    [SerializeField]
    protected Rigidbody2D rigidBody2D = null;

    [SerializeField]
    protected float moveVelMag = 5.0f;

    [SerializeField]
    protected float moveDuration = 5.0f;

    [SerializeField]
    protected float minMoveMag = 2.0f;

    [SerializeField]
    protected float linearDrag = 10.0f;

    [SerializeField]
    protected int score = 1;

    protected float lastMoveTime = 0.0f;

    protected Vector2 pushForce;

    protected bool destroy = false;

    protected Animator animator;

    protected enum PushedState {
        Unpushed,
        ReadyToPush,
        Pushed,
    };

    protected PushedState pushedState = PushedState.Unpushed;

	public void Push(Vector3 pushStartPos, float force) {
        Vector2 deltaPos = this.gameObject.transform.localPosition - pushStartPos;

        this.pushForce = deltaPos.normalized * force;

        this.pushedState = PushedState.ReadyToPush;
    }

    public void Capture() {
        GameManager.instance.AddScore(this.score);
        this.destroy = true;
    }

    protected void ReadyObjectToMove() {
        this.SetAnimTrigger("Idle");
        this.pushedState = PushedState.Unpushed;
    }

    protected void PerformMove() {
        float angle = Random.Range(0.0f, Mathf.PI * 2.0f);
        Vector2 moveVec = new Vector2(1, 0);

        moveVec.x = moveVec.x * Mathf.Cos(angle) - moveVec.y * Mathf.Sin(angle);
        moveVec.y = moveVec.x * Mathf.Sin(angle) + moveVec.y * Mathf.Cos(angle);

        this.rigidBody2D.velocity = moveVec.normalized * this.moveVelMag;

        this.lastMoveTime = Time.time;

        this.SetAnimTrigger("Walk");
    }

    public int GetCharDirForAnim() {
        return (this.rigidBody2D.velocity.x >= 0) ? -1 : 1;
    }

    protected void SetAnimTrigger(string name) {
        this.animator.SetTrigger(name);
    }

    protected void Start() {
        this.animator = this.GetComponent<Animator>();

        this.ReadyObjectToMove();
        this.PerformMove();
    }

    protected void FixedUpdate() {
        switch (this.pushedState) {
            case PushedState.ReadyToPush:
                this.rigidBody2D.velocity = new Vector2(0, 0);
                this.pushedState = PushedState.Pushed;
                this.rigidBody2D.drag = this.linearDrag;
                this.rigidBody2D.AddForce(this.pushForce);
                this.lastMoveTime = 0;
                this.SetAnimTrigger("Pushed");
                break;
            case PushedState.Unpushed:
                this.rigidBody2D.drag = 0;
                break;
        }
    }

    protected void Update() {
        if (this.rigidBody2D.velocity.magnitude <= this.minMoveMag) {
            this.ReadyObjectToMove();
        }

        if (this.pushedState == PushedState.Unpushed && (Time.time - this.lastMoveTime) >= this.moveDuration) {
            this.PerformMove();
        }
    }

    protected void LateUpdate() {
        if (this.destroy) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
