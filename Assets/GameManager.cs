using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private PushObject pushObjectPrefab = null;

    private GameObject screenSpaceCanvasGO = null;

	// Use this for initialization
	void Start () {
        this.screenSpaceCanvasGO = FindObjectOfType<Canvas>().gameObject;
	}

    private void GeneratePushCircle() {
        PushObject obj = GameObject.Instantiate<PushObject>(this.pushObjectPrefab);
        obj.transform.position = Input.mousePosition;
        obj.transform.SetParent(this.screenSpaceCanvasGO.transform);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            this.GeneratePushCircle();
        }
	}
}
