using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateMonsterCageData {
    [MenuItem("Assets/Create/MonsterCageData")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<MonsterCageData>();
    }
}
