using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    
    Image healthbar;  //the red health bar sprite
    public Sprite earth;
    public Image icon;
    public GameObject bar;
    Image[] manaIcons; //the blue sprites that represent the player's mana; 
    [SerializeField]
    float maxHealth, maxMana;
    bool isPaused;
    public GameObject pauseScreen;
    public GameObject pauseButton;


    private void Start()
    {
        healthbar = GameObject.Find("Fill").GetComponent<Image>();
        manaIcons = bar.GetComponentsInChildren<Image>();
        isPaused = false; 

        SetMana(2);
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
        //hide all stars first, Check if there's mana icon
        foreach (Image child in manaIcons)
        {
            child.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < value; i++)
        {
            manaIcons[i].gameObject.SetActive(true);
        }
    }

    //enables / disables pause screen
    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }

        if (!isPaused)
        {
            Time.timeScale = 1; 
        }

        
    }

}
