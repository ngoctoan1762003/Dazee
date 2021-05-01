using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDust : MonoBehaviour
{
    public GameObject Player;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        if (Player.GetComponent<Player>().isFaceRight == false) { 
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("DashDust");
    }

    void Flip()
    {
        gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
