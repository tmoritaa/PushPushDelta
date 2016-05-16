using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CapturedMonsterResultDisplay : MonoBehaviour {
    [SerializeField]
    Image monsterImage;

    [SerializeField]
    Text monsterName;

    [SerializeField]
    Text monsterNumber;
    
    public void Initialize(Monster monster, int count) {
        this.monsterImage.sprite = monster.MainImage.sprite;
        this.monsterName.text = monster.MonsterName;
        this.monsterNumber.text = "x " + count;
    }
}
