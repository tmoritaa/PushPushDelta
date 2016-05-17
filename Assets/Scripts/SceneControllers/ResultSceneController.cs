using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultSceneController : AbstractSceneController {
    [SerializeField]
    Text resultText;

    [SerializeField]
    CapturedMonsterResultDisplay resultDisplayPrefab = null;

    [SerializeField]
    GameObject resultDisplayRoot = null;

	// Use this for initialization
	void Start () {
        List<object> objects = SceneTransitionData.Instance.RetrieveData();

        Dictionary<string, Monster> monsterDict = MonsterManager.Instance.MonsterDict;

        for(int i = 0; i < objects.Count; i += 2) {
            string name = (string)objects[i];
            int count = (int)objects[i + 1];

            PlayerManager.Instance.AddToCaptured(name, count);

            Monster monster = monsterDict[name];
            CapturedMonsterResultDisplay display = GameObject.Instantiate<CapturedMonsterResultDisplay>(this.resultDisplayPrefab);
            display.Initialize(monster, count);
            int displayPosIdx = i / 2;
            RectTransform rectTrans = display.GetComponent<RectTransform>();
            rectTrans.anchorMin = new Vector2(0, 1.0f - (displayPosIdx + 1) * 0.15f);
            rectTrans.anchorMax = new Vector2(1, 1.0f - displayPosIdx * 0.15f);
            display.transform.SetParent(this.resultDisplayRoot.transform, false);
        }
    }

    public void GoToMainMenu() {
        SceneTransitionManager.Instance.LoadSceneAsRoot(ConstantVars.MAIN_MENU_SCENE_NAME);
    }
}
