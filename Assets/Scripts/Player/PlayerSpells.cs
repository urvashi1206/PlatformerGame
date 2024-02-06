using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Spell
{
    Fire, 
    Freeze, 
    Earth
}
public class PlayerSpells : MonoBehaviour
{
    //point to shoot the spell from
    public Transform spellPoint;
    public GameObject bulletPrefab;
    public GameObject EarthProjectile;
    public GameObject FireProjectile;
    public GameObject FreezeProjectile;
    public float currentMana;
    public float manaValue;

    //mana regeneration
    bool isRegenerating;
    float regenThreshold;
    [SerializeField]
    float regenSpeed;

    private int currentSpellIndex; 

    private Spell[] spells = new Spell[] { Spell.Fire, Spell.Freeze, Spell.Earth};
    // Start is called before the first frame update
    void Start()
    {
        currentMana = 5;
        currentSpellIndex = 0; 
    }

    //If the player hasn't cast a spell after certain amount of time, start slowly recovering mana
    void RegenerateMana()
    {

    }

    // Update is called once per frame
    void Update()
    {

        PlayerHUD manaAmount = gameObject.GetComponentInChildren<PlayerHUD>();
        manaAmount.SetMana(currentMana);

        //cast the current equipped spell.
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Cast(currentSpellIndex);
        }

        //swap to the next spell; in order of Fire -> Freeze -> Earth
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentSpellIndex == 2)
            {
                currentSpellIndex = 0;
            }
            else
            {
                currentSpellIndex++;


                switch (currentSpellIndex)
                {
                    case 0:
                        {
                            manaValue = 1;
                            break;
                        }
                    case 1:
                        {
                            manaValue = 2;
                            break;
                        }
                    case 2:
                        {
                            manaValue = 2;
                            break;
                        }

                   
                }
                Debug.Log(currentSpellIndex);

            }

           
        }
      
    }

    void Cast(int currentSpell)
    {
        //shoot spell
        if ((currentMana - manaValue) >= 0)
        {
            Instantiate(bulletPrefab, spellPoint.position, spellPoint.rotation);
            currentMana = currentMana - manaValue;

        }

    }
    void SwapSpell()
    {

    }
}
