using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D r2D;

    public float maxHealth = 100;
    float currentHealth;

    public GameObject Player;
    public float speed;

    public LayerMask playerMask;
    public float attackRadius, damage, attackDuration, attackCurrentTime;
    public bool attackReady = true;

    //AI
    public float distance, attackDistance;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        r2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //chase----------------------------
        if(Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) <= distance &&
           Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) >= attackDistance)
        {
            Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position;
            direction.Normalize();
            direction.y = 0;
            r2D.velocity = direction * speed;
        }

        //attack-----------------------------
        if(Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) < attackDistance){
            if (attackReady == true)
            {
                Collider2D player = Physics2D.OverlapCircle((Vector2)transform.position, attackRadius, playerMask);
                if (player != null)
                {
                    player.GetComponent<Player>().TakeDamage(damage);
                    if (player.transform.position.x > transform.position.x)
                    {
                        player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(150, 100));
                    }
                    else
                    {
                        player.GetComponent<Player>().DamagedKnockBackForce(new Vector2(-150, 100));
                    }
                    attackReady = false;
                }            
            }
        }

        if (attackReady == false)
        {
            if (attackCurrentTime <= 0 && attackReady == false)
            {
                attackReady = true;
                attackCurrentTime = attackDuration;
            }
            attackCurrentTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        KnockBack(new Vector2(150, 100));
        if (currentHealth <= 0) 
        { 
            Dead();
        }
    }

    void KnockBack(Vector2 KnockForce)
    {
        r2D.velocity = Vector2.zero;
        if (Player.transform.position.x > transform.position.x)
        {
            r2D.AddForce(new Vector2(-KnockForce.x, KnockForce.y));
        }
        else r2D.AddForce(new Vector2(KnockForce.x, KnockForce.y));
    }

    void Dead()
    {
        Debug.Log(name + "Dead");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

}
