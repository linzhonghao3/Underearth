using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSSfight : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject BOSSpanel;
    public GameObject Playerpanel;
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
            BOSSpanel.SetActive(true);
            Playerpanel.SetActive(true);
            Camera.main.GetComponent<AudioSource>().enabled=false;
            bossPrefab.GetComponent<AudioSource>().Play();
        }
    }
}
