using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBar : MonoBehaviour {
    [SerializeField]
    Text moneyText = null;

    [SerializeField]
    Button backButton = null;
    
    public void ShowTopbar(bool show) {
        this.gameObject.SetActive(show);
    }

    public void ShowBackButton(bool show) {
        this.backButton.gameObject.SetActive(show);
    }
	
	// Update is called once per frame
	void Update () {
        this.moneyText.text = "$" + PlayerManager.Instance().PlayerMoney.ToString();

        this.ShowBackButton(SceneTransitionManager.Instance().GetNumberOfLoadedScenes() >= 2);
	}
}
