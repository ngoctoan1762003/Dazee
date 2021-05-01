using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image HealthUI, HealthEf, ManaUI, ManaEf, StaminaUI, StaminaEf;
    public Player player;

    public GameObject SoulFragmentUI;

    public GameObject[] slot;

    public GameObject inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthUI.fillAmount = player.currentHealth / player.maxHp;
        ManaUI.fillAmount = player.currentMana / player.maxMana;
        StaminaUI.fillAmount = player.currentStamina / player.maxStamina;
        if (HealthEf.fillAmount > HealthUI.fillAmount)
        {
            HealthEf.fillAmount -= Time.deltaTime;
        }
        else
        {
            HealthEf.fillAmount = HealthUI.fillAmount;
        }
        if (ManaEf.fillAmount > ManaUI.fillAmount)
        {
            ManaEf.fillAmount -= Time.deltaTime;
        }
        else
        {
            ManaEf.fillAmount = ManaUI.fillAmount;
        }
        if (StaminaEf.fillAmount > StaminaUI.fillAmount)
        {
            StaminaEf.fillAmount -= Time.deltaTime;
        }
        else
        {
            StaminaEf.fillAmount = StaminaUI.fillAmount;
        }

        //Inventory UI-------------------------------
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

    //soul fragment----------------------------------------------
    public void chooseHP()
    {
        GameManager.instance.maxHp += 100;
        CloseSoulFragmentUI();
    }

    public void chooseMP()
    {
        GameManager.instance.maxMana += 100;
        CloseSoulFragmentUI();
    }

    public void chooseDEF()
    {
        GameManager.instance.maxDefense += 25;
        CloseSoulFragmentUI();
    }

    public void chooseDMG()
    {
        GameManager.instance.maxDamage += 50;
        CloseSoulFragmentUI();
    }

    public void chooseSTA()
    {
        GameManager.instance.maxStamina += 100;
        CloseSoulFragmentUI();
    }

    public void CloseSoulFragmentUI()
    {
        SoulFragmentUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void DisplayItem()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i < GameManager.instance.items.Count)
            {
                slot[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slot[i].transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.items[i].ItemImage;

                slot[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                slot[i].transform.GetChild(1).GetComponent<Text>().text = GameManager.instance.itemsNumber[i].ToString();
            }
            else
            {
                slot[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slot[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                slot[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                slot[i].transform.GetChild(1).GetComponent<Text>().text = null;
            }
        }
    }
}
