using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;
    public static PlayerManager Instance {
        get { return PlayerManager.instance; }
    }

    [SerializeField]
    private MonsterMoneyGenerationData moneyGenerationData = null;

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

    [SerializeField]
    private MonsterCageData monsterCageData = null;
    private Dictionary<string, int> monsterCageSizeLevels = new Dictionary<string, int>();

    [SerializeField]
    private MonsterBankData monsterBankData = null;

    private int bankLevel = 0;
    private int curBankMoney = 0;

    public void AddToCaptured(string monsterName, int count) {
        if (!this.capturedMonsters.ContainsKey(monsterName)) {
            this.capturedMonsters[monsterName] = 0;
        }

        this.capturedMonsters[monsterName] = Math.Min(this.capturedMonsters[monsterName] + count, this.monsterCageData.MonsterCageEntries[this.monsterCageSizeLevels[monsterName]].maxSize);
    }

    public void AddBankToPlayerMoney() {
        this.playerMoney += this.curBankMoney;
        this.curBankMoney = 0;
    }

    public bool AttemptBankUpgrade() {
        List<MonsterBankData.MonsterBankEntry> monsterBankEntries = this.monsterBankData.MonsterBankEntries;
        
        if ((this.bankLevel < monsterBankEntries.Count - 1) && this.playerMoney >= monsterBankEntries[this.bankLevel+1].cost) {
            this.playerMoney -= monsterBankEntries[this.bankLevel + 1].cost;
            this.bankLevel += 1;

            return true;
        }

        return false;
    }

    public bool AttemptCageUpgrade(string monsterName) {
        List<MonsterCageData.MonsterCageEntry> monsterCageEntries = this.monsterCageData.MonsterCageEntries;
        int level = this.monsterCageSizeLevels[monsterName];

        if ((level < monsterCageEntries.Count - 1) && this.playerMoney >= monsterCageEntries[level+1].cost) {
            this.playerMoney -= monsterCageEntries[this.bankLevel + 1].cost;
            this.monsterCageSizeLevels[monsterName] += 1;

            return true;
        }

        return false;
    }

    public void GetCurBankLevelAndCost(out int curLevel, out int costToUpgrade, out int maxSize, out int curStored) {
        curLevel = this.bankLevel + 1;
        maxSize = this.monsterBankData.MonsterBankEntries[this.bankLevel].maxSize;
        if (this.bankLevel >= this.monsterBankData.MonsterBankEntries.Count - 1) {
            costToUpgrade = -1;
        } else {
            costToUpgrade = this.monsterBankData.MonsterBankEntries[this.bankLevel + 1].cost;
        }
        curStored = this.curBankMoney;
    }

    public void GetMonsterCageInfo(string monsterName, out int size, out int level, out int cost) {
        level = this.monsterCageSizeLevels[monsterName];
        size = this.monsterCageData.MonsterCageEntries[level].maxSize;
        if (level >= this.monsterCageData.MonsterCageEntries.Count - 1) {
            cost = -1;
        } else {
            cost = this.monsterCageData.MonsterCageEntries[level+1].maxSize;
        }

        level += 1;
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

        // TODO: for now just init, should try to load, then init.
        Dictionary<string, Monster> monsterDict = MonsterManager.Instance.MonsterDict;
        foreach(string name in monsterDict.Keys) {
            this.monsterCageSizeLevels[name] = 0;
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

                this.curBankMoney = Math.Min(this.curBankMoney + entryDict[name].value * count, this.monsterBankData.MonsterBankEntries[this.bankLevel].maxSize);

                this.lastMoneyAddedPerMonster[name] = Time.time;
            } 
        }
    }
}
