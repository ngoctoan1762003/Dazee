using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Archer : MonoBehaviour
{
    public Rigidbody2D r2D;
    public Player player; 

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

    // Start is called before the first frame update
    void Start()
    {
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
                NormalAttack();
                attackTime = attackDuration;
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
}
