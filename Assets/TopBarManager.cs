using UnityEngine;
using System.Collections;

public class TopBarManager : MonoBehaviour {
    [SerializeField]
    private TopBar topbar;

    private static TopBarManager instance;
    public static TopBarManager Instance() {
        return TopBarManager.instance;
    }

    public void ShowTopbar(bool show) {
        this.topbar.ShowTopbar(show);
    }

    public void BackButtonPressed() {
        SceneTransitionManager.Instance().GoToPrevScene();
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform);
        TopBarManager.instance = this;
    }

    void OnDestroy() {
        TopBarManager.instance = null;
    }
}
