using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    
    Image healthbar;  //the red health bar sprite
    public Sprite earth;
    public Sprite fire;
    public Sprite freeze;
    public Image currentIcon;
    public TextMeshProUGUI spellName;
    Transform[] manaIcons; //the blue sprites that represent the player's mana; 
    [SerializeField]
    float maxHealth, maxMana;
    GameObject[] manaObjects;


    private void Start()
    {
        healthbar = GameObject.Find("Fill").GetComponent<Image>();

        manaIcons = GameObject.Find("Mana").GetComponentsInChildren<Transform>();
        manaObjects = GameObject.FindGameObjectsWithTag("Mana");
    }

    //value should never be set above max health
    public void SetLife(float value)
    {
        Debug.Log(value);
        healthbar.fillAmount = value / maxHealth;
    }

    public void SwapIcon(int index)
    {
        switch (index)
        {
            case 0:
                {
                    currentIcon.sprite = fire;
                    spellName.text = "Fire";
                    break; 
                }
            case 1:
                {
                    currentIcon.sprite = freeze;
                    spellName.text = "Freeze";
                    break;
                }
            case 2:
                {
                    currentIcon.sprite = earth;
                    spellName.text = "Earth";
                    break;
                }
        }
    }
    //value should never be set above max mana
    public void SetMana(float value)
    {
        //show all stars first, Check if there's mana icon

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
