using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetObject : MonoBehaviour {
    List<Pushable> objToCapture = new List<Pushable>();

    public void CaptureObjects() {
        foreach(Pushable p in this.objToCapture) {
            p.Capture();
        }

        this.objToCapture.Clear();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Pushable") {
            this.objToCapture.Add(other.GetComponent<Pushable>());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Pushable") {
            this.objToCapture.Remove(other.GetComponent<Pushable>());
        }
    }
}
