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
        resultText.text = "Game over man";
    }

    public void GoToMainMenu() {
        SceneTransitionManager.Instance().LoadSceneAsRoot(ConstantVars.MAIN_MENU_SCENE_NAME);
    }
}
