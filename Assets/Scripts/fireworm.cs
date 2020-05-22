using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fireworm : MonoBehaviour
{
    public GameObject firewormUI;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            firewormUI.SetActive(true);
            other.gameObject.GetComponent<Collect>().fireWormNumber+=1;
            StartCoroutine(waits());
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            
        }
    }
    IEnumerator waits(){
        yield return new WaitForSeconds(2f);
        firewormUI.SetActive(false);
        gameObject.SetActive(false);
    }
    


}
