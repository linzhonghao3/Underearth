using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Player"){
            Vector3 playerPos=other.gameObject.transform.position;
            Vector2 difference=transform.position-playerPos;
            other.gameObject.transform.position=new Vector2(playerPos.x-difference.x*2,playerPos.y-difference.y*2);
            other.gameObject.GetComponent<Player>().Damage(20);
        }
    }
}
