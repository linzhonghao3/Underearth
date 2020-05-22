using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightHand : MonoBehaviour
{
    public bool shortDistanAttackright;
    public bool hasBeenDamagedbyShort;
    void Update()
    {
        hasBeenDamagedbyShort=GameObject.FindGameObjectWithTag("BOSS").GetComponent<BOSS>().hasBeenDamagedByShort;
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Player"&&shortDistanAttackright){
            other.gameObject.GetComponent<Player>().Damage(10);
            shortDistanAttackright=false;
            hasBeenDamagedbyShort=true;
        }
    }
}
