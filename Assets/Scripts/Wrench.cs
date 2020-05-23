using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public List<GameObject> gearList;
    public GameObject door;

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Hook"){
            Debug.Log("Hit the buttom");
            transform.rotation=Quaternion.Euler(0f,180f,60f);
            foreach (GameObject gears in gearList){
                gears.GetComponent<gearRolling>().enabled=false;
            }
            door.GetComponent<Door>().changeStatus();
        }

    }
}
