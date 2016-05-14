using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpSpriteBehaviour : StateMachineBehaviour {
    private GameObject characterObject;

    private GameObject shadowObject;

    private float startTime;

    private Vector3 origPos;

    [SerializeField]
    private float finalLocalYPos;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.startTime = Time.fixedTime;
        Image[] images = animator.GetComponentsInChildren<Image>();
        foreach(Image img in images) {
            if (img.name == "Character") {
                this.characterObject = img.gameObject;
            }
        }

        this.origPos = this.characterObject.transform.localPosition;
	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float duration = animator.GetComponent<Monster>().MoveDuration;

        float posRatio = 1.0f - (Mathf.Abs((Time.fixedTime - this.startTime) - (duration / 2.0f)) / (duration / 2.0f));

        Vector3 pos = this.origPos;
        pos.y += this.finalLocalYPos * posRatio;
        this.characterObject.transform.localPosition = pos;
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.characterObject.transform.localPosition = this.origPos;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
