using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuSceneController : AbstractSceneController {
    public void GoToLevelSelectScene() {
        SceneManager.LoadScene(ConstantVars.LEVEL_SELECT_SCENE_NAME);
    }
}
