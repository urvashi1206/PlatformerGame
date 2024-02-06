using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    public float duration = 5f;
    private float timer = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(timer > duration)
        {
            Destroy(gameObject);
        }

        timer += Time.fixedDeltaTime;


    }
}
