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

    private List<object> data = new List<object>();

    public void AddDataObj(object obj) {
        this.data.Add(obj);
    }

    public List<object> RetrieveData() {
        List<object> retList = new List<object>(this.data);
        this.data.Clear();
        return retList;
    }
}
