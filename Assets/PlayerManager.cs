using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager instance;
    public static PlayerManager Instance() {
        return PlayerManager.instance;
    }

    // TODO: should be saved in player prefs.
    private int playerMoney;
    public int PlayerMoney {
        get { return this.playerMoney; }
        set { this.playerMoney = value; }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform);
        PlayerManager.instance = this;
    }

    void OnDestroy() {
        PlayerManager.instance = null;
    }
}
