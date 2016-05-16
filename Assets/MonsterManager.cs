using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour {
    private static MonsterManager instance;
    public static MonsterManager Instance {
        get { return MonsterManager.instance; }
    }

    private Dictionary<string, Monster> monsterDict = new Dictionary<string, Monster>();
    public Dictionary<string, Monster> MonsterDict {
        get { return this.monsterDict; }
    }

    void Awake() {
        MonsterManager.instance = this;
        DontDestroyOnLoad(this.gameObject.transform);
    }

	// Use this for initialization
	void Start () {
        Monster[] monsters = Resources.LoadAll<Monster>("MonsterPrefabs");
        foreach(Monster monster in monsters) {
            this.monsterDict[monster.MonsterName] = monster;
        }
	}

    void OnDestroy() {
        MonsterManager.instance = null;
    }
}
