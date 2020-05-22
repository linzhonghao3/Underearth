using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longwater : MonoBehaviour
{
    // 此脚本是为了解决海水3，4过长 会影响到跷跷板区域特设
    // 以另一个碰撞体代替海洋区域，这个脚本用于海水的流动，与water的前半部分完全一致
    public float x;
    public Vector3 place;
    private float moveSpeed=2f;

    void Update()
    {
        transform.position=new Vector3(transform.position.x-moveSpeed*Time.deltaTime,transform.position.y,transform.position.z);
        if (transform.position.x<x){
            transform.position=place;
        }
    }

    // Update is called once per frame
}
