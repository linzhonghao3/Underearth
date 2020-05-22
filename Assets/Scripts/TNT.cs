using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{   
    public GameObject boomEffect;
    public GameObject areaToDestroy;
    
    public void Boom(){
        this.gameObject.GetComponent<SpriteRenderer>().enabled=false;//TNT消失
        Instantiate(boomEffect,transform.position,Quaternion.identity); //原地生成爆炸效果
        StartCoroutine(DestroyArea());

    }
    IEnumerator DestroyArea(){
        yield return new WaitForSeconds(0.5f);
        areaToDestroy.SetActive(false);
        Destroy(gameObject);
    }
}
