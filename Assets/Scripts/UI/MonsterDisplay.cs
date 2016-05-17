using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterDisplay : MonoBehaviour {
    [SerializeField]
    Text monsterNameText;

    [SerializeField]
    Text upgradeButtonText;

    private string monsterName;
    
    public void InitWithMonster(Monster monster) {
        this.monsterName = monster.MonsterName;
        int count = PlayerManager.Instance.CapturedMonsters[monster.MonsterName];
        this.monsterNameText.text = this.monsterName + " x" + count;
    }

    public void UpgradeCage() {
        PlayerManager.Instance.AttemptCageUpgrade(this.monsterName);
    }

    void Update() {
        int size, level, cost;
        PlayerManager.Instance.GetMonsterCageInfo(this.monsterName, out size, out level, out cost);

        this.upgradeButtonText.text = "Size = " + size + "\nLevel = " + level + "\nCost to Upgrade = " + cost;
    }
}
