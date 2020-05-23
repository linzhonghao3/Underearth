using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanShou : MonoBehaviour
{
    [SerializeField]bool isPressed=false;
    public GameObject door;
    private float playerPosX;
    /*private Rigidbody2D rb;
    private HingeJoint2D hj;
    public float downForce=10f;
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        hj=this.GetComponent<HingeJoint2D>();
    }
    void FixedUpdate(){
        if (hj.connectedBody.gameObject.tag=="Hook"){
            //若把手与锚点相连
            rb.AddForce(Vector2.down*downForce);
        }
    }*/
    void Update(){
        playerPosX=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Hook"){
            Debug.Log("Hit the banshou 2");
            changeStatus();     
        }
    }
    void changeStatus(){
        if (isPressed==false){
            isPressed=true;
            if (playerPosX>transform.position.x){
                Debug.Log("向右");
                transform.rotation=Quaternion.Euler(0f,0f,135f);
            }
            else {
                Debug.Log("向左");
                transform.rotation=Quaternion.Euler(0f,0f,45f);
            }
            door.GetComponent<Door>().changeStatus();
            //transform.position=new Vector3(transform.position.x+0.1f,transform.position.y,transform.position.z);
            //door.transform.rotation=Quaternion.Euler(0f,90f,0f);
        }
        else if (isPressed==true) {
            isPressed=false;
            transform.rotation=Quaternion.Euler(0f,0f,90f);
            door.GetComponent<Door>().changeStatus();
            //transform.position=new Vector3(transform.position.x-0.1f,transform.position.y,transform.position.z);
            //door.transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }
}  
