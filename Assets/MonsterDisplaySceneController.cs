using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterDisplaySceneController : AbstractSceneController {
    [SerializeField]
    private MonsterDisplay monsterDisplayPrefab = null;

    [SerializeField]
    private float swipeMag = 10f;

    private GameObject monsterDisplayRoot;

    private List<MonsterDisplay> monsterDisplays = new List<MonsterDisplay>();

    private int curDisplayIdx = 0;

    private void UpdateGraphics() {
        for (int i = 0; i < this.monsterDisplays.Count; ++i) {
            if (this.curDisplayIdx == i) {
                this.monsterDisplays[i].gameObject.SetActive(true);
            } else {
                this.monsterDisplays[i].gameObject.SetActive(false);
            }
        }
    }

    public void GoToNextMonsterDisplay(int dir) {
        this.curDisplayIdx = (this.curDisplayIdx + Math.Sign(dir) + this.monsterDisplays.Count) % this.monsterDisplays.Count;

        this.UpdateGraphics();
    }

    // Use this for initialization
    void Start () {
        this.monsterDisplayRoot = GameObject.Find("MonsterDisplays");

        Dictionary<string, int> capturedMonsters = PlayerManager.Instance().CapturedMonsters;
        Dictionary<string, Monster> monsters = MonsterManager.Instance().MonsterDict;

        foreach (string key in capturedMonsters.Keys) {
            MonsterDisplay display = GameObject.Instantiate<MonsterDisplay>(this.monsterDisplayPrefab);
            display.InitWithMonster(monsters[key]);
            display.transform.SetParent(this.monsterDisplayRoot.transform, false);

            this.monsterDisplays.Add(display);
        }

        this.UpdateGraphics();
	}
}
