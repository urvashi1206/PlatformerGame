using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear_Platform : MonoBehaviour
{
    public float fadeDelay = 3f;
    public float alphaValue = 0;
    public bool destroyGameObject = false;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyGameObject = true;
        spriteRenderer.color = new Color(1.0f, 0.6f, 0.7f, 1.0f);
        StartCoroutine(FadeTo(alphaValue, fadeDelay));
    }

    IEnumerator FadeTo(float aValue, float fadeTime)
    {
        float alpha = spriteRenderer.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha, aValue, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        if(destroyGameObject)
        {
            Destroy(gameObject);
        }
    }
}

