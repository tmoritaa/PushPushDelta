using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectSceneController : AbstractSceneController {
    public void GoToLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
