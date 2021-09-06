using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventoryChecker{
    // Start is called before the first frame update

    [SerializeField] private UnityEvent onFulfill;
    [SerializeField] private List<InventoryCheck> checks;


    public void Initialize () {
        Inventory.changeEvent.AddListener(CheckFulfillment);
    }

    private void CheckFulfillment() {
        foreach (InventoryCheck check in checks) {
            if (!check.Check()) return;
        }
        onFulfill.Invoke();
    }
}