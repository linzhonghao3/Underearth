using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sp;
    private Collider2D door;
    public GameObject box;
    public GameObject mouse;
    public GameObject HideBackground;
    public Vector3 mousePos;
    public Sprite closedDoor;
    public bool beOpen=false;
    void Start(){
        animator=GetComponent<Animator>();
        animator.enabled=false;
        sp=GetComponent<SpriteRenderer>();
        door=GetComponent<BoxCollider2D>();
    }
    public void changeStatus(){
        if (!beOpen){
            beOpen=true;
            animator.enabled=true;
            sp.sortingOrder=4;
            door.enabled=false;
            box.layer=0; //开门时把隐藏区域layer改为0层默认，camera能看到
            HideBackground.layer=0;
            if (mouse!=null&&mousePos!=null){
                Instantiate(mouse,mousePos,Quaternion.identity);}
        }
        else {
            beOpen=false;
            animator.enabled=false;
            sp.sortingOrder=0;
            sp.sprite=closedDoor;
            door.enabled=true;
            box.layer=12; //关门时把隐藏区域layer改为12层“HideArea”，camera看不到
            HideBackground.layer=12;
        }
    }
}
