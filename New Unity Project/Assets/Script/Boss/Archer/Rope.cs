using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float lifeSpan=3f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0) Destroy(gameObject);
    }
}
