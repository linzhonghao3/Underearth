using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public My_healthbar healthbar;
    public int maxHp=100;
    public int currentHP;
    private Animator animator;

    void Start()
    {   
        animator=GetComponent<Animator>();
        currentHP=maxHp;
        healthbar.SetMaxValue(maxHp);
    }
    public void Damage(int damage){
        currentHP-=damage;
        healthbar.SetHeal(currentHP);
        if (currentHP<=0){
            transform.gameObject.GetComponent<Move>().enabled=false;
            animator.SetBool("isJumping",false);
            animator.SetBool("isFalling",false);
            animator.SetBool("isDead",true);
            Invoke("Restart",2);}
        
    }
    void Restart(){
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Enemy"){
            Damage(20);
            Vector2 difference= other.transform.position-transform.position;
            transform.position=new Vector2(transform.position.x-difference.x/2,
                                            transform.position.y-difference.y/2);
            other.transform.position=new Vector2(other.transform.position.x+difference.x/2,
                                                other.transform.position.y+difference.y/2);

       }
       else return;
    }
}
