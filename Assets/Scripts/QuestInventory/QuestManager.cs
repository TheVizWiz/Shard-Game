using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public static class QuestManager {
    private const string folder = "Quests";
    private const string savePath = "quests";
    public static Dictionary<string, Quest> quests;
    
    
    public static void Initialize() {
        string[] questPaths = SaveManager.ReadFileFromResources(Path.Combine(folder, "master"));
        quests = new Dictionary<string, Quest>();

        foreach (string path in questPaths) {
            Quest quest = Quest.LoadQuestFromJSON(Path.Combine(folder, path));
            quest.Initialize();
            quests.Add(quest.name, quest);
        }

        Dictionary<string, string> currentStepList = SaveManager.LoadSaveObject<string>("quests")?.GetDictionary();

        foreach (KeyValuePair<string, Quest> quest in quests) {
            if (currentStepList != null && currentStepList.TryGetValue(quest.Key, out string currentStep)) {
                if (currentStep == "null") {
                    quests.Remove(quest.Key);
                } else
                    quest.Value.SetCurrentStep(currentStepList[currentStep]);
            } else {
                quest.Value.currentStep = quest.Value.startStep;
            }
        }

        Inventory.changeEvent.AddListener(UpdateQuests);
    }


    private static void UpdateQuests() {
        List<string> removeKeys = new List<string>();
        foreach (KeyValuePair<string, Quest> pair in quests) {
            pair.Value.updateQuest();
            if (pair.Value.currentStep == null) {
                removeKeys.Add(pair.Key);
                Debug.Log("finished Quest");
            }
        }

        foreach (string s in removeKeys) {
            quests.Remove(s);
        }
    }

    public static void SaveQuestProgress() { 
        SaveObject<string> saveObject = new SaveObject<string>();
        foreach (KeyValuePair<string, Quest> quest in quests) {
            if (quest.Value.currentStep == null) {
                saveObject.Add(quest.Key, "null");
            } else {
                saveObject.Add(quest.Key, quest.Value.currentStep.GetName());
            }
        }

        SaveManager.SaveSaveObject(SaveManager.GetSaveString(savePath), saveObject);
    }
}