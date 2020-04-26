using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearRolling : MonoBehaviour
{   
    private Transform gearTransform;
    float nextZ=0;
    public bool beOn=true;
    // Start is called before the first frame update
    void Start()
    {
        gearTransform=GetComponent<Transform>();
    }
    void FixedUpdate(){
        if (beOn){
            nextZ+=Time.deltaTime*30;
            gearTransform.rotation=Quaternion.Euler(0,0,nextZ);}
        else return;
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Player"){
            Debug.Log("Kill by gear");
            other.gameObject.GetComponent<Player>().Damage(100);
        }
        else return;
    }
    public void changeStatus(){
        if (beOn){
        beOn=false;}
        else beOn=true;
    }
}
