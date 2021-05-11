using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject Nofication;
    public Text NoficationText;
    public int SceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Nofication.SetActive(true);
            NoficationText.text = "Load Scene";
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Nofication.SetActive(true);
            NoficationText.text = "Load Scene";
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneIndex);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Nofication.SetActive(false);
        NoficationText.text = "";
    }
}
