using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortDistanceAttack : StateMachineBehaviour
{   
    bool hasFlashed; //近战两下攻击，需要在中间重置一次伤害判定 以确保两次都能造成伤害
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasFlashed=false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime>0.44f&&!hasFlashed){
            animator.gameObject.GetComponent<BOSS>().hasBeenDamagedByShort=false;
            hasFlashed=true;
            //避免一次伤害多次判定 以及保证两次锤地板都能造成伤害
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BOSS>().hasBeenDamagedByShort=false;
        hasFlashed=false;
    }

 
}
