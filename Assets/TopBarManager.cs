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
        this.topbar.Show(show);
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform);
        TopBarManager.instance = this;
    }

    void OnDestroy() {
        TopBarManager.instance = null;
    }
}
