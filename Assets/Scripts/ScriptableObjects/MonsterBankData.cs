using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterBankData : ScriptableObject {
    [System.Serializable]
    public class MonsterBankEntry {
        public int cost;
        public int maxSize;
    }

    [SerializeField]
    private List<MonsterBankEntry> monsterBankEntries = new List<MonsterBankEntry>();
    public List<MonsterBankEntry> MonsterBankEntries {
        get { return this.monsterBankEntries; }
    }
}
