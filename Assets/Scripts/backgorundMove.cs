using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgorundMove : MonoBehaviour
{   public Transform backgrondPos;
    float targetX;
    public Vector3 camPos;
    public float backgorundWide=19;
    // Start is called before the first frame update
    void Start(){
        backgrondPos=GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        camPos=Camera.main.transform.position;
        targetX=backgrondPos.position.x;
        if (backgrondPos.position.x+backgorundWide<camPos.x){
            targetX+=2*backgorundWide;
        }
        if (backgrondPos.position.x-backgorundWide>camPos.x){
            targetX-=2*backgorundWide;
        }
        transform.position=new Vector3(targetX,transform.position.y,transform.position.z);
    }
}
