using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{   public CharacterControl2D cc;
    public Rigidbody2D rb;
    public Animator animator; 
    public GameObject hook;
    public Vector2 destination;
    public Vector2 secondDestination;
    // Start is called before the first frame update

    public float horizontalForce=300f;
    float move;
    bool jump;
    public bool isHanging;
    public bool isJumping;
    public bool isFalling;
    private GameObject currentHook;
    void Start()
    {
        cc=GetComponent<CharacterControl2D>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        move=Input.GetAxis("Horizontal");
        jump=Input.GetButton("Jump");
        Jump();
        Run();
        ThrowHook();
        SightChanging();
        ChangeRope();
        if (isHanging){
            cc.isHanging=true;
            ConnectOtherSide();
        }
        else {cc.isHanging=false;}
    }
    void FixedUpdate(){
        cc.Move(move,jump);
    }
    void Run(){
        animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
    }
    void Jump(){
        if (rb.velocity.y>0.02){
            isJumping=true;
            isFalling=false;
        }
        else if(rb.velocity.y<-0.02){ 
            isJumping=false;
            isFalling=true;
        }
        else {
            isFalling=false;
            isJumping=false;
        }

        if(isJumping&&!(gameObject.GetComponent<CharacterControl2D>().m_Grounded)){  //纵向速度大于0且不在地上 （防止低空抽搐）
            animator.SetBool("isJumping",true);
            animator.SetBool("isFalling",false);
        }
        else if (isFalling&&!(gameObject.GetComponent<CharacterControl2D>().m_Grounded)){
            animator.SetBool("isFalling",true);
            animator.SetBool("isJumping",false);
        }
        else {
            animator.SetBool("isJumping",false);
            animator.SetBool("isFalling",false);
        }
    }
    void ThrowHook(){
        if (Input.GetMouseButtonDown(1)){
            if (!isHanging){
                destination=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentHook=Instantiate(hook,transform.position,Quaternion.identity) as GameObject;
                currentHook.GetComponent<ropeScript>().destination=destination;
                isHanging=true;
            }
            else {
                Destroy(currentHook);
                isHanging=false;
                gameObject.GetComponent<CharacterControl2D>().isHanging=false;
            }
        }
    }
    void Die(){
        if (gameObject.GetComponent<Player>().currentHP<=0){
            animator.SetBool("isDead",true);
        }
        else animator.SetBool("isDead",false);
    }
    void SightChanging(){
        //鼠标滚轮放大缩小视野
        if (Input.GetAxis("Mouse ScrollWheel")<0){
            if (Camera.main.fieldOfView<=70){
                Camera.main.fieldOfView+=5;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel")>0){
            if (Camera.main.fieldOfView>=30){
                Camera.main.fieldOfView-=5;
            }
        }
    }
    void ChangeRope(){
        if (isHanging){
            if (Input.GetAxis("Mouse ScrollWheel")>0){
                Debug.Log("缩短绳子");

            }
            else if (Input.GetAxis("Mouse ScrollWheel")<0){
                Debug.Log("伸长绳子");
            }
        }
    }
    void ConnectOtherSide(){
        //一头已连接状态时按K键连接另一头
        if (isHanging){
            if (Input.GetKeyDown(KeyCode.K)){
                //按K键且人离要连的点距离很近时才能连接

            
            secondDestination=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentHook.GetComponent<ropeScript>().secondDestination=secondDestination;
            currentHook.GetComponent<ropeScript>().ConnectTheotherSideofRope();
            }
        }
    }
    
}