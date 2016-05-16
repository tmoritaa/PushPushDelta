using UnityEngine;
using System.Collections;

public abstract class Monster : MonoBehaviour {
    [SerializeField]
    protected Rigidbody2D rigidBody2D = null;

    [SerializeField]
    protected float moveVelMag = 5.0f;

    [SerializeField]
    protected float moveDuration = 1.0f;
    public float MoveDuration {
        get { return this.moveDuration; }
    }

    [SerializeField]
    protected float minMoveMag = 2.0f;

    [SerializeField]
    protected float linearDrag = 10.0f;

    [SerializeField]
    protected int score = 1;

    [SerializeField]
    protected float breakDuration = 0;

    protected float breakStartTime = 0;

    protected float lastMoveTime = 0.0f;

    protected Vector2 pushForce;

    protected bool destroy = false;

    protected Animator animator;

    protected enum State {
        Idle,
        Walk,
        Break,
        Pushed,
    };

    protected State state = State.Idle;

    bool readyToPush = false;

	public void Push(Vector3 pushStartPos, float force) {
        if (!CanBePushed()) {
            return;
        }

        Vector2 deltaPos = this.gameObject.transform.localPosition - pushStartPos;

        this.pushForce = deltaPos.normalized * force;

        this.readyToPush = true;
    }

    public void Capture() {
        GameSceneController.instance.AddScore(this.score);
        this.destroy = true;
    }

    public int GetCharDirForAnim() {
        int res = 0;
        if (this.rigidBody2D.velocity.x > 0) {
            res = -1;
        } else if (this.rigidBody2D.velocity.x < 0) {
            res = 1;
        }

        return res;
    }

    protected virtual void PerformMove() {
        float angle = Random.Range(0.0f, Mathf.PI * 2.0f);
        Vector2 moveVec = new Vector2(1, 0);

        moveVec.x = moveVec.x * Mathf.Cos(angle) - moveVec.y * Mathf.Sin(angle);
        moveVec.y = moveVec.x * Mathf.Sin(angle) + moveVec.y * Mathf.Cos(angle);

        this.rigidBody2D.velocity = moveVec.normalized * this.moveVelMag;
    }

    protected virtual bool CanBePushed() {
        return true;
    }

    protected void SetAnimTrigger(string name) {
        this.animator.SetTrigger(name);
    }

    protected virtual void Start() {
        this.animator = this.GetComponent<Animator>();

        this.MoveToIdleState();
    }

    protected virtual void MoveToPushedState() {
        this.state = State.Pushed;
        this.lastMoveTime = 0;
        this.SetAnimTrigger("Pushed");
    }

    protected virtual void MoveToIdleState() {
        this.rigidBody2D.velocity = new Vector2(0, 0);
        this.SetAnimTrigger("Idle");
        this.state = State.Idle;
    }

    protected virtual void MoveToBreakState() {
        this.rigidBody2D.velocity = new Vector2(0, 0);
        this.SetAnimTrigger("Idle");
        this.state = State.Break;
        this.breakStartTime = Time.time;
    }

    protected virtual void MoveToWalkState() {
        this.state = State.Walk;
        this.lastMoveTime = Time.time;
        this.SetAnimTrigger("Walk");
        this.PerformMove();
    }

    protected virtual void HandleIdle() {
        this.MoveToWalkState();
    }

    protected virtual void HandleWalk() {
        if ((Time.time - this.lastMoveTime) >= this.moveDuration) {
            this.MoveToBreakState();
        }
    }

    protected virtual void HandleBreak() {
        if ((Time.time - this.breakStartTime) >= this.breakDuration) {
            this.MoveToIdleState();
        }
    }

    protected virtual void HandlePushed() {
        if (this.rigidBody2D.velocity.magnitude <= this.minMoveMag) {
            this.MoveToIdleState();
        }
    }

    protected void Update() {
        switch(this.state) {
            case State.Idle:
                this.HandleIdle();
                break;
            case State.Walk:
                this.HandleWalk();
                break;
            case State.Break:
                this.HandleBreak();
                break;
            case State.Pushed:
                this.HandlePushed();
                break;
        }
    }

    protected void FixedUpdate() {
        if (this.readyToPush) {
            this.MoveToPushedState();
            this.rigidBody2D.velocity = new Vector2(0, 0);
            this.rigidBody2D.drag = this.linearDrag;
            this.rigidBody2D.AddForce(this.pushForce);
            this.readyToPush = false;
        } else if (this.state != State.Pushed) {
            this.rigidBody2D.drag = 0;
        }
    }

    protected void LateUpdate() {
        if (this.destroy) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
