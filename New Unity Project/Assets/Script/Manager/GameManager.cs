using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public bool isPaused = false;

    public List<Item> items = new List<Item>();
    public List<int> itemsNumber = new List<int>();

    
    //collect soul fragment-------
    public float maxHp, maxMana, maxStamina, maxDefense, maxDamage;
    public float playerPosX, playerPosY;

    public bool isLoaded = true;// check if player choose play or load and whether it's loaded or not

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        //LoadGame(0);
    }

    //inventory------------------------------------------------------
    public void AddItem(Item _item)
    {
        if (!items.Contains(_item))
        {
            items.Add(_item);
            itemsNumber.Add(1);
        }
        else
        {
            for(int i=0; i<items.Count; i++)
            {
                if (items[i] == _item)
                {
                    itemsNumber[i]++;
                }
            }
        }
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().DisplayItem();
    }

    public void RemoveItem(Item _item)
    {
        for(int i=0; i<items.Count; i++)
        {
            if (_item == items[i])
            {
                itemsNumber[i]--;
            }
            if (itemsNumber[i] == 0)
            {
                items.Remove(items[i]);
                itemsNumber.Remove(itemsNumber[i]);
            }
        }
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().DisplayItem();
    }

    //save------------------
    public Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.maxDam = GameManager.instance.maxDamage;
        save.maxDef = GameManager.instance.maxDefense;
        save.maxHp = GameManager.instance.maxHp;
        save.maxMana = GameManager.instance.maxMana;
        save.maxSta = GameManager.instance.maxStamina;
        save.playerPosX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        save.playerPosY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
        return save;
    }

    //XML------------------
    public void SaveGame(int FileIndex)
    {
        Save save = CreateSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement sceneName = xmlDocument.CreateElement("SceneName");
        sceneName.InnerText = SceneManager.GetActiveScene().buildIndex.ToString();
        root.AppendChild(sceneName);

        XmlElement maxDamElement = xmlDocument.CreateElement("maxDamElement");
        maxDamElement.InnerText = save.maxDam.ToString();
        root.AppendChild(maxDamElement);

        XmlElement maxDefElement = xmlDocument.CreateElement("maxDefElement");
        maxDefElement.InnerText = save.maxDef.ToString();
        root.AppendChild(maxDefElement);

        XmlElement maxManaElement = xmlDocument.CreateElement("maxManaElement");
        maxManaElement.InnerText = save.maxMana.ToString();
        root.AppendChild(maxManaElement);

        XmlElement maxStaElement = xmlDocument.CreateElement("maxStaElement");
        maxStaElement.InnerText = save.maxSta.ToString();
        root.AppendChild(maxStaElement);

        XmlElement maxHPElement = xmlDocument.CreateElement("maxHPElement");
        maxHPElement.InnerText = save.maxHp.ToString();
        root.AppendChild(maxHPElement);

        XmlElement playerPosXElement = xmlDocument.CreateElement("PlayerPositionX");
        playerPosXElement.InnerText = save.playerPosX.ToString();
        root.AppendChild(playerPosXElement);

        XmlElement playerPosYElement = xmlDocument.CreateElement("PlayerPositionY");
        playerPosYElement.InnerText = save.playerPosY.ToString();
        root.AppendChild(playerPosYElement);

        xmlDocument.AppendChild(root);

        if (FileIndex == 0)
        {
            xmlDocument.Save(Application.dataPath + "/DataXML.text");
        }
        else if (FileIndex == 1)
        {
            xmlDocument.Save(Application.dataPath + "/DataXMLSecond.text");
        }
    }

    public void LoadGame(int FileIndex)
    {
        Save save = new Save();
        XmlDocument xmlDocument = new XmlDocument();

        //Load which file-----------
        if (FileIndex == 0)
        {
            Debug.Log("Loaded");
            xmlDocument.Load(Application.dataPath + "/DataXml.text");
            GameManager.instance.isLoaded = false;
        }
        else if (FileIndex == 1)
        {
            Debug.Log("Loaded");
            xmlDocument.Load(Application.dataPath + "/DataXmlSecond.text");
            GameManager.instance.isLoaded = false;
        }
        //------------------------

        XmlNodeList maxDamElement = xmlDocument.GetElementsByTagName("maxDamElement");
        save.maxDam = int.Parse(maxDamElement[0].InnerText);

        XmlNodeList maxDefElement = xmlDocument.GetElementsByTagName("maxDefElement");
        save.maxDef = int.Parse(maxDefElement[0].InnerText);

        XmlNodeList maxManaElement = xmlDocument.GetElementsByTagName("maxManaElement");
        save.maxMana = int.Parse(maxManaElement[0].InnerText);

        XmlNodeList maxStaElement = xmlDocument.GetElementsByTagName("maxStaElement");
        save.maxSta = int.Parse(maxStaElement[0].InnerText);

        XmlNodeList maxHPElement = xmlDocument.GetElementsByTagName("maxHPElement");
        save.maxHp = int.Parse(maxHPElement[0].InnerText);

        XmlNodeList playerposX = xmlDocument.GetElementsByTagName("PlayerPositionX");
        save.playerPosX = float.Parse(playerposX[0].InnerText);

        XmlNodeList playerposY = xmlDocument.GetElementsByTagName("PlayerPositionY");
        save.playerPosY = float.Parse(playerposY[0].InnerText);

        GameManager.instance.maxDamage = save.maxDam;
        GameManager.instance.maxDefense = save.maxDef;
        GameManager.instance.maxHp = save.maxHp;
        GameManager.instance.maxMana = save.maxMana;
        GameManager.instance.maxStamina = save.maxSta;
        GameManager.instance.playerPosX = save.playerPosX;
        GameManager.instance.playerPosY = save.playerPosY;
    }
}
