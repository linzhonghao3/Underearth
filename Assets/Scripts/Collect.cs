using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public Text fireWormNumberText;
    public int fireWormNumber=0;


    // Update is called once per frame
    void Update()
    {
        fireWormNumberText.text=fireWormNumber.ToString();
    }
}
