using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingG : MonoBehaviour
{   
    private Rigidbody2D rb;
    public float rightForce;
    private float timeonGround;
    private float lastVelocityX=0f;
    [SerializeField] private float acceleration;
    void Start(){
        rb=this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {   //上升时
        if (transform.position.y>=6f&&rb.velocity.y>0){
            if (rb.velocity.x<-0.1f){
                rb.velocity+=(Vector2.right*rightForce);
                if (rb.velocity.x<-1f){
                    rb.velocity+=(Vector2.right*rightForce/2);
                    if (rb.velocity.x<-3f){
                        rb.velocity+=(Vector2.right*rightForce/2);
                    }
                }

            }
            if (transform.position.y>8f){
                rb.mass=10;
            }
            
        }
        //落地一秒后重力变回来
        if (transform.position.y<6f){
            timeonGround+=Time.deltaTime;
        }
        else timeonGround=0;

        if (timeonGround>1f){
            rb.mass=0.5f;
        }
    }
}
