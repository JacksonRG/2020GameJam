using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    void Start(){
        slider.value = .5f;
    }

    public void SetHealth(float d){
        slider.value = d;
    }
}
