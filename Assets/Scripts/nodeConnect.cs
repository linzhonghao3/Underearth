using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeConnect : MonoBehaviour
{
    private Rigidbody2D player_rigidbody;
    private HingeJoint2D node_hingejoint;
    private Rigidbody2D node_rigidbody;
    void Start()
    {
        player_rigidbody=GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        node_hingejoint=gameObject.GetComponent<HingeJoint2D>();
        node_rigidbody=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (node_hingejoint.connectedBody==player_rigidbody){
            player_rigidbody.gameObject.GetComponent<CharacterControl2D>().isHanging=true;
        }
        else player_rigidbody.gameObject.GetComponent<CharacterControl2D>().isHanging=false;
    }
}
