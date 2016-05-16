using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuSceneController : AbstractSceneController {
    public void GoToLevelSelectScene() {
        SceneTransitionManager.Instance.AddScene(ConstantVars.LEVEL_SELECT_SCENE_NAME);
    }

    public void GoToMonsterDisplayScene() {
        SceneTransitionManager.Instance.AddScene(ConstantVars.MONSTER_DISPLAY_SCENE_NAME);
    }
}
