using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameSceneController : AbstractSceneController {
    [SerializeField]
    private Pusher pusherPrefab = null;

    [SerializeField]
    private Text timerText = null;

    [SerializeField]
    private int gameDuration = 60;

    private float gameStartTime = 0;

    private GameObject pushRoot = null;

    private Dictionary<string, int> capturedMonsters = new Dictionary<string, int>();

    public static GameSceneController instance = null;

    public void Capture(Monster monster) {
        if (!this.capturedMonsters.ContainsKey(monster.MonsterName)) {
            this.capturedMonsters[monster.MonsterName] = 0;
        }

        this.capturedMonsters[monster.MonsterName] += 1;
    }

    private void StartGame() {
        this.gameStartTime = Time.time;

        this.timerText.text = this.gameDuration.ToString();
    }

    private void EndGame() {
        foreach(string key in this.capturedMonsters.Keys) {
            SceneTransitionData.Instance.AddDataObj(key);
            SceneTransitionData.Instance.AddDataObj(this.capturedMonsters[key]);
        }
        
        SceneTransitionManager.Instance.LoadSceneAsRoot(ConstantVars.RESULT_SCENE_NAME);
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
        obj.transform.SetParent(this.pushRoot.transform, false);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        obj.transform.localPosition = newPos;
    }

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

    // Update is called once per frame
    void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            this.GeneratePushCircle();
        }

        this.UpdateTimer();

        this.CheckIfGameOver();
	}
}
