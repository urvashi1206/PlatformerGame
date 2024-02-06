using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    //Image healthbar;  
    public Slider slider;    //the red health bar sprite

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //value should never be set above max health
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
