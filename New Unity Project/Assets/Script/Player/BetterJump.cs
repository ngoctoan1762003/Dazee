using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    float fallMultiplier = 2.5f, jumpMultiplier = 2f;
    Rigidbody2D r2D;
    // Start is called before the first frame update
    void Start()
    {
        r2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (r2D.velocity.y < 0) {
            r2D.velocity += Vector2.up * Physics2D.gravity.y * ( fallMultiplier -1 ) * Time.deltaTime;
        }else if (r2D.velocity.y>0 && !Input.GetKey(KeyCode.Space)) {
            r2D.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
