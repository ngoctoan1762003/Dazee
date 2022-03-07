using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Archer archer;
    public Image HPLeft;
    public GameObject BossHP;
    public Animator CManim;

    // Update is called once per frame
    void Update()
    {
        HPLeft.fillAmount = archer.currentHealth / archer.maxHealth;
        if (archer.currentHealth <= 0)
        {
            BossHP.SetActive(false);
            CManim.SetBool("FightBoss", false);
        }
    }
}
