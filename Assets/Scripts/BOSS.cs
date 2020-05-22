using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public My_healthbar hb;
    public int maxHP=500;
    public int currentHP;   
    [SerializeField]
    public Transform target; 
    public float moveSpeed=1f;
    private Rigidbody2D rb;
    public Vector3 front;
    Vector3 Up=new Vector3 (0,0.15f,0);
    Vector3 Down=new Vector3 (0,-0.1f,0);
    float CDforLong=30f;
    float nextLong;
    float CDforMid=20f;
    float nextMid;
    public bool isChasing=false;
    private Animator animator;
    public bool beAbleToShoot;
    public bool midDistanceAttack;
    public bool hasBeenDamagedByMid;
    public bool hasBeenDamagedByShort;
    public bool shortDistanAttackleft;
    public bool shortDistanAttackright;
    public Transform rightHandPoint;
    public Transform leftHandTerminal;
    public Transform rightHandTerminal;
    public Transform leftHandPoint;
    public float shortAttackRadius=2f;
    public Transform eyePos;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb=GetComponent<Rigidbody2D>();
        hb.SetMaxValue(maxHP);
        currentHP=maxHP;
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator=this.GetComponent<Animator>();
        }

    // Update is called once per frame
    void FixedUpdate()
    {   if (transform.localScale.x<0){
            front=new Vector3(-1,0,0);
        }
        else{
            front=new Vector3(1,0,0);
        }
        nextLong+=Time.deltaTime;
        nextMid+=Time.deltaTime;
        ChasePlayer();
        Run(front);
        CliffTest(front);
        DamagePlayer();
        
    }
    void ChasePlayer(){
        Vector3 Eyeview=transform.position+new Vector3(0,3.5f,0);
        int [] distance=new int[]{10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10};
        while (Physics2D.Raycast(Eyeview,front+Up,distance[19],LayerMask.GetMask("Ground"))){
            distance[19]-=1;
        }
        //有一根视野是向上的，19根向下，向上单独讨论
        for (int i=0;i<=14;i++){
            while (Physics2D.Raycast(Eyeview,front+i*Down,distance[i],LayerMask.GetMask("Ground"))){
                distance[i]-=1;
            }
            Debug.DrawLine(Eyeview,Eyeview+(front+i*Down)*distance[i],Color.red);
        }
        for (int i=15;i<=18;i++){
            while (Physics2D.Raycast(Eyeview,front+(2*i-14)*Down,distance[i],LayerMask.GetMask("Ground"))){
                distance[i]-=1;
                //第15根线开始 每次加2个down向量，以此来保证靠近boss身体视线无盲区
            }
            Debug.DrawLine(Eyeview,Eyeview+(front+(2*i-14)*Down)*distance[i],Color.red);
        }
        Debug.DrawLine(Eyeview,Eyeview+(front+Up)*distance[19],Color.red);


        if (Physics2D.Raycast(Eyeview,front,distance[0],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+Up,distance[19],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+Down,distance[1],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+2*Down,distance[2],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+3*Down,distance[3],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+4*Down,distance[4],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+5*Down,distance[5],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+6*Down,distance[6],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+7*Down,distance[7],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+8*Down,distance[8],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+9*Down,distance[9],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+10*Down,distance[10],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+11*Down,distance[11],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+12*Down,distance[12],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+13*Down,distance[13],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+14*Down,distance[14],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+16*Down,distance[15],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+18*Down,distance[16],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+20*Down,distance[17],LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+22*Down,distance[18],LayerMask.GetMask("Player"))){
            moveSpeed=3f;
            isChasing=true;
            if (Mathf.Abs(target.transform.position.x-transform.position.x)<3f){
                animator.SetTrigger("ShortDistance");
            }
            else if (Mathf.Abs(target.transform.position.x-transform.position.x)>=6f&&nextLong>=CDforLong){
                nextLong=0f;
                animator.SetTrigger("LongDistance");
            }
            else if (Mathf.Abs(target.transform.position.x-transform.position.x)>=3f&&
                Mathf.Abs(target.transform.position.x-transform.position.x)<6f&&nextMid>=CDforMid){
                    nextMid=0f;
                    animator.SetTrigger("MidDistance");
                }
            
        }
        else {
            moveSpeed=1.5f;
            isChasing=false;
            }
        
    }
    public void TakeDamage(int damage){
        currentHP-=damage;
        if (currentHP<=0){
            Destroy(gameObject);
        }
        hb.SetHeal(currentHP);

        //受到伤害且角色在BOSS背后就转头
        if (transform.localScale.x<0&&target.position.x>transform.position.x||
            transform.localScale.x>0&&target.position.x<transform.position.x){
                transform.localScale=new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }
        
    }
    public void CliffTest(Vector3 front){
        Vector3 nowPos=transform.position+new Vector3(0,1f,0);
        Debug.DrawLine(nowPos,nowPos+front*3f);
            if (Physics2D.Raycast(nowPos,front,3f,LayerMask.GetMask("Ground"))){
                if (!isChasing){
                    transform.localScale=new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
                }
                else return;

            }

    }
    void Run(Vector3 front){
        rb.velocity=new Vector2(front.x*moveSpeed,rb.velocity.y);
    }
    public void DamagePlayer(){
        //砸地板时人若是在地上，扣血20
        if (midDistanceAttack){
            StartCoroutine(FindObjectOfType<CameraFollow>().CameraShake(0.2f,0.3f));
            if (target.gameObject.GetComponent<CharacterControl2D>().m_Grounded&&!hasBeenDamagedByMid){
                target.gameObject.GetComponent<Player>().Damage(20);
                midDistanceAttack=false;
                hasBeenDamagedByMid=true;
            }
        }
        if (shortDistanAttackright){
            //BOSS右手攻击，玩家在BOSS右手范围内受伤害
            if (Vector3.Distance(rightHandTerminal.position,target.position)<shortAttackRadius){
                target.gameObject.GetComponent<Player>().Damage(10);
                shortDistanAttackright=false;
                hasBeenDamagedByShort=true;
            }    
        }
        else if (shortDistanAttackleft){
            //BOSS左手攻击，玩家在BOSS左手范围内受伤害
            if (Vector3.Distance(leftHandTerminal.position,target.position)<shortAttackRadius){
                target.gameObject.GetComponent<Player>().Damage(10);
                shortDistanAttackleft=false;
                hasBeenDamagedByShort=true;
            }  
        }
    }
   
    
}
