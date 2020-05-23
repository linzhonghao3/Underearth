using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private int counter=0;
    //public AudioClip clip;
    void Start()
    {   
        this.gameObject.GetComponent<AudioSource>().Play();
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
            other.GetComponent<destoryRolling>().Damage();
            Destroy(gameObject);
        }
        if (other.gameObject.tag=="TNT"){
            other.GetComponent<TNT>().Boom();
        }
        if (other.gameObject.tag=="BOSS"){
            Destroy(gameObject);
            other.GetComponent<BOSS>().TakeDamage(20);
        }
        if (other.gameObject.tag=="BOSSHead"){
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("BOSS").GetComponent<BOSS>().TakeDamage(40);
        }
        
    }
}
