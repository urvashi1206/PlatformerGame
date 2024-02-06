using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerImpact : MonoBehaviour
{
    private SpriteRenderer sprite;
    public int maxHealth = 100;
    public float currentHealth;
    private int flickerAmount;
    private float flickerDuration;
    private int damage;
    public HealthBar health;
    // Start is called before the first frame update
    void Start()
    {
        flickerAmount = 3;
        flickerDuration = 0.1f;
        sprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        damage = 10;
        health = GameObject.Find("Health").gameObject.GetComponent<HealthBar>();
        health.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Invincible(float damageNum)
    {
        currentHealth -= damageNum;
        Debug.Log(currentHealth);
        if(currentHealth <= 0 )
        {
            SceneManager.LoadScene("EndScreen");
        }
        health.SetHealth(currentHealth);
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
