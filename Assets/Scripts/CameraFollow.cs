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
    public float xDis=3f;
    public float yDis=1.5f;
    public Vector3 FixedCameraforWater;
    public float xleftForplayerinSlopeArea;
    public float xrightForplayerinSlopeArea;
    public float xleftForplayerinGearArea;
    public float xrightForplayerinGearArea;
    public float xleftForplayerinwaterArea;
    public float xrightForplayerinwaterArea;
    public float xleftForplayerinBOSSArea;
    public float xrightForplayerinBOSSArea;
    public float xleftForplayerinseesawArea;
    public float xrightForplayerinseesawArea;
    public float xleftForplayerinDiCiArea;
    public float xrightForplayerinDiCiArea;
    bool firstIn=true;


    bool isMoving=false;
    public Vector3 nextPos;
    void Start(){
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nextPos=transform.position;
    }
    void FixedUpdate()
    {   
        SpecialArea();
            //transform.position=FixedCameraforWater;
        Vector3 Pos=new Vector3(player.position.x+xDis,player.position.y+yDis,-10);
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
    void SpecialArea(){
        if (xleftForplayerinwaterArea<=player.transform.position.x&&player.transform.position.x<=xrightForplayerinwaterArea){
            transform.position=FixedCameraforWater;
        }
        else if (xleftForplayerinBOSSArea<=player.transform.position.x&&player.transform.position.x<=xrightForplayerinBOSSArea){
            boundaryleft=232f;
            boundaryright=233.5f;
            boundaryup=25.27f;
            if (firstIn){
                Camera.main.transform.position=new Vector3(232.8f,22.85f,-10f);
                firstIn=false;
            }
        }
        else if (xleftForplayerinSlopeArea<player.transform.position.x&&player.transform.position.x<=xrightForplayerinSlopeArea){
            boundaryup=3.7f;
        }
        else if (xleftForplayerinGearArea<player.transform.position.x&&player.transform.position.x<=xrightForplayerinGearArea){
            boundaryup=9f;
        }
        else if (xleftForplayerinseesawArea<=player.transform.position.x&&xrightForplayerinseesawArea>=player.transform.position.x){
            boundaryup=15f;
        }
        else if (xleftForplayerinDiCiArea<=player.transform.position.x&&xrightForplayerinDiCiArea>=player.transform.position.x){
            boundaryup=22.5f;
        }
    }
    public IEnumerator CameraShake (float maxTime,float amount){
        Vector3 originalPos=transform.localPosition;
        float shakeTime=0f;
        while (shakeTime<maxTime){
            float x=Random.Range(-1f,1f)*amount;
            float y=Random.Range(-1f,1f)*amount;
            transform.localPosition=new Vector3(originalPos.x+x,originalPos.y+y,originalPos.z);
            shakeTime+=Time.deltaTime;
            yield return new WaitForSeconds(0f);
        }
    } 
}
