using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPointUIController : MonoBehaviour
{
    [SerializeField] private Image statusBarImage;
    [SerializeField] private Image statusIconImage;

    public void SetStatusBarValue(float value)
    {
        statusBarImage.fillAmount = value;
        statusBarImage.color = value switch
        {
            <= 0.33f => Color.red,
            <= 0.66f => Color.yellow,
            _ => Color.green
        };
        
        SetStatusIconColor(value);
    }
    
    public void SetStatusIconColor(float value)
    {
        statusIconImage.color = value switch
        {
            <= 0.33f => Color.red,
            <= 0.66f => Color.yellow,
            _ => Color.green
        };
    }
}
