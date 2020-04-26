using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGear : MonoBehaviour
{
    public List<GameObject> Gears;
    bool isPressed;


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Bullet"){
            Debug.Log("Hit the buttom");
            changeStatus();
        }
    }
    void changeStatus(){
        if (isPressed==false){
            isPressed=true;
            //transform.position=new Vector3(transform.position.x+0.1f,transform.position.y,transform.position.z);
            foreach (GameObject gear in Gears){
                gear.GetComponent<gearRolling>().changeStatus();
            }
        }
        else if (isPressed==true) {
            isPressed=false;
            //transform.position=new Vector3(transform.position.x-0.1f,transform.position.y,transform.position.z);
            foreach (GameObject gear in Gears){
                gear.GetComponent<gearRolling>().changeStatus();
            }
        }
    }
}
