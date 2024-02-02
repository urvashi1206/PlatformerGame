using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    BoxCollider2D collider; //this enemy's collider

    [SerializeField]
    float speed; //movement speed 
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        currentPosition = this.transform.position; 
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentPosition.x += speed;

        
        this.transform.position = currentPosition;
    }

    void OnCollisionEnter(Collider other)
    {
        //when the enemy hits a wall or pit, reverse direction
        speed = speed * -1; 
    }

}
