using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;

public class MainMenu : MonoBehaviour
{
    public GameObject LoadAndChooseFileUI;
    public void Play()
    {
        Debug.Log("press");

    } 

    public void LoadUI()
    {
        LoadAndChooseFileUI.SetActive(true);
    }

    public void Load0()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "/DataXml.text");

        XmlNodeList sceneName = xmlDocument.GetElementsByTagName("SceneName");
        SceneManager.LoadScene(int.Parse(sceneName[0].InnerText));

        GameManager.instance.LoadGame(0);
    }

    public void Load1()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(Application.dataPath + "/DataXmlSecond.text");

        XmlNodeList sceneName = xmlDocument.GetElementsByTagName("SceneName");
        SceneManager.LoadScene(int.Parse(sceneName[0].InnerText));

        GameManager.instance.LoadGame(1);
    }

    public void AboutUs()
    {
        Debug.Log("pre");
    }

    public void Help()
    {
        Debug.Log("pre");
    }

    public void Setting()
    {
        Debug.Log("pre");
    }
}
