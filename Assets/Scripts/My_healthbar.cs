using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class My_healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxValue(int health){
        slider.maxValue = health;
        slider.value = health;
        fill.color=gradient.Evaluate(1f);
    }
    public void SetHeal(int health){
        slider.value=health;
        fill.color=gradient.Evaluate(slider.normalizedValue);
    }
}
