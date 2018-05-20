using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBools : StateMachineBehaviour
{
    public enum EWhen { Enter, Exit }

    public EWhen when = EWhen.Exit;
    public List<Entry> entriesToSet;

    [System.Serializable]
    public class Entry
    {
        public string name;
        public bool val;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < entriesToSet.Count; i++)
            animator.SetBool(entriesToSet[i].name, entriesToSet[i].val);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < entriesToSet.Count; i++)
            animator.SetBool(entriesToSet[i].name, entriesToSet[i].val);
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
