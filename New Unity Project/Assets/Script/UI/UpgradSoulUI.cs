using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradSoulUI : MonoBehaviour
{
    public GameObject SoulFragmentUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoulFragmentUI.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }
}
