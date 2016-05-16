using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneTransitionManager : MonoBehaviour {
    private static SceneTransitionManager instance;
    public static SceneTransitionManager Instance {
        get { return SceneTransitionManager.instance; }
    }

    List<string> loadedScenes = new List<string>();

    public int GetNumberOfLoadedScenes() {
        return this.loadedScenes.Count;
    }

    public void LoadSceneAsRoot(string name) {
        this.loadedScenes.Clear();
        this.loadedScenes.Add(name);
        SceneManager.LoadScene(name);
    }

    public void AddScene(string name) {
        this.loadedScenes.Add(name);
        SceneManager.LoadScene(name);
    }

    public void GoToPrevScene() {
        if (this.loadedScenes.Count <= 1) {
            return;
        }

        string name = this.loadedScenes[this.loadedScenes.Count - 2];
        this.loadedScenes.RemoveAt(this.loadedScenes.Count - 1);
        SceneManager.LoadScene(name);
    }
    
    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform);
        SceneTransitionManager.instance = this;
    }

    void OnDestroy() {
        SceneTransitionManager.instance = null;
    }
}
