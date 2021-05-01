using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        inventoryUI.gameObject.SetActive(false);
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.isPaused == true)
            {
                inventoryUI.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                inventoryUI.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            GameManager.instance.isPaused = !GameManager.instance.isPaused;
        }
    }
}
