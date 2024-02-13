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
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    public float shootRange = 7.0f;
    public float meleeRange = 2.0f;
    public float meleeDetectRange = 5.0f;

    public float meleeDamage = 20f; 

    private Transform currentPoint;

    PlayerImpact playerImpact;

    Rigidbody2D rb;

    Animator animator;

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
        animator = GetComponent<Animator>();
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
        //Check facing direction
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        switch (enemyState)
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
                            animator.SetTrigger("isShoot");
                            enemyState = EnemyState.Shoot;
                            return;
                        }
                        break;
                }

                if (currentPoint == null)
                    return;

                //Get target position
                Vector2 direction1 = (currentPoint.position - transform.position).normalized;
                Vector2 targetPosition = rb.position + direction1 * speed * Time.fixedDeltaTime;

                //Move
                rb.MovePosition(targetPosition);

                //Check if get correct position
                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
                {
                    currentPoint = currentPoint == pointA ? pointB : pointA;
                }
                if (direction1.x >= 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }


                break;


            case EnemyState.Shoot:
                //For shoot bullets

                rb.velocity = Vector2.zero;

                Vector2 direction2 = playerTransform.position - transform.position;

                if (direction2.x >= 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                if (playerTransform && (transform.position - playerTransform.position).magnitude >= shootRange)
                {
                    animator.SetTrigger("isRun");
                    enemyState = EnemyState.Patrol;

                    return;
                }

/*                timer += Time.deltaTime;
                if (timer > waitingTime)
                {

                }*/
                break;

            case EnemyState.Melee:
                //Out of melee range
                if (playerTransform && (transform.position - playerTransform.position).magnitude >= meleeRange)
                {
                    animator.SetTrigger("isRun");
                    enemyState = EnemyState.Patrol;
                    return;
                }

/*                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    playerImpact.Invincible(meleeDamage);
                    timer = 0;
                }*/
                
                break;

            case EnemyState.ChasePlayer:
                if (playerTransform)
                {
                    Vector2 direction = playerTransform.position - transform.position;
                    Vector2 toPlayer = playerTransform.position - transform.position;
                    float distanceToA = Vector2.Distance(transform.position, pointA.position);
                    float distanceToB = Vector2.Distance(transform.position, pointB.position);

                    //check if enemy is outside the range between pointA and pointB
                    if (transform.position.x < pointA.position.x || transform.position.x > pointB.position.x)
                    {
                        //find closest point to return to
                        Transform closestPoint = distanceToA < distanceToB ? pointA : pointB;
                        direction = (closestPoint.position - transform.position).normalized;

                        //change enemy state to Patrol if get the closest point
                        if (Vector2.Distance(transform.position, closestPoint.position) < 0.5f)
                        {
                            enemyState = EnemyState.Patrol;
                            return;
                        }
                    }
                    else if (toPlayer.magnitude <= meleeRange)
                    {
                        animator.SetTrigger("isAttack");
                        enemyState = EnemyState.Melee;
                    }
                    else if (toPlayer.magnitude >= meleeDetectRange)
                    {
                        enemyState = EnemyState.Patrol;
                    }

                    // Move towards the player or the closest point
                    rb.velocity = direction * speed;

                }

                break;

        }

    }

    public void Shoot()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        Quaternion bulletRotation;

        if (toPlayer.x >= 0)
        {
            bulletRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            bulletRotation = Quaternion.Euler(0, 0, 180);
        }

        Instantiate(bulletPrefab, shootPoint.position, bulletRotation);
        //timer = 0;
    }


    public void MeleeAttack()
    {
        playerImpact.Invincible(meleeDamage);
    }
}


