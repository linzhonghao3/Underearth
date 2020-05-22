using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidDistanceAttack : StateMachineBehaviour
{
    Vector3 prePos;
    bool hasFlashed; //砸地板两下，需要在中间重置一次伤害判定 以确保两次都能造成伤害
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prePos=animator.gameObject.transform.position;
        hasFlashed=false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.position=prePos;
        if (stateInfo.normalizedTime>0.5f&&!hasFlashed){
            animator.gameObject.GetComponent<BOSS>().hasBeenDamagedByMid=false;
            hasFlashed=true;
            //避免一次伤害多次判定 以及保证两次锤地板都能造成伤害
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BOSS>().hasBeenDamagedByMid=false;
        hasFlashed=false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
