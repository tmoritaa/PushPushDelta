using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultSceneController : MonoBehaviour {
    [SerializeField]
    Text resultText;

	// Use this for initialization
	void Start () {
        List<object> data = SceneTransitionData.Instance.RetrieveData();
        int score = (int)data[0];
        resultText.text = "You scored " + score + " points";
    }

    public void GoToLevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }
}
