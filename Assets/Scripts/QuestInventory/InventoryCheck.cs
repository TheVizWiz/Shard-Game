using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryCheck {
    [SerializeField] private string itemName;
    [SerializeField] private WhenToCheck condition;
    [SerializeField] private int amount;


    public InventoryCheck(string itemName, WhenToCheck condition, int amount) {
        this.itemName = itemName;
        this.condition = condition;
        this.amount = amount;
    }

    public bool Check() {
        
        int amount = Inventory.GetAmount(itemName);
        switch (condition) {
            case WhenToCheck.Less when amount < this.amount:
            case WhenToCheck.LessOrEqual when amount <= this.amount:
            case WhenToCheck.Equal when amount == this.amount:
            case WhenToCheck.GreaterOrEqual when amount >= this.amount:
            case WhenToCheck.Greater when amount > this.amount:
                return true;
            default:
                return false;
        }
    }
}

public enum WhenToCheck {
    Less,
    LessOrEqual,
    Equal,
    GreaterOrEqual,
    Greater
}