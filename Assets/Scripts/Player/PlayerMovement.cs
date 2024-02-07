using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody2D rb;
    Camera cam; 
    public float jumpHeight = 100f;
    public bool isGrounded;
    private float jumpCount = 0;
    private float extraJumps = 0;
    public float speed = 7.0f;
    float jumpCoolDown;
    float facing;
    bool facingRight = true;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform feet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //keep the camera from rotating
        float dirX = Input.GetAxisRaw("Horizontal");
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < extraJumps)
            {
                rb.velocity = new Vector3(0, jumpHeight, 0);
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
        Vector3 currenScale = gameObject.transform.localScale;
        currenScale.x *= -1;
        gameObject.transform.localScale = currenScale;

        facingRight = !facingRight;
    }
}

