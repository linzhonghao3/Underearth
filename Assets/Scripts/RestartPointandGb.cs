using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPointandGb : MonoBehaviour
{  
    public List<GameObject> gameObjectstoReset;
    public List<Vector3> resetPlaces;


    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Move>().restartPoint=transform.position;
            for (int i=0;i<gameObjectstoReset.Count;i++){
                gameObjectstoReset[i].transform.position=resetPlaces[i];
            }         
        }
    }
}