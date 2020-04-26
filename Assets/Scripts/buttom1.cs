using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttom1 : MonoBehaviour

{
    bool isPressed=false;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Bullet"){
            Debug.Log("Hit the buttom");
            changeStatus();
        }
    }
    void changeStatus(){
        if (isPressed==false){
             isPressed=true;
             transform.position=new Vector3(transform.position.x+0.1f,transform.position.y,transform.position.z);
             door.transform.rotation=Quaternion.Euler(0f,90f,0f);
        }
        else if (isPressed==true) {
            isPressed=false;
            transform.position=new Vector3(transform.position.x-0.1f,transform.position.y,transform.position.z);
            door.transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }
}
