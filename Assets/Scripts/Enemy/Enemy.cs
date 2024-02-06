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

    public float shootRange = 7.0f;
    public float meleeRange = 2.0f;
    public float meleeDetectRange = 5.0f;

    private Transform currentPoint;

    PlayerImpact playerImpact;

    Rigidbody2D rb;

    //Player
    Transform playerTransform;

    //EnemyState
    public EnemyState enemyState = EnemyState.Patrol;

    public enum EnemyType
    {
        ShootEnemy,
        MeleeEnemy,
    }

    public EnemyType enemyType;

    public enum EnemyState
    {
        Patrol,
        Shoot,
        Melee,
        ChasePlayer,
    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerImpact = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerImpact>(); ;
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
                
                //Check if attack according to different enemy type
                switch(enemyType)
                {
                    case EnemyType.MeleeEnemy:
                        if (playerTransform && (transform.position - playerTransform.position).magnitude < meleeDetectRange)
                        {
                            enemyState = EnemyState.ChasePlayer;
                            return;
                        }
                        break;

                    case EnemyType.ShootEnemy:
                        if (playerTransform && (transform.position - playerTransform.position).magnitude < shootRange)
                        {

                            enemyState = EnemyState.Shoot;
                            return;
                        }

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

                if (playerTransform && (transform.position - playerTransform.position).magnitude >= shootRange)
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

            case EnemyState.Melee:
                //Out of melee range
                if (playerTransform && (transform.position - playerTransform.position).magnitude >= meleeRange)
                {
                    enemyState = EnemyState.Patrol;
                    break;
                }

                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    playerImpact.Invincible();
                    timer = 0;
                }
                
                break;

            case EnemyState.ChasePlayer:
                //Out of melee range
/*                if (playerTransform && (transform.position - playerTransform.position).magnitude >= meleeDetectRange)
                {
                    enemyState = EnemyState.Patrol;
                    break;
                }*/

                if (playerTransform)
                {
                    Vector2 direction = playerTransform.position - transform.position;

                    // Check if player is in detect range
                    if (direction.magnitude >= meleeDetectRange)
                    {
                        enemyState = EnemyState.Patrol;
                    }
                    else if(direction.magnitude <= meleeRange)
                    {
                        enemyState = EnemyState.Melee;
                    }
                    else
                    {
                        if (direction.x >= 0)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }

                        rb.velocity = direction.normalized * speed;
                    }
                }

                break;

        }

    }

}


