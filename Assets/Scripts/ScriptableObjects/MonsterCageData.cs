using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterCageData : ScriptableObject {
    [System.Serializable]
    public class MonsterCageEntry {
        public int cost;
        public int maxSize;
    }

    [SerializeField]
    private List<MonsterCageEntry> monsterCageEntries = new List<MonsterCageEntry>();
    public List<MonsterCageEntry> MonsterCageEntries {
        get { return this.monsterCageEntries; }
    }
}
