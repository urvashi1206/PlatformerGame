using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Earth : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 0;
    public Rigidbody2D rb;
    public GameObject PlatformPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position += new Vector3(0, .25f, 0);
        //make the spell move forward
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Environment"  || collision.gameObject.tag == "Platform")
        {
            Debug.Log("Wall");
            ///spawn a standable platform where the projectile impacts
            GameObject.Instantiate(PlatformPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        if (collision.gameObject.tag == "Target")
        {
            Debug.Log("Enemy");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Bullet") 
        {
            Debug.Log("Firing Enemy");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

      
    }
}
