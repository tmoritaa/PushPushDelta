using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopBar : MonoBehaviour {
    [SerializeField]
    Text moneyText = null;
    
    public void Show(bool show) {
        this.gameObject.SetActive(show);
    }
	
	// Update is called once per frame
	void Update () {
        this.moneyText.text = "$" + PlayerManager.Instance().PlayerMoney.ToString();
	}
}
