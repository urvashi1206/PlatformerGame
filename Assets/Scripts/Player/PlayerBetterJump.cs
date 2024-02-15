using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBetterJump : MonoBehaviour
{
    public float fallMultiplier = 3.5f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            Debug.Log("High");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        
    }
}
