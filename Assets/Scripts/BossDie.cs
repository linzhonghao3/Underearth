using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : StateMachineBehaviour
{
    Vector3 prePos; 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prePos=animator.transform.position;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.position=prePos;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
