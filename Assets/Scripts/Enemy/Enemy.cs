using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    float timer;
    int waitingTime;
    // Start is called before the first frame update
    void Start()
    {
        waitingTime = 3;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            timer = 0;
        }
    }
}
