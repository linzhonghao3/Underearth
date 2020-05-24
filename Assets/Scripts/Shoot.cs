using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : StateMachineBehaviour
{   
    public Vector3 mousePos;
    public float moveDirection;
    public float preDiretion;
    bool haveChanged;
    Vector3 front;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.gameObject.GetComponent<AudioSource>().Play();
        mousePos=animator.gameObject.GetComponent<Weapon>().mousePos;
        preDiretion=animator.gameObject.transform.rotation.y;
        moveDirection=animator.gameObject.GetComponent<Move>().move;
        if ((moveDirection==-1f)&&mousePos.x>animator.gameObject.transform.position.x){
            //往左跑 向右开枪
            animator.gameObject.transform.rotation=Quaternion.Euler(0f,0f,0f);
            haveChanged=true;
        }
        else if ((moveDirection==1f)&&mousePos.x<animator.gameObject.transform.position.x){
            //往右跑 向左开枪
            animator.gameObject.transform.rotation=Quaternion.Euler(0f,180f,0f);
            haveChanged=true;
        }
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        
        if (haveChanged){
            animator.transform.Rotate(0f,-180f,0f);
            haveChanged=false;
        }

    }

   
}
