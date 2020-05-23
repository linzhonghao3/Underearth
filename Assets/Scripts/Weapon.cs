using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform shootpoint;
    public GameObject Bullet;
    public float fireRate=0.75f;
    public float fireRateBig=2f;//换弹时间
    private float nextFire;
    private float nextFireBig;
    public float BulletSpeed=10f;
    private Animator animator;
    private AnimatorStateInfo ShootAnimation;
    public bool isShooting;
    private Move move;
    private CharacterControl2D characterControl2D;
    public int bulletCount;
    public Text bulletNumText;
    // Update is called once per frame
    void Start(){
        animator=GetComponent<Animator>();
        move=GetComponent<Move>();
        characterControl2D=GetComponent<CharacterControl2D>();
        ShootAnimation=animator.GetCurrentAnimatorStateInfo(0);
        bulletCount=6;
        nextFireBig=2.1f;
        
    }
    void FixedUpdate()
    {   
        if (bulletNumText!=null){
            bulletNumText.text=bulletCount.ToString();}
        if (bulletCount<=0){
            bulletCount=6;
            nextFireBig=0;    
        }
        nextFireBig+=Time.deltaTime;
        nextFire+=Time.deltaTime;
        if (Input.GetMouseButtonDown(0)&&(nextFire>fireRate)&&(nextFireBig>fireRateBig)){
            animator.SetTrigger("Shoot");
            isShooting=true;
            Shoot();

        }
    }
    void Shoot(){
        Vector3 mousePos=Input.mousePosition;
        mousePos=Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z=0;
        float fireAngle=Vector2.Angle(mousePos-transform.position,Vector2.up);
        if (mousePos.x>transform.position.x){
            fireAngle-=fireAngle;
        }
        nextFire=0;
        
        GameObject myBullet=Instantiate(Bullet,shootpoint.position,Quaternion.identity) as GameObject;
        myBullet.GetComponent<Rigidbody2D>().velocity=(mousePos-transform.position).normalized*BulletSpeed;
        myBullet.transform.eulerAngles=new Vector3(0,0,fireAngle);
        bulletCount-=1;  
    }
}
