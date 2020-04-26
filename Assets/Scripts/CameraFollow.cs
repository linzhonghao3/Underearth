using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    public Transform player;
    public float boundaryleft=1.6f;
    public float boundaryright=49f;
    public float boundaryup=0.35f;
    public float boundarydown=-0.95f;
    Vector3 distance;
    public float speed=2f;

    bool isMoving=false;
    public Vector3 nextPos;
    void Start(){
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nextPos=transform.position;
    }
    void FixedUpdate()
    {   
        Vector3 Pos=new Vector3(player.position.x+3,player.position.y+3.5f,-10);
        if (!(Pos.x>boundaryleft&&Pos.x<boundaryright)) {
            Pos.x=transform.position.x;
        }
        if (!(Pos.y>boundarydown&&Pos.y<boundaryup)) {
            Pos.y=transform.position.y;
        }
        transform.position=Vector3.Lerp(transform.position,Pos,speed*Time.deltaTime);
        //Follow();
        
    }
    void Follow(){
        transform.position=Vector3.Lerp(transform.position,nextPos,speed*Time.deltaTime);
        if (transform.position!=nextPos){       //如果当前位置不等于下一个预判位置 （表示还在移动中）不计算
            return;
        }
        else {                                  //已经到达目标位置，可以开始下一轮计算
            float distanceX=player.position.x-transform.position.x;
            float distanceY=player.position.y-transform.position.y;
            float nextX=transform.position.x;
            float nextY=transform.position.y;
            if (distanceX>3){
                Debug.Log("X>3");
                nextX=transform.position.x+8;
            }
            else if (distanceX<-3){
                Debug.Log("X<-3");
                nextX=transform.position.x-8;
            }
            else if (distanceY>3){
                Debug.Log("Y>3");
                nextY=transform.position.y+8;
            }
            else if (distanceY<-3){
                Debug.Log("Y<-3");
                nextY=transform.position.y-8;
            }    
            nextPos=new Vector3(nextX,nextY,transform.position.z);
            
        }
        
    }  
}
