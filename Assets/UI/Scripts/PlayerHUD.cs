using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    
    Image healthbar;  //the red health bar sprite
    Transform[] manaIcons; //the blue sprites that represent the player's mana; 
    [SerializeField]
    float maxHealth, maxMana; 

    private void Start()
    {
        healthbar = GameObject.Find("bar").GetComponent<Image>();

        manaIcons = GameObject.Find("Mana").GetComponentsInChildren<Transform>();
    }

    //value should never be set above max health
    public void SetLife(float value)
    {
        Debug.Log(value);
        healthbar.fillAmount = value / maxHealth;
    }

    //value should never be set above max mana
    public void SetMana(float value)
    {
        //show all stars first
        foreach (Transform child in manaIcons)
        {
            child.gameObject.SetActive(true);
        }

        //i subtracted by 1 to prevent out of bounds errors
        for (int i = (int)maxMana; i > value; i--)
        {
            manaIcons[i].gameObject.SetActive(false);
        }
    }

}
