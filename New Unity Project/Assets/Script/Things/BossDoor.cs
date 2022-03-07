using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Rigidbody2D r2;
    public Animator CManim;
    public GameObject BossHP;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            r2.bodyType = RigidbodyType2D.Dynamic;
            CManim.SetBool("FightBoss", true);
            BossHP.SetActive(true);
            Destroy(gameObject);
        }
    }
}
