using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpMonster : Monster {
    private bool jumping = false;

    protected override bool CanBePushed() {
        return (base.CanBePushed() && !this.jumping);
    }

    protected override void MoveToBreakState() {
        this.jumping = false;
        base.MoveToBreakState();
    }

    protected override void HandlePushed() {
        if (this.rigidBody2D.velocity.magnitude <= this.minMoveMag) {
            this.MoveToBreakState();
        }
    }

    protected override void PerformMove() {
        this.jumping = true;
        base.PerformMove();
    }
}
