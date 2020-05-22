using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceAttack : StateMachineBehaviour
{
    Vector3 targetPos;
    Vector3 BOSSPos;
    AnimatorStateInfo info;
    public bool ableToShoot;
    LineRenderer linerenderer;
    Vector3 front;
    GameObject player;
    bool hasDamaged;
    Vector3 prePos;
    Vector3 eyePos;
    Vector3 targetRange;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.gameObject.transform.position+=new Vector3(0f,4f,0f);
        prePos=animator.gameObject.transform.position;
        front=animator.gameObject.GetComponent<BOSS>().front;
        targetPos=animator.gameObject.transform.position+new Vector3(0f,-4f,0f)+front*3;
        linerenderer=animator.gameObject.GetComponent<LineRenderer>();
        player=animator.gameObject.GetComponent<BOSS>().target.gameObject;
        eyePos=animator.gameObject.GetComponent<BOSS>().eyePos.position;
        targetRange=targetPos;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.transform.position=prePos;
        front=animator.gameObject.GetComponent<BOSS>().front;
        ableToShoot=animator.gameObject.GetComponent<BOSS>().beAbleToShoot;
        
        if (stateInfo.normalizedTime>0.35f){
            linerenderer.enabled=true;
            Debug.DrawLine(eyePos,targetPos,Color.green);
            linerenderer.SetPosition(0,eyePos);
            linerenderer.SetPosition(1,targetPos);
            linerenderer.startWidth=0.1f;
            linerenderer.endWidth=0.3f;
            Vector3 direction=(targetPos-eyePos).normalized;
            float length=(targetPos-eyePos).sqrMagnitude;
            if (Physics2D.Raycast(eyePos,direction,length,LayerMask.GetMask("Player"))&&!hasDamaged)
            {   //一次射线只造成一次伤害
                player.GetComponent<Player>().Damage(20);
                hasDamaged=true;
            }        
            if (targetPos.x>=targetRange.x+10f) return;
            else targetPos+= 0.105f*front;
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BOSS>().moveSpeed=1f;
        animator.gameObject.transform.position-=new Vector3(0f,4f,0f);
        linerenderer.enabled=false;
        hasDamaged=false;
    }

}
