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
    public GameObject currentSpell;
    public GameObject EarthProjectile;
    public GameObject FireProjectile;
    public GameObject FreezeProjectile;
    public float currentMana;
    public float currentHealth;
    public float manaValue;

    //mana regeneration
    //If the player hasn't cast a spell after certain amount of time, start slowly recovering mana
    public bool isRegenerating = false;
    public float regenThreshold = 3f;
    public float regen_timer = 0f;
    public float time_not_casting = 0f; 
    [SerializeField]
    public  float regenSpeed = .5f;

    private int currentSpellIndex; 

    private Spell[] spells = new Spell[] { Spell.Fire, Spell.Freeze, Spell.Earth};
    // Start is called before the first frame update
    void Start()
    {
        currentSpell = FireProjectile;

        currentMana = 5;
        currentSpellIndex = 0;
        manaValue = 1; //starting with the 'Fire' spell
}




    // Update is called once per frame
    void Update()
    {

        PlayerHUD manaAmount = gameObject.GetComponentInChildren<PlayerHUD>();

        currentMana = Mathf.Clamp(currentMana, 0, 5);
        manaAmount.SetMana(currentMana);

    

        //cast the current equipped spell.
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Cast();
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
            }

                PlayerHUD hud = gameObject.GetComponentInChildren<PlayerHUD>();
                hud.SwapIcon(currentSpellIndex);

                switch (currentSpellIndex)
                {
                    case 0:
                        {
                            currentSpell = FireProjectile;
                            manaValue = 1;
                            break;
                        }
                    case 1:
                        {
                            currentSpell = FreezeProjectile;
                            manaValue = 2;
                            break;
                        }
                    case 2:
                        {
                            currentSpell = EarthProjectile;
                            manaValue = 2;
                            break;
                        }

                   
                }
              

            

           
        }
      
    }
    private void FixedUpdate()
    {
        time_not_casting += Time.fixedDeltaTime;

        if(time_not_casting > regenThreshold)
        {
            isRegenerating = true;          
        }

        if (isRegenerating)
        {
            regen_timer += Time.deltaTime;
            if(regen_timer > regenSpeed)
            {
                regen_timer = 0f;
                currentMana++;
          
            }
          
        }
    
    }
    void Cast()
    {
        //shoot spell
        if ((currentMana - manaValue) >= 0)
        {
            //if the player casts a spell, stop regeneration
            isRegenerating = false;
            time_not_casting = 0f;
            regen_timer = 0f;

            Instantiate(currentSpell, spellPoint.position, spellPoint.rotation);
            currentMana = currentMana - manaValue;

 
        }

    }
    void SwapSpell()
    {

    }
}
