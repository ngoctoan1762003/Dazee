using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    public float lifeSpan;
    public float damage;


    private void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("EarthShield"))
        {
            collision.gameObject.GetComponent<EarthShield>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}


