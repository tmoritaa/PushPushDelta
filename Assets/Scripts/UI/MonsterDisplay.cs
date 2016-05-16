using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterDisplay : MonoBehaviour {
    [SerializeField]
    Text text;
    
    public void InitWithMonster(Monster monster) {
        int count = PlayerManager.Instance.CapturedMonsters[monster.MonsterName];
        this.text.text = monster.MonsterName + " x" + count;
    }
}
