using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetObject : MonoBehaviour {
    List<Monster> objToCapture = new List<Monster>();

    [SerializeField]
    private float cooldownDuration = 1;

    private Animator animator;

    private float captureStartTime = 0;

    private bool capturing = false;

    public void CaptureObjects() {
        if (this.capturing) {
            return;
        }

        foreach(Monster p in this.objToCapture) {
            p.Capture();
        }

        this.objToCapture.Clear();

        this.capturing = true;
        this.captureStartTime = Time.time;
        this.animator.SetTrigger("Capture");
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pushable") {
            this.objToCapture.Add(other.GetComponent<Monster>());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Pushable") {
            this.objToCapture.Remove(other.GetComponent<Monster>());
        }
    }

    void Start() {
        this.animator = this.GetComponent<Animator>();
    }

    void Update() {
        if (this.capturing && (Time.time - this.captureStartTime) >= this.cooldownDuration) {
            this.animator.SetTrigger("Idle");
            this.capturing = false;
        }
    }
}
