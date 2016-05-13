using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneController : MonoBehaviour {
    [SerializeField]
    Text resultText;

	// Use this for initialization
	void Start () {
        int score = (int)SceneTransitionData.Instance.data[0];

        resultText.text = "You scored " + score + " points";
	}
}
