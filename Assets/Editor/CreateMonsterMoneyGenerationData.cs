using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateMonsterMoneyGenerationData {
    [MenuItem("Assets/Create/MonsterMoneyGenerationData")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<MonsterMoneyGenerationData>();
    }
}
