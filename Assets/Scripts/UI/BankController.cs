using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BankController : MonoBehaviour {
    [SerializeField]
    private Text bankStatusAndEmptyText = null;

    [SerializeField]
    private Text bankUpgradeText = null;

    public void EmptyBank() {
        PlayerManager.Instance.AddBankToPlayerMoney();
    }

    public void AttemptBankUpgrade() {
        PlayerManager.Instance.AttemptBankUpgrade();
    }

    void Update() {
        int curLevel, costToUpgrade, maxSize, storedMoney;
        PlayerManager.Instance.GetCurBankLevelAndCost(out curLevel, out costToUpgrade, out maxSize, out storedMoney);

        this.bankStatusAndEmptyText.text = "Stored Money = " + storedMoney + "\nBankSize = " + maxSize + "\nEmpty Bank";
        this.bankUpgradeText.text = "CurLevel = " + curLevel + "\nCostToUpgrade = " + costToUpgrade;
    }
}
