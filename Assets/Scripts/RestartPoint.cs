using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPoint : MonoBehaviour
{   
    public List<GameObject> buttoms;
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Move>().restartPoint=transform.position;      
        }
        if (buttoms!=null){
            foreach(GameObject buttom in buttoms)
            {
                buttom.SetActive(true);
            }
        }
    }
}
