using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartSceneController : AbstractSceneController {
    void LateUpdate() {
        SceneTransitionManager.Instance.LoadSceneAsRoot(ConstantVars.MAIN_MENU_SCENE_NAME);
    }
}
