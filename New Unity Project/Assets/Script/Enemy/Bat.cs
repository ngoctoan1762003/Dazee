using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject Player;
    public float distance, attackDistance;
    public Rigidbody2D r2D;
    public float speed;

    public GameObject Bullet;
    public float attackDuration, attackCurrentTime;
    public bool attackReady = true;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        r2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //--ChasePlayer--------------------
        if (Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) <= distance &&
            Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) >= attackDistance)
        {
            Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position;
            direction.Normalize();
            direction.y = 0;
            r2D.velocity = direction * speed;
        }

        if (Vector2.Distance((Vector2)transform.position, (Vector2)Player.transform.position) < attackDistance)
        {
            if (attackReady == true)
            {
                GameObject bulletClone = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
            }
            attackReady = false;
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
}

