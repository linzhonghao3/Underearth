using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftHand : MonoBehaviour
{   
    public bool shortDistanAttackleft=false;
    public bool hasBeenDamagedbyShort=false;
    void Update()
    {
        hasBeenDamagedbyShort=GameObject.FindGameObjectWithTag("BOSS").GetComponent<BOSS>().hasBeenDamagedByShort;
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Player"&&shortDistanAttackleft){
            other.gameObject.GetComponent<Player>().Damage(10);
            hasBeenDamagedbyShort=true;
        }
    }
}
