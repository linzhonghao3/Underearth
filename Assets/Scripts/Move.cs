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
    public Vector3 restartPoint;
    [SerializeField] private float fallingGravityMultiplex=2f;
    void Start()
    {
        cc=GetComponent<CharacterControl2D>();
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        restartPoint=new Vector3(-15f,-1.64f,0f);//默认复活点
    }

    // Update is called once per frame
    void Update()
    {   
        
        move=Input.GetAxis("Horizontal");
        jump=Input.GetButton("Jump");
        if (jump) this.gameObject.transform.Find("JumpVoice").GetComponent<AudioSource>().Play();
        Jump(); //判定跳跃动画
        BetterJump(); //优化跳跃曲线
        Run();
        ThrowHook();
        SightChanging();
        ChangeRope();
        if (transform.position.y<-5){
            Die();}
        if (isHanging){
            cc.isHanging=true;
            ConnectOtherSide();
        }
        else {cc.isHanging=false;}
    }
    void FixedUpdate(){
        cc.Move(move,jump);
    }
    void BetterJump(){
        if (rb.velocity.y<0){
            //下落状态重力大
            rb.velocity+=Vector2.up*Physics2D.gravity*(fallingGravityMultiplex-1)*Time.deltaTime;
        }
        if (rb.velocity.y>0){
            //上升状态 
        }
    }
    void Run(){
        animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
        /*if (Mathf.Abs(rb.velocity.x)>0.1f&&!isHanging&&!isJumping&&!isFalling){
            this.gameObject.transform.Find("RunVoice").GetComponent<AudioSource>().Play();
        }*/
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
                animator.SetTrigger("Throw");
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
    public void Die(){
        animator.SetTrigger("Die");
        gameObject.GetComponent<Move>().enabled=false;
            //在最近碰撞过的复活点复活
        StartCoroutine("Waits");
        if (GameObject.FindGameObjectWithTag("Hook")!=null){
            Destroy(GameObject.FindGameObjectWithTag("Hook"));
            isHanging=false;
            gameObject.GetComponent<CharacterControl2D>().isHanging=false;
        }
    }
    IEnumerator Waits(){
        yield return new WaitForSeconds(1f);
        transform.position=restartPoint;
        gameObject.GetComponent<Move>().enabled=true;
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