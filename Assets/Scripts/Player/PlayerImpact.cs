using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpact : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float currentHealth;
    private int flickerAmount;
    private float flickerDuration;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        flickerAmount = 3;
        flickerDuration = 0.1f;
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = 100;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Invincible()
    {
        currentHealth -= damage;
        if(currentHealth < 0 )
        {
            Destroy(gameObject);
        }
        PlayerHUD healthAmount = gameObject.GetComponentInChildren<PlayerHUD>();
        healthAmount.SetLife(currentHealth);
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
