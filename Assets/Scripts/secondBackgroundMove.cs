using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondBackgroundMove : MonoBehaviour
{   public Transform backgrondPos;
    float targetX;
    public Vector3 camPos;
    // Start is called before the first frame update
    void Start(){
        backgrondPos=GetComponent<Transform>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        camPos=Camera.main.transform.position;
        targetX=backgrondPos.position.x;
        if (backgrondPos.position.x+12.45f<camPos.x){
            targetX+=24.9f;
        }
        if (backgrondPos.position.x-12.45f>camPos.x){
            targetX-=24.9f;
        }
        transform.position=new Vector3(targetX,transform.position.y,transform.position.z);
    }
}
