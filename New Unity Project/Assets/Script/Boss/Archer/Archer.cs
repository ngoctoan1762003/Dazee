using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Archer : MonoBehaviour
{
    public Rigidbody2D r2D;
    public Player player;

    //Stat
    public float maxHealth = 10000;
    public float currentHealth;

    //NORMAL ATTACK
    public GameObject Arrow;
    public float speed;
    public Vector3 rightOffset;
    public Vector3 leftOffset;
    public float shotDuration;

    //ATTACK CONTROLLER
    public float attackRange;
    public float attackDuration;
    public float attackTime;

    //TIE
    public GameObject Rope;

    //RAIN
    public GameObject RainArrow;

    //TELE
    public Transform[] telePos;

    //WALL
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        r2D = gameObject.GetComponent<Rigidbody2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) < attackRange)
        {
            if (attackTime <= 0)
            {
                int rand = Random.Range(1,6);
                Debug.Log(rand);
                if(rand==1) {
                    if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 1) NormalAttack();
                    else Rain();
                    attackTime = attackDuration;
                }
                if(rand==2)
                {
                    TieRope();
                    attackTime = attackDuration;
                }
                if (rand == 3)
                {
                    Rain();
                    attackTime = attackDuration;
                }
                if (rand == 4)
                {
                    Tele();
                    attackTime = attackDuration;
                }
                if (rand == 5)
                {
                    WallMake();
                    attackTime = attackDuration-1.5f;
                }
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
        
    }

    void NormalAttack()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        GameObject ArrowClone;
        if (player.transform.position.x > transform.position.x)
        {
            ArrowClone = Instantiate(Arrow, transform.position + rightOffset, Quaternion.identity) as GameObject;
            ArrowClone.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else {
            ArrowClone = Instantiate(Arrow, transform.position + leftOffset, Quaternion.identity) as GameObject;
            ArrowClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        yield return new WaitForSeconds(shotDuration);
        GameObject ArrowClone1;
        if (player.transform.position.x > transform.position.x)
        {
            ArrowClone1 = Instantiate(Arrow, transform.position + rightOffset, Quaternion.identity) as GameObject;
            ArrowClone1.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else
        {
            ArrowClone1 = Instantiate(Arrow, transform.position + leftOffset, Quaternion.identity) as GameObject;
            ArrowClone1.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        yield return new WaitForSeconds(shotDuration);
        GameObject ArrowClone2;
        if (player.transform.position.x > transform.position.x)
        {
            ArrowClone2 = Instantiate(Arrow, transform.position + rightOffset, Quaternion.identity) as GameObject;
            ArrowClone2.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        else
        {
            ArrowClone2 = Instantiate(Arrow, transform.position + leftOffset, Quaternion.identity) as GameObject;
            ArrowClone2.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
    }

    public void TieRope()
    {
        GameObject RopeClone = Instantiate(Rope, player.transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(player.Tied(3f));
    }
    
    public void Rain()
    {
        StartCoroutine(RainShot());
    }

    IEnumerator RainShot()
    {
        GameObject ArrowClone;
        ArrowClone = Instantiate(RainArrow, new Vector3(player.transform.position.x, 10, 0), Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(shotDuration);
        ArrowClone = Instantiate(RainArrow, new Vector3(player.transform.position.x, 10, 0), Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(shotDuration);
        ArrowClone = Instantiate(RainArrow, new Vector3(player.transform.position.x, 10, 0), Quaternion.identity) as GameObject;
    }

    public void Tele()
    {
        int rand = Random.Range(1, 6);
        transform.position = telePos[rand].position;
    }

    public void WallMake()
    {
        GameObject WallClone;
        if (player.transform.position.x > transform.position.x)
        {
            WallClone = Instantiate(wall, new Vector3(player.transform.position.x + 1, player.transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            WallClone = Instantiate(wall, new Vector3(player.transform.position.x - 1, player.transform.position.y, 0), Quaternion.identity);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("hit");
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
        if (player.transform.position.x > transform.position.x)
        {
            r2D.AddForce(new Vector2(-KnockForce.x, KnockForce.y));
        }
        else r2D.AddForce(new Vector2(KnockForce.x, KnockForce.y));
    }


    public void Dead()
    {
        Destroy(gameObject);
    }
}
