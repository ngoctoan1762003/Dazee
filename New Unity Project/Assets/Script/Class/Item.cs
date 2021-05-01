using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item", fileName ="NewItem")]
public class Item : ScriptableObject
{
    public string ItemName;
    public string ItemDescribe;
    public string ItemPrice;
    public Sprite ItemImage;
}
