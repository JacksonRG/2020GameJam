using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;

    void Start(){
        slider.value = 1.0f;
		fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float d){
        slider.value = d;
		fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
