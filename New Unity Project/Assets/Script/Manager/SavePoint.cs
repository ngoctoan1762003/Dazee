using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    public GameObject SaveUI;
    public GameObject Nofication;
    public Text NoficationText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Nofication.SetActive(true);
            NoficationText.text = "Save";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Nofication.SetActive(true);
            NoficationText.text = "Save";
            if (Input.GetKeyDown(KeyCode.F))
            {
                SaveUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Nofication.SetActive(false);
        NoficationText.text = "";
    }

    public void Save0()
    {
        GameManager.instance.SaveGame(0);
        Debug.Log("saved");
        SaveUI.SetActive(false);
    }

    public void Save1()
    {
        GameManager.instance.SaveGame(1);
        Debug.Log("saved");
        SaveUI.SetActive(false);
    }
}
