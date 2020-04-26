using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private int counter=0;
    void Start()
    {
    }
    void FixedUpdate(){
        counter+=1;
        if (counter>=60){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        
        if (other.gameObject.tag=="Enemy"){
            Destroy(gameObject);
            other.GetComponent<enemy>().TakeDamage(20);
        }
        if (other.gameObject.layer==LayerMask.NameToLayer("Ground")){
            Destroy(gameObject);
        }
       
        if (other.gameObject.tag=="BrokenRock"){
            Destroy(other.gameObject);
            other.GetComponent<rockboom>().Boom();
        }
    }
}
