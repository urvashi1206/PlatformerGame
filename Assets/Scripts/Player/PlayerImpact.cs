using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerImpact : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int maxHealth = 100;
    public float currentHealth;
    private int flickerAmount;
    private float flickerDuration;
    private int damage;
    public PlayerSpells playerSpells; 
    // Start is called before the first frame update
    void Start()
    {
        flickerAmount = 3;
        flickerDuration = 0.1f;
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        damage = 10;
        //to grab reference to player's health
        playerSpells = gameObject.GetComponent<PlayerSpells>();
    }

    public void Invincible(float damageNum)
    {
      
        playerSpells.AddHealth(-damageNum);
        StartCoroutine(DamageFlicker());
    }

    IEnumerator DamageFlicker()
    {
        for (int i = 0; i < flickerAmount; i++) 
        {
            sprite.color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
            yield return new WaitForSeconds(flickerDuration);
            sprite.color = Color.white;
            yield return new WaitForSeconds(flickerDuration);
        }
    }
}
