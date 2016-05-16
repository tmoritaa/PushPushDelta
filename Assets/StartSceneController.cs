using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartSceneController : AbstractSceneController {
    void LateUpdate() {
        SceneManager.LoadScene(ConstantVars.MAIN_MENU_SCENE_NAME);
    }
}
