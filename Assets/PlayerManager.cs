using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;
    public static PlayerManager Instance() {
        return PlayerManager.instance;
    }

    [SerializeField]
    MonsterMoneyGenerationData moneyGenerationData = null;

    private Dictionary<string, float> lastMoneyAddedPerMonster = new Dictionary<string, float>();

    // TODO: should be saved in player prefs.
    private int playerMoney;
    public int PlayerMoney {
        get { return this.playerMoney; }
        set { this.playerMoney = value; }
    }

    private Dictionary<string, int> capturedMonsters = new Dictionary<string, int>();
    public Dictionary<string, int> CapturedMonsters {
        get { return this.capturedMonsters; }
    }

    public void Capture(Monster monster) {
        if (!this.capturedMonsters.ContainsKey(monster.MonsterName)) {
            this.capturedMonsters[monster.MonsterName] = 0;
        }

        this.capturedMonsters[monster.MonsterName] += 1;
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform);
        PlayerManager.instance = this;
    }

    void Start() {
        Dictionary<string, MonsterMoneyGenerationData.MonsterMoneyGenerationEntry> entryDict = this.moneyGenerationData.GetEntryDict();

        foreach(MonsterMoneyGenerationData.MonsterMoneyGenerationEntry entry in entryDict.Values) {
            this.lastMoneyAddedPerMonster[entry.monster.MonsterName] = Time.time;
        }
    }

    void OnDestroy() {
        PlayerManager.instance = null;
    }

    void Update() {
        Dictionary<string, MonsterMoneyGenerationData.MonsterMoneyGenerationEntry> entryDict = this.moneyGenerationData.GetEntryDict();

        foreach (string name in this.capturedMonsters.Keys) {
            if (Time.time - this.lastMoneyAddedPerMonster[name] >= entryDict[name].duration) {
                int count = this.capturedMonsters[name];

                this.playerMoney += entryDict[name].value * count;

                this.lastMoneyAddedPerMonster[name] = Time.time;
            } 
        }
    }
}
