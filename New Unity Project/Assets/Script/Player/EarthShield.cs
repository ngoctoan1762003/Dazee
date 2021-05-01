using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShield : MonoBehaviour
{
    public float Health, currentHealth, lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (currentHealth <= 0 || lifeSpan<=0)
        {
            Destroy(gameObject);
        }    
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
