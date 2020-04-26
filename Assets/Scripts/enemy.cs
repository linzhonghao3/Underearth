using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{   
    public My_healthbar hb;
    public int maxHP=50;
    public int currentHP;   
    [SerializeField]
    private Transform target; 
    public float moveSpeed=1f;
    private Rigidbody2D rb;
    Vector3 front;
    Vector3 Up=new Vector3 (0,0.15f,0);
    Vector3 Down=new Vector3 (0,-0.1f,0);
    public bool isChasing=false;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb=GetComponent<Rigidbody2D>();
        hb.SetMaxValue(maxHP);
        currentHP=maxHP;
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

    // Update is called once per frame
    void FixedUpdate()
    {   if (transform.localScale.x<0){
            front=new Vector3(-1,0,0);
        }
        else{
            front=new Vector3(1,0,0);
        }
        ChasePlayer();
        Run(front);
        CliffTest(front);
        
    }
    void ChasePlayer(){
        Vector3 Eyeview=transform.position+new Vector3(0,0.3f,0);
        
        float distance1=5f;
        float distance2=5f;
        float distance3=5f;
        while (Physics2D.Raycast(Eyeview,front,distance1,LayerMask.GetMask("Ground"))){
            distance1-=1;
        }
        while (Physics2D.Raycast(Eyeview,front+Up,distance2,LayerMask.GetMask("Ground"))){
            distance2-=1;
        }
        while (Physics2D.Raycast(Eyeview,front+Down,distance3,LayerMask.GetMask("Ground"))){
            distance3-=1;
        }
        Debug.DrawLine(Eyeview,Eyeview+(front+Up)*distance2,Color.red);
        Debug.DrawLine(Eyeview,Eyeview+(front+Down)*distance3,Color.red);
        Debug.DrawLine(Eyeview,Eyeview+front*distance1,Color.red);

        if (Physics2D.Raycast(Eyeview,front,distance1,LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+Up,distance2,LayerMask.GetMask("Player"))||
            Physics2D.Raycast(Eyeview,front+Down,distance3,LayerMask.GetMask("Player"))){
            moveSpeed=5f;
            isChasing=true;
        }
        else {
            moveSpeed=1.2f;
            isChasing=false;
            }
        
    }
    public void TakeDamage(int damage){
        currentHP-=damage;
        if (currentHP<=0){
            Destroy(gameObject);
        }
        hb.SetHeal(currentHP);
        
    }
    public void CliffTest(Vector3 front){
        Vector3 nowPos=transform.position+new Vector3(0,-0.5f,0);
        Debug.DrawLine(nowPos,nowPos+front*0.5f);
            if (Physics2D.Raycast(nowPos,front,0.5f,LayerMask.GetMask("Ground"))){
                if (!isChasing){
                    transform.localScale=new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
                }
                else return;

            }

    }
    void Run(Vector3 front){
        rb.velocity=new Vector2(front.x*moveSpeed,rb.velocity.y);
    }
   
    
}
