using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneTransitionData {
    private static SceneTransitionData instance;

    public static SceneTransitionData Instance {
        get {
            if (instance == null) {
                instance = new SceneTransitionData();
            }
            return SceneTransitionData.instance;
        }
    }

    private SceneTransitionData() {}

    public List<object> data = new List<object>();
}
