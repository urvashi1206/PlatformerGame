using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpells : MonoBehaviour
{
    //point to shoot the spell from
    public Transform spellPoint;
    public PlayerHUD playerHud;
    public GameObject EarthProjectile;

    public float maxMana = 2;
    public float maxHealth = 100;
    public float currentMana;
    private float currentHealth;

    //mana regeneration
    //If the player hasn't cast a spell after certain amount of time, start slowly recovering mana
    private bool isRegenerating = false;
    public float regenThreshold = 3f;
    private float regen_timer = 0f;
    private float time_not_casting = 0f;
    [SerializeField]
    public float regenSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;

        AddHealth(-10);
    }
    // Update is called once per frame
    void Update()
    {

        //cast the Earth Spell
        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }

    }

    public void AddHealth(float num)
    {

        currentHealth += num;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
        Debug.Log(currentHealth);
        playerHud.SetLife(currentHealth);
    }
    public void AddMana(float num)
    {
        currentMana += num;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        playerHud.SetMana(currentMana);
    }

    //used to facilitate mana regenearation
    void FixedUpdate()
    {
        time_not_casting += Time.fixedDeltaTime;

        if (time_not_casting > regenThreshold)
        {
            isRegenerating = true;
        }

        if (isRegenerating)
        {
            regen_timer += Time.deltaTime;
            if (regen_timer > regenSpeed)
            {
                regen_timer = 0f;
                AddMana(1);

            }

        }

    }
    void Cast()
    {
        Debug.Log("pew pew");
        //shoot spell if there is mana remaining
        if (currentMana > 0)
        {
            //if the player casts a spell, stop regeneration
            isRegenerating = false;
            time_not_casting = 0f;
            regen_timer = 0f;

            Instantiate(EarthProjectile, spellPoint.position, spellPoint.rotation);
            AddMana(-1);
        }

    }
}


