using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaoGuang : MonoBehaviour
{
public List<GameObject> Lights;
public GameObject hintPanel;

    // Update is called once per frame
    void Update()
    {   
        if (transform.position.x>=59.1&&transform.position.x<=66){
            hintPanel.SetActive(true);
        }
        else hintPanel.SetActive(false);

        if (Input.GetKey(KeyCode.Q)){
            foreach(GameObject light in Lights){
                light.SetActive(true);
                hintPanel.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q)){
            foreach (GameObject light in Lights){
                light.SetActive(false);
            }
        }
    }
}
