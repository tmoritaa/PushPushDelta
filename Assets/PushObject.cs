using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PushObject : MonoBehaviour {
    [SerializeField]
    private Image refCircle = null;

    [SerializeField]
    private Collider2D colliderComponent = null;

    [SerializeField]
    private float targetTimeDuration = 2;

    [SerializeField]
    private float pushForce = 50000;

    private List<Pushable> objectsToPush = new List<Pushable>();

    private float startTime;

    int destroyCounter = 0;

	// Use this for initialization
	void Start () {
        this.startTime = Time.time;
            
        this.refCircle.transform.localScale = new Vector3(0, 0, 1);
    }

    private void UpdateGraphics() {
        float deltaTime = Time.time - this.startTime;

        float ratio = deltaTime / this.targetTimeDuration;

        this.refCircle.transform.localScale = new Vector3(ratio, ratio, 1);
    }

    private void PushObjects() {
        float deltaTime = Time.time - this.startTime;
        float ratio = deltaTime / this.targetTimeDuration;

        if (ratio > 1.0f) {
            ratio = 1.0f - (ratio - 1.0f);
        }

        foreach (Pushable p in this.objectsToPush) {
            p.Push(this.gameObject.transform.localPosition, this.pushForce * ratio);
        }

        this.objectsToPush.Clear();
    }
	
	// Update is called once per frame
	void Update () {
        this.UpdateGraphics();

        if (this.destroyCounter != 0) {
            if (this.destroyCounter > 3) {
                this.colliderComponent.enabled = false;
                this.destroyCounter = 0;
                this.PushObjects();
                GameObject.Destroy(this.gameObject);
            }

            this.destroyCounter += 1;
        }

	    if (Input.GetMouseButtonUp(0)) {
            this.colliderComponent.enabled = true;
            this.destroyCounter = 1;
        }
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pushable") {
            this.objectsToPush.Add(other.GetComponent<Pushable>());
        }
    }
}
