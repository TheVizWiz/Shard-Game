using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Debug = UnityEngine.Debug;

[Serializable]
public class Quest {
    public string name;
    public string startStepString; 
    public QuestStep currentStep, startStep;
    public List<QuestStep> stepList;
    public Dictionary<string, string> displayNames;
    public Dictionary<string, string> descriptions;
    private Dictionary<string, QuestStep> steps;

    static Quest() {
    }

    public Quest(QuestStep step) {
        startStep = step;
        currentStep = startStep;
        steps = new Dictionary<string, QuestStep>();
    }

    public Quest(string name) {
        this.name = name;
        steps = new Dictionary<string, QuestStep>();
    }

    /// <summary>
    /// checks if quest is finished or not
    /// </summary>
    /// <returns>false if quest is not done, true if quest is done</returns>
    public bool updateQuest() {
        string s = currentStep.CheckTransition();
        if (s == "") {
            currentStep = null;
            return true;
        }

        if (s == null) return false;


        currentStep = steps[s];
        return false;
    }

    public static Quest LoadQuestFromJSON(string filePath) {
        // Debug.Log(filePath);
        Quest quest = JsonUtility.FromJson<Quest>(Resources.Load<TextAsset>(filePath).text);
        quest.Initialize();
        return quest;
    }

    public void EndQuest() {
        Debug.Log("Quest has been ended");
    }

    public void Initialize() {
        steps = new Dictionary<string, QuestStep>();
        foreach (QuestStep step in stepList) {
            steps.Add(step.name, step);
        }

        startStep = steps[startStepString];
        currentStep = startStep;
    }

    public void SetCurrentStep(string s) {
        currentStep = steps[s];
    }

    public string GetDisplayName() {
        return displayNames[GameManager.language];
    }

    public string GetName() {
        return name;
    }

    public string GetDescription() {
        return descriptions[GameManager.language];
    }
}

[Serializable]
public class QuestStep {
    public string name;
    public Dictionary<string, string> displayNames;
    public Dictionary<string, string> descriptions;
    public List<QuestTransition> transitions;

    public QuestStep() {
        transitions = new List<QuestTransition>();
        displayNames = new Dictionary<string, string>();
        descriptions = new Dictionary<string, string>();
    }

    public string CheckTransition() {
        foreach (QuestTransition transition in transitions) {
            if (transition.CheckReqs()) return transition.endQuest;
        }

        return null;
    }

    public string GetDisplayName() {
        return displayNames[GameManager.language];
    }

    public string GetName() {
        return name;
    }

    public string GetDescription() {
        return displayNames[GameManager.language];
    }

    public override string ToString() {
        string s = displayNames[GameManager.language] + "\t" + descriptions[GameManager.language];
        if (transitions.Count == 0) return s;
        foreach (QuestTransition transition in transitions) {
            s += "\n\t" + transition.ToString();
        }

        return s;
    }
}

[Serializable]
public class QuestTransition {
    public string endQuest;
    public List<InventoryItem> requirements;
    public List<InventoryItem> removeItems;
    public List<InventoryItem> addItems;


    public QuestTransition() {
        // requirements = new Dictionary<string, int>();
        // removeItems = new Dictionary<string, int>();
        // addItems = new Dictionary<string, int>();
    }

    public bool CheckReqs() {
        foreach (InventoryItem req in requirements) {
            if (!Inventory.HasValue(req.name, req.amount)) {
                return false;
            }
        }

        DoTransition();
        return true;
    }

    public void DoTransition() {
        foreach (InventoryItem item in removeItems) {
            if (item.amount > 0) Inventory.Discard(item.name, item.amount);
            // else Inventory.Discard(pair.Key);
        }

        foreach (InventoryItem item in addItems) {
            Inventory.PickUp(item.name, item.amount);
        }
    }

    public override string ToString() {
        string s = "";
        foreach (InventoryItem keyValuePair in requirements) {
            s += keyValuePair.amount + "\t" + keyValuePair.name + "\n";
        }

        return s + ((endQuest == null) ? "" : endQuest.ToString());
    }
}