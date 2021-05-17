using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    public float lifeSpan;
    public float damage;

    private void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") )
        {
            collision.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

