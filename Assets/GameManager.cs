using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private PushObject pushObjectPrefab = null;

    private GameObject canvasGO = null;

	// Use this for initialization
	void Start () {
        this.canvasGO = FindObjectOfType<Canvas>().gameObject;
	}

    private void GeneratePushCircle() {
        PushObject obj = GameObject.Instantiate<PushObject>(this.pushObjectPrefab);
        obj.transform.position = Input.mousePosition;
        obj.transform.SetParent(this.canvasGO.transform);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            this.GeneratePushCircle();
        }
	}
}
