﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectSceneController : MonoBehaviour {
    public void GoToLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
