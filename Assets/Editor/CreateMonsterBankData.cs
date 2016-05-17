using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateMonsterBankData {
    [MenuItem("Assets/Create/MonsterBankData")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<MonsterBankData>();
    }
}
