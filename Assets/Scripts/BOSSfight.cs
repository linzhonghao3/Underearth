using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSfight : MonoBehaviour
{
    public GameObject bossPrefab;
    private Transform playerPos;
    bool beginBossFight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerPos.position.x>transform.position.x+10f&&!beginBossFight){
            bossPrefab.SetActive(true);
            transform.position=new Vector3(transform.position.x,23.54f,0f);
            beginBossFight=true;
        }
    }
}
