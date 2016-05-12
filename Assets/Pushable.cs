using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {
    [SerializeField]
    Rigidbody2D rigidBody2D = null;

	public void Push(Vector3 pushStartPos, float force) {
        Vector2 deltaPos = this.gameObject.transform.localPosition - pushStartPos;

        this.rigidBody2D.AddForce(deltaPos.normalized * force);
    }

    public void Capture() {
        Debug.Log("Captured");
        GameObject.Destroy(this.gameObject);
    }
}
