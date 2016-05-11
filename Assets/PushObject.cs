using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushObject : MonoBehaviour {
    [SerializeField]
    private Image refCircle = null;

    [SerializeField]
    private float targetTimeDuration = 2;

    private float startTime;

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
	
	// Update is called once per frame
	void Update () {
        this.UpdateGraphics();

	    if (Input.GetMouseButtonUp(0)) {
            GameObject.Destroy(this.gameObject);
        }
	}
}
