using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
    public float weight;
    public float value;
    public Dictionary<Item.ItemType, int> itemCount;
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
        itemCount = new Dictionary<Item.ItemType, int>();
        weight = 0f;
        value = 0f;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        weight += item.weight;
        value += item.value;
        if (!itemCount.ContainsKey(item.itemType)) itemCount.Add(item.itemType, 0);
        itemCount[item.itemType]++;
    }

    public int GetCount(Item.ItemType itemType)
    {
        if (!itemCount.ContainsKey(itemType)) return 0;
        return itemCount[itemType];
    }

    public void Empty()
    {
        itemCount = new Dictionary<Item.ItemType, int>();
    }
}
