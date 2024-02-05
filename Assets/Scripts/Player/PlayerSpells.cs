using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    //point to shoot the spell from
    public Transform spellPoint;
    public GameObject bulletPrefab;
    public float manaValue;
    // Start is called before the first frame update
    void Start()
    {
        manaValue = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            Cast();
        }
    }

    void Cast()
    {
        //shoot spell
        manaValue--;
        if (manaValue >= 0) 
        {
            Instantiate(bulletPrefab, spellPoint.position, spellPoint.rotation);
            PlayerHUD manaAmount = gameObject.GetComponentInChildren<PlayerHUD>();
            manaAmount.SetMana(manaValue);
        }
    }
}
