using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disappear_Platform : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public bool isEntered;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isEntered = true;
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        Color startColor = spriteRenderer.material.color;
        float t = 0f;
        //spriteRenderer.color = new Color(1.0f, 0.6f, 0.7f, 1.0f);
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            spriteRenderer.material.color = Color.Lerp(startColor, Color.clear, t);
            yield return null;
        }
        
        // Reset for the next fade-in
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        spriteRenderer.material.color = startColor;

        // Fade in
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            spriteRenderer.material.color = Color.Lerp(Color.clear, startColor, t);
            yield return null;
        }
        //yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(true); // Activate the GameObject after fading in
    }
}

