using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public UIBar healthBar;
    public UIBar dashBar;
    [SerializeField] private float health;
    [SerializeField] private float barOffset = 0.163f;
    float barScaleIncrementAmout;
    // Start is called before the first frame update
    void Start()
    {
        barScaleIncrementAmout = 0.15f * healthBar.transform.localScale.x;
    }


    public void UpdateBarMaxValue(UIBar bar, float newMax, float currentValue)
    {
        bar.SetMaxValue(newMax);
        bar.SetValue(currentValue);
        
        bar.transform.localScale -= Vector3.left * barScaleIncrementAmout;
        bar.transform.position -= Vector3.left * barOffset;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    
}
