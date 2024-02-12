using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Pickup : MonoBehaviour
{
    public float recoverAmount = 50;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        //When the player collides with this object, 
        if(obj.tag == "Player")
        {
            obj.GetComponent<PlayerSpells>().AddHealth(recoverAmount);
            //delete self
            Destroy(gameObject);
        }
    }
}
