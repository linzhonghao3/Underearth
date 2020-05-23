using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryRolling : MonoBehaviour
{   
    float maxHP=60;
    float currentHP;
    void Start(){
        currentHP=maxHP;
    }
    void Update()
    {
        if (transform.position.y<-5|| currentHP<=0){
            Destroy(gameObject);
        }
    }
    public void Damage(){
        currentHP-=20;
    }
}
