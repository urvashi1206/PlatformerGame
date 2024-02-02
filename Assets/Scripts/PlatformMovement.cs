using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public float speed = 10f;
    Vector3 pointA;
    Vector3 pointB;
    private float moveValue = 5.0f;

    void Start()
    {
        pointA = transform.position;
        pointB = new Vector2(transform.position.x + moveValue, transform.position.y);
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }
}
