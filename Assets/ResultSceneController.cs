using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultSceneController : AbstractSceneController {
    [SerializeField]
    Text resultText;

	// Use this for initialization
	void Start () {
        List<object> data = SceneTransitionData.Instance.RetrieveData();
        int score = (int)data[0];
        resultText.text = "You scored " + score + " points";
    }

    public void GoToLevelSelect() {
        SceneManager.LoadScene(ConstantVars.LEVEL_SELECT_SCENE_NAME);
    }
}
