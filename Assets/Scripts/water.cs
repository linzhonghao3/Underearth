using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    public float x;
    public Vector3 place;
    private float moveSpeed=2f;

    private List<GameObject> Woods=new List<GameObject>();
    public float xleftRegion;
    public float xrightRegion;
    private float playerPosX;

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x-moveSpeed*Time.deltaTime,transform.position.y,transform.position.z);
        if (transform.position.x<x){
            transform.position=place;
        }
        playerPosX=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
    }
    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag=="Wood"){
            if (!Woods.Contains(other.gameObject))
                {Woods.Add(other.gameObject);}

            foreach (GameObject wood in Woods){
                wood.GetComponent<Rigidbody2D>().velocity=new Vector2(0f,0f);
            }
        }
        if (other.gameObject.tag=="Player"){
            if (playerPosX>=xleftRegion&&playerPosX<=xrightRegion){
                other.gameObject.GetComponent<Move>().Die();
            }
        }
    }
}
