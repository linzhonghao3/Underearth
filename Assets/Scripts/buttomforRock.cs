using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttomforRock : MonoBehaviour
{   
    float Radius=1f;
    public LayerMask playerMask;
    public GameObject rollingRock;
    public Transform rockPoint;
    bool hasDone=false;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D player=Physics2D.OverlapCircle(transform.position,Radius,playerMask);
        if (player!=null){
            Instantiate(rollingRock,rockPoint.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
