using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    float timer;
    int waitingTime = 1;

    //Enemy Movement
    public float speed = 1f;
    public Transform pointA;
    public Transform pointB;

    public float attackRange = 7.0f;

    private Transform currentPoint;

    Rigidbody2D rb;

    //Player
    Transform playerTransform;

    //EnemyState
    EnemyState enemyState = EnemyState.Patrol;

    enum EnemyState
    {
        Patrol,
        Shoot,
    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
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
        switch(enemyState)
        {
            case EnemyState.Patrol:



                //Check if shoot
                if (playerTransform && (transform.position - playerTransform.position).magnitude < attackRange)
                {

                    enemyState = EnemyState.Shoot;
                    break;
                }

                //Change face direction
                Vector2 point = currentPoint.position - transform.position;
                if (currentPoint == pointB.transform)
                {
                    rb.velocity = new Vector2(speed, 0);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, 0);
                    transform.localScale = new Vector3(1, 1, 1);
                }

                //Move
                if (Vector2.Distance(transform.position,currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                {
                    currentPoint = pointA.transform;
                }
                if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                {
                    currentPoint = pointB.transform;
                }

            break;


            case EnemyState.Shoot:
                //For shoot bullets

                if (playerTransform && (transform.position - playerTransform.position).magnitude >= attackRange)
                {

                    enemyState = EnemyState.Patrol;
                    break;
                }
                //Check face direction
                if (playerTransform && (playerTransform.position - transform.position).x >= 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                    timer = 0;
                }
            break;


        }




    }
}


