using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float zoomDist; // zoom distance
    public float yOffset; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;

        this.transform.position = new Vector3(playerPos.x, playerPos.y + yOffset, zoomDist);

    }
}
