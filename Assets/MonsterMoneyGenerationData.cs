using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterMoneyGenerationData : ScriptableObject {
    [System.Serializable]
    public class MonsterMoneyGenerationEntry {
        public Monster monster;
        public float duration;
        public int value;
    }

    [SerializeField]
    private List<MonsterMoneyGenerationEntry> moneyGenerationEntries;

    private Dictionary<string, MonsterMoneyGenerationEntry> entryDict = null;


    public Dictionary<string, MonsterMoneyGenerationEntry> GetEntryDict() {
        if (this.entryDict == null) {
            this.entryDict = new Dictionary<string, MonsterMoneyGenerationEntry>();

            foreach (MonsterMoneyGenerationEntry entry in this.moneyGenerationEntries) {
                this.entryDict[entry.monster.MonsterName] = entry;
            }
        }

        return this.entryDict;
    }
}
