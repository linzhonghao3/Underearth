using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    private Rigidbody2D rb;
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x=Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right*x);
        float y=Input.GetAxis("Vertical");
        rb.AddForce(Vector2.up*y);
    }
}
