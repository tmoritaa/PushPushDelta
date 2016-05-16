using UnityEngine;
using System.Collections;

public abstract class AbstractSceneController : MonoBehaviour {
    [SerializeField]
    bool showTopbar = true;

    protected virtual void Awake() {
        if (TopBarManager.Instance() != null) {
            TopBarManager.Instance().ShowTopbar(this.showTopbar);
        }
    }
}
