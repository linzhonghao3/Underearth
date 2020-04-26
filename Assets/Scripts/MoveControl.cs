using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveControl : MonoBehaviour
{   
    [SerializeField]
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    public float force=5;
    public float jumpForce=5;
    public Animator animator;
    bool isAttacking=false;
    public float moveH;
    void Start(){
        sp=this.GetComponent<SpriteRenderer>();
        rb=this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.velocity=Vector2.up*jumpForce;
        }
        moveH=Input.GetAxis("Horizontal");
        rb.velocity=new Vector2(moveH,rb.velocity.y);
        animator.SetFloat("Speed",Mathf.Abs(moveH));
        if (rb.velocity.y!=0){
            animator.SetBool("isJumping",true);
        }
        else animator.SetBool("isJumping",false);
        Attack();
        Flip();
        
    }

    void Flip(){
        if (moveH<0){
            sp.flipX=true;
        }
        else if (moveH>0){
            sp.flipX=false;
        }
    }
    void Attack(){
        if (Input.GetKeyDown(KeyCode.X)){
            animator.SetTrigger("Attack1");
        }
    }
}
