using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class Dialogue {
    public Dictionary<string, List<string>> lines;
    public Dictionary<string, List<string>> staticLines;
    public List<DialogueOption> options;
    public List<InventoryItem> requirements;

    private string currentLine;

    public Dialogue() {
        lines = new Dictionary<string, List<string>>();
        staticLines = new Dictionary<string, List<string>>();
        options = new List<DialogueOption>();
        requirements = new List<InventoryItem>();
    }

    public string GetNextLine() {
        string s = lines[GameManager.language][0];
        foreach (List<string> list in lines.Values) {
            if (list.Count > 0)
                list.RemoveAt(0);
        }

        currentLine = s;
        return s;
    }

    public string GetCurrentLine() {
        return currentLine;
    }

    public bool HasNextLine() {
        return lines[GameManager.language].Count > 0;
    }

    public bool IsLastLine() {
        return lines[GameManager.language].Count == 0;
    }

    public void ClearLines() {
        lines.Clear();
    }

    public void ResetLines() {
        foreach (List<string> list in lines.Values) {
            list.Clear();
        }

        foreach (KeyValuePair<string, List<string>> pair in staticLines) {
            foreach (string s in pair.Value) {
                lines[pair.Key].Add(s);
            }
        }
    }

    public void InitializeStaticLines() {
        foreach (KeyValuePair<string, List<string>> pair in lines) {
            foreach (string s in pair.Value) {
                if (!staticLines.ContainsKey(pair.Key))
                    staticLines[pair.Key] = new List<string>();
                staticLines[pair.Key].Add(s);
            }
        }
    }


    public bool EndDialogue(string optionPicked) {
        foreach (DialogueOption option in options) {
            if (option.displayStrings[GameManager.language] == optionPicked) {
                List<string> strings = option.optionStrings[GameManager.language];
                foreach (string s in strings) {
                    lines[GameManager.language].Add(s);
                }

                return option.Finish();
            }
        }

        return false;
    }


    public bool CheckReqs() {
        foreach (InventoryItem item in requirements) {
            if (!Inventory.HasValue(item.name, item.amount)) return false;
        }

        return true;
    }
}

[Serializable]
public class DialogueOption {
    public Dictionary<string, List<string>> optionStrings;
    public Dictionary<string, string> displayStrings;
    public List<InventoryItem> additionItems;
    public List<InventoryItem> removeItems;

    public DialogueOption() {
        additionItems = new List<InventoryItem>();
        removeItems = new List<InventoryItem>();
        optionStrings = new Dictionary<string, List<string>>();
    }

    public bool Finish() {
        foreach (InventoryItem item in removeItems) {
            if (!Inventory.HasValue(item.name, item.amount)) return false;
        }

        foreach (InventoryItem item in removeItems) {
            Inventory.Discard(item.name, item.amount);
        }

        foreach (InventoryItem item in additionItems) {
            Inventory.PickUp(item.name, item.amount);
        }

        return true;
    }

    public List<string> GetEndStrings() {
        return optionStrings[GameManager.language];
    }
}