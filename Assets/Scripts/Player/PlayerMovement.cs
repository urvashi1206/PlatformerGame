using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody2D rb;
    Camera cam; 
    public float jumpvelocity = 15f;
    public bool isGrounded;
    private float jumpCount = 0;
    public float speed = 7.0f;
    float jumpCoolDown;
    float facing;
    bool facingRight = true;
    public Animator animator;
    private Vector3 respawnPoint;
    private int CheckPointCount;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform feet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
        CheckPointCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);
        //keep the camera from rotating
        float dirX = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(dirX));
        if (dirX != 0)
        {
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        }

        if (dirX > 0 && !facingRight) 
        {
            Flip();
        }

        if (dirX < 0 && facingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) 
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpvelocity;
                jumpCount++;
            }
        }
        CheckGrounded();
    }

    void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
        }
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        /*Vector3 currenScale = gameObject.transform.localScale;
        currenScale.x *= -1;
        gameObject.transform.localScale = currenScale;*/
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            if (CheckPointCount == 0)
            {
                PlayerImpact impact = gameObject.GetComponent<PlayerImpact>();
                impact.Invincible(100.0f);
            }
            else
            {
                transform.position = respawnPoint;
            }
        }
        else if (collision.gameObject.tag == "CheckPoint")
        {
            CheckPointCount++;
            respawnPoint = transform.position;
        }
    }
}

