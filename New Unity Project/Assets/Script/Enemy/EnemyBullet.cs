using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    public float damage, speed;
    public Player player;
    public Vector2 direction;
    public Rigidbody2D r2D;
    public float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        r2D = gameObject.GetComponent<Rigidbody2D>();
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        r2D.velocity = direction * speed;
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
            player.TakeDamage(100);

            if (player.transform.position.x > transform.position.x)
            {
                player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(150, 100));
            }
            else
            {
                player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(-150, 100));
            }
        }

        if (collision.collider.CompareTag("EarthShield"))
        {
            collision.gameObject.GetComponent<EarthShield>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
