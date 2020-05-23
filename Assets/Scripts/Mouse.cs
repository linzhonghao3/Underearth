using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    
    public float moveSpeed;
    private float count;
    void Start()
    {  
        count=0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        transform.position=new Vector3(transform.position.x+0.15f,transform.position.y,transform.position.z);
        count+=Time.deltaTime;
        if (count>4f){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Move>().Die();
        }
        else return;
    }
}
