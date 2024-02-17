using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivate : MonoBehaviour
{
    public float activationDelay = 5f;  // Adjust the time delay as needed
    public GameObject objectToActivate;
    public Disappear_Platform platform;
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Disappear_Platform>();
        Invoke("ActivateObject", activationDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //if (platform.isEntered == true) 
        //{
        
        //}
    }

    void ActivateObject()
    {
        objectToActivate.SetActive(true);
        objectToActivate.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.3f, 1.0f);
        // You can also reset the alpha value if needed, depending on your shader and material setup.
        // objectToActivate.GetComponent<Renderer>().material.color = Color.white;
    }
}
