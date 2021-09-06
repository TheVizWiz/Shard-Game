using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class NPCManager {
    public static Dictionary<string, NPC> npcs;
    private static string filePath = "NPCs";

    public static void Initialize() {
        string[] npcList = SaveManager.ReadFileFromResources(Path.Combine(filePath, "master"));
        npcs = new Dictionary<string, NPC>();
        foreach (string s in npcList) {
            NPC npc = NPC.LoadNPCFromJSON(Path.Combine(filePath, s));
            npcs.Add(npc.name, npc);
        }
    }
}