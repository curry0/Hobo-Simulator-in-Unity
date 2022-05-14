using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    //only for test purposes
    public Item()
    {
        itemType = ItemType.can;
        value = .15f;
        weight = .01f;
    }

    public enum ItemType
    {
        can,
        glassBottle,
        plasticBottle
    }

    public ItemType itemType;
    public float value;
    public float weight;
}
