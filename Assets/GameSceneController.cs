using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameSceneController : AbstractSceneController {
    [SerializeField]
    private Pusher pusherPrefab = null;

    [SerializeField]
    private Text timerText = null;

    [SerializeField]
    private int gameDuration = 60;

    private float gameStartTime = 0;

    private GameObject pushRoot = null;

    public static GameSceneController instance = null;

    protected override void Awake() {
        base.Awake();
        GameSceneController.instance = this;
    }

	// Use this for initialization
	void Start() {
        this.pushRoot = GameObject.Find("PushRoot");

        this.StartGame();
	}

    void OnDestroy() {
        GameSceneController.instance = null;
    }

    private void StartGame() {
        this.gameStartTime = Time.time;

        this.timerText.text = this.gameDuration.ToString();
    }

    private void EndGame() {
        SceneManager.LoadScene(ConstantVars.RESULT_SCENE_NAME);
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
        Pusher obj = GameObject.Instantiate<Pusher>(this.pusherPrefab);
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
