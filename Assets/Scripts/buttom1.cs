﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttom1 : MonoBehaviour

{
    bool isPressed=false;
    public GameObject door;

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Bullet"){
            Debug.Log("Hit the buttom");
            changeStatus();
        }
    }
    void changeStatus(){
        if (isPressed==false){
            isPressed=true;
            door.GetComponent<Door>().changeStatus();
            //transform.position=new Vector3(transform.position.x+0.1f,transform.position.y,transform.position.z);
            //door.transform.rotation=Quaternion.Euler(0f,90f,0f);
        }
        else if (isPressed==true) {
            isPressed=false;
            door.GetComponent<Door>().changeStatus();
            //transform.position=new Vector3(transform.position.x-0.1f,transform.position.y,transform.position.z);
            //door.transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }
}
