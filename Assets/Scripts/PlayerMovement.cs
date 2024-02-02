using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector3 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
    void FixedUpdate()
    {
        moveCharacter(movement);
    }


    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

}

