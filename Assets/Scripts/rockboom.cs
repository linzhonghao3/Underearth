using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockboom : MonoBehaviour
{   
    public GameObject explosion;
    private Collider2D rock;
    // Start is called before the first frame update
    void Start()
    {
        rock=GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Boom(){
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
