using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundparallax : MonoBehaviour
{   public Transform[] transforms;
    Transform camPos;
    Vector3 previousPos;
    public float parallaxScale=-20f;
    public float smoothingSpeed=0.4f;
    public float parallaxReductionFactor=2f;
    // Start is called before the first frame update
    void Awake(){
        camPos=Camera.main.transform;
    }
    void Start()
    {
        previousPos=camPos.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        float parallax=(previousPos.x-camPos.position.x)*parallaxScale;
        for (int i=0;i<transforms.Length;i++){
            float backGroundTargetPosX=transforms[i].position.x+parallax*(i*parallaxReductionFactor+1);
            Vector3 backGroundTargetPos=new Vector3(backGroundTargetPosX,transforms[i].position.y,transforms[i].position.z);
            transforms[i].position=Vector3.Lerp(transforms[i].position,backGroundTargetPos,smoothingSpeed*Time.deltaTime);
        }
        previousPos=camPos.position;
    }
}
