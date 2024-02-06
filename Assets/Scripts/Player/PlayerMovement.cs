using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpHeight = 100f;
    public bool isGrounded;
    public float speed = 7.0f;

    private float h_velocity;
    private float v_velocity;
    private float v_acceleration;
    private float h_acceleration;
    private float deacceleration; 

    private float maxSpeed = 7.0f;

    public float gravity;


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

        CheckGrounded();
        CheckJump();
        CheckMove();
        UpdateSpeed();
    }

    void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            v_velocity = jumpHeight;
        }
    }

    void CheckMove()
    {
        int jump  = Input.GetAxisRaw("Horizontal");
    }

    void UpdateSpeed()
    {
        //apply negating forces
    
        v_velocity += v_acceleration;
        h_velocity += h_acceleration;
     
    }
}

