using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public static class Inventory {
    
    private static Dictionary<string, InventoryItem> items;
    private static SaveObject<InventoryItem> saveObject;
    
    private const string PATH = "plinv";
    private const string itemListPath = "ItemList";

    public static UnityEvent changeEvent;

    public static void Initialize() {
        items = new Dictionary<string, InventoryItem>();
        saveObject = new SaveObject<InventoryItem>();
        changeEvent = new UnityEvent();
    }

    public static void PickUp(string item, int amount) {
        if (items.TryGetValue(item, out _)) {
            items[item].AddAmount(amount);
        } else {
            items.Add(item, new InventoryItem(item, amount));
        }

        changeEvent.Invoke();
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static bool Discard(string item, int amount) {
        if (!HasValue(item, amount)) return false;
        InventoryItem inventoryItem = items[item];
        if (inventoryItem.amount < amount) return false;
        if (inventoryItem.RemoveAmount(amount)) {
            items.Remove(item);
            changeEvent.Invoke();
            return true;
        }

        return false;
    }

    public static bool HasValue(string item, int amount) {
        bool check = items.TryGetValue(item, out InventoryItem x);
        if (!check) return false;
        if (x.amount >= amount) return true;
        else return false;
    }

    /// <summary>
    /// Amount of given item in inventory.
    /// </summary>
    /// <param name="item">the name of the item to be checked</param>
    /// <returns> -1 if there are none in inventory, else the amount in the inventory</returns>
    public static int GetAmount(string item) {
        if (items.TryGetValue(item, out InventoryItem x)) {
            return x.amount;
        }

        return 0;
    }


    public static void Save() {
        saveObject.AddAll(items);
        SaveManager.SaveObject(Path.Combine(GameManager.GetPath(), PATH), saveObject);
    }

    public static void Load() {
        saveObject = SaveManager.LoadSaveObject<InventoryItem>(Path.Combine(GameManager.GetPath(), PATH));
        if (saveObject == null) {
            items = new Dictionary<string, InventoryItem>();
            saveObject = new SaveObject<InventoryItem>();
        } else {
            items = saveObject.GetDictionary();
        }
    }
}

[Serializable]
public class InventoryItem {
    public string name;
    public Sprite image;
    public bool hidden;
    public int amount;

    public InventoryItem() {
    }

    public InventoryItem(string name, Sprite image) {
        this.name = name;
        this.image = image;
        this.hidden = false;
    }

    public InventoryItem(string name, Sprite image, bool hidden) {
        this.hidden = hidden;
        this.image = image;
        this.name = name;
    }

    public InventoryItem(string name, Sprite image, bool hidden, int amount) {
        this.name = name;
        this.image = image;
        this.hidden = hidden;
        this.amount = amount;
    }

    public InventoryItem(string name, int amount) {
        this.name = name;
        this.amount = amount;
        this.hidden = true;
    }

    public bool AddAmount(int a) {
        amount += a;
        return true;
    }

    /// <summary>
    /// Removes the given amount of the item from the player's inventory.
    /// </summary>
    /// <param name="a"> the amount to return</param>
    /// <returns> true if there amount > 0, false if there is no amount left.</returns>
    public bool RemoveAmount(int a) {
        if (a >= amount) {
            amount = 0;
            return false;
        }

        amount -= a;
        return true;
    }
}