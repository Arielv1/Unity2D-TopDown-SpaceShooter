using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxValue(float max)
    {
        slider.maxValue = max;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
