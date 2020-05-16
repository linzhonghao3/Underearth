using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanShou : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private HingeJoint2D hj;
    public float downForce=10f;
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        hj=this.GetComponent<HingeJoint2D>();
    }
    void FixedUpdate(){
        if (hj.connectedBody.gameObject.tag=="Hook"){
            //若把手与锚点相连
            rb.AddForce(Vector2.down*downForce);
        }
    }
}  
