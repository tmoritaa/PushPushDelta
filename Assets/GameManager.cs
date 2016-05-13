using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private PushObject pushObjectPrefab = null;

    [SerializeField]
    private Text scoreText = null;

    private int curScore = 0;

    private GameObject screenSpaceCanvasGO = null;

    public static GameManager instance = null;

    void Awake() {
        GameManager.instance = this;
    }

	// Use this for initialization
	void Start() {
        this.screenSpaceCanvasGO = FindObjectOfType<Canvas>().gameObject;

        this.scoreText.text = "Score: " + this.curScore;
	}

    public void AddScore(int score) {
        this.curScore += score;
        this.scoreText.text = "Score: " + this.curScore;
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
