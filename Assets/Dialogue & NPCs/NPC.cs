using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class NPC {
    public string name;
    public Dictionary<string, string> displayNames;
    public Dictionary<string, string> descriptions;
    public Dictionary<string, string> autoDeclineMessages;
    public List<Dialogue> dialogues;


    public NPC() { 
        dialogues = new List<Dialogue>();
        displayNames = new Dictionary<string, string>();
        descriptions = new Dictionary<string, string>();
        autoDeclineMessages = new Dictionary<string, string>();
    }

    public Dialogue ActivateDialogue() {
        foreach (Dialogue dialogue in dialogues) {
            if (dialogue.CheckReqs()) return dialogue;
        }

        return null;
    }

    public static NPC LoadNPCFromJSON(string filePath) {
        NPC npc = JsonConvert.DeserializeObject<NPC>(Resources.Load<TextAsset>(filePath).text);
        foreach (Dialogue dialogue in npc.dialogues) {
            dialogue.InitializeStaticLines();
        }

        return npc;
    }
    
    public override string ToString() {
        return displayNames[GameManager.language] + ", " + descriptions + ", " + autoDeclineMessages;
    }
}