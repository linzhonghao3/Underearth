using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform shootpoint;
    public GameObject Bullet;
    public float fireRate=0.5f;
    public float nextFire;
    public float BulletSpeed=10f;
    private Animator animator;
    private AnimatorStateInfo ShootAnimation;
    public bool isShooting;
    private Move move;
    private CharacterControl2D characterControl2D;
    // Update is called once per frame
    void Start(){
        animator=GetComponent<Animator>();
        move=GetComponent<Move>();
        characterControl2D=GetComponent<CharacterControl2D>();
        ShootAnimation=animator.GetCurrentAnimatorStateInfo(0);
        
    }
    void FixedUpdate()
    {   
        nextFire+=Time.deltaTime;
        if (Input.GetMouseButtonDown(0)&&(nextFire>fireRate)){
            animator.SetTrigger("Shoot");
            isShooting=true;
            //move.enabled=false;
            //characterControl2D.enabled=false;
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
        //StartCoroutine(WaitShootEnd());

        
    }
    IEnumerator WaitShootEnd(){
        yield return null;
        AnimatorStateInfo ShootAnimation=animator.GetCurrentAnimatorStateInfo(0);
        if (ShootAnimation.IsName("CowBoyShoot")&&(ShootAnimation.normalizedTime>1f)){
            animator.SetTrigger("EndShoot");
            isShooting=false;
            //move.enabled=true;
            //characterControl2D.enabled=true;
        }
        else {
            StartCoroutine(WaitShootEnd());
        }
    }
}
