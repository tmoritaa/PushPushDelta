using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private PushObject pushObjectPrefab = null;

    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private Text timerText = null;

    [SerializeField]
    private int gameDuration = 60;

    private int curScore = 0;

    private float gameStartTime = 0;

    private GameObject pushRoot = null;

    public static GameManager instance = null;

    void Awake() {
        GameManager.instance = this;
    }

	// Use this for initialization
	void Start() {
        this.pushRoot = GameObject.Find("PushRoot");
        this.scoreText.text = "Score: " + this.curScore;

        this.StartGame();
	}

    void OnDestroy() {
        GameManager.instance = null;
    }

    public void AddScore(int score) {
        this.curScore += score;
        this.scoreText.text = "Score: " + this.curScore;
    }

    private void StartGame() {
        this.gameStartTime = Time.time;

        this.timerText.text = this.gameDuration.ToString();
    }

    private void EndGame() {
        SceneTransitionData.Instance.data.Add(this.curScore);
        SceneManager.LoadScene("Result");
    }

    private void UpdateTimer() {
        int diff = (int)(Time.time - this.gameStartTime);
        this.timerText.text = Math.Max(0, (this.gameDuration - diff)).ToString();
    }

    private void CheckIfGameOver() {
        int diff = (int)(Time.time - this.gameStartTime);

        if (diff > this.gameDuration) {
            EndGame();
        }
    }

    private void GeneratePushCircle() {
        PushObject obj = GameObject.Instantiate<PushObject>(this.pushObjectPrefab);
        obj.transform.position = Input.mousePosition;
        obj.transform.SetParent(this.pushRoot.transform);
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            this.GeneratePushCircle();
        }

        this.UpdateTimer();

        this.CheckIfGameOver();
	}
}
