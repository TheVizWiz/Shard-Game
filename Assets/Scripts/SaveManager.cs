using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using Object = System.Object;

public static class SaveManager {

    private static BinaryFormatter formatter;

    public static void Initialize() {
        formatter = new BinaryFormatter();
    }

    /// <summary>
    /// Saves a SaveObject at the given path.
    /// </summary>
    /// <param name="path">path to save the object to, will override files if they already exist</param>
    /// <param name="o">the SaveObject to serialize</param>
    /// <returns>true if the process was completed successfully, false otherwise</returns>
    public static bool SaveSaveObject<T>(string path, SaveObject<T> o) {
        string parent = Path.GetDirectoryName(Path.Combine(Application.persistentDataPath, path));
        if (!Directory.Exists(parent)) Directory.CreateDirectory(parent ?? string.Empty);
        Debug.Log(Path.Combine(Application.persistentDataPath, path));
        FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, path), FileMode.OpenOrCreate);
        formatter.Serialize(stream, o);
        stream.Close();
        return true;
    }

    /// <summary>
    /// deserializes and returns a SaveObject based on given path
    /// </summary>
    /// <param name="path"> path of the item to be gotten</param>
    /// <returns>null if no file is found, otherwise SaveObject with dictionary from file</returns>
    [CanBeNull]
    public static SaveObject<T> LoadSaveObject<T>(string path) {
        FileStream stream;
        try {
            stream = new FileStream(Path.Combine(Application.persistentDataPath, path), FileMode.Open);
        } catch (FileNotFoundException) {
            return null;
        } 

        if (!stream.CanRead) return null;

        SaveObject<T> saveObject = formatter.Deserialize(stream) as SaveObject<T>;
        stream.Close();
        return saveObject;
    }


    public static T LoadObject<T>(string path) where T : class {
        FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, path), FileMode.Open);
        if (!stream.CanRead) return default;

        T t = formatter.Deserialize(stream) as T;
        stream.Close();
        return t;
    }

    public static bool SaveObject<T>(string path, T t) where T : class {
        FileStream stream = new FileStream(Path.Combine(Application.persistentDataPath, path), FileMode.OpenOrCreate);
        formatter.Serialize(stream, t);
        stream.Close();
        return true;
    }

    public static string[] ReadFileFromResources(string path) {
        char[] limiters = {'\r', '\n'};
        return (Resources.Load(path) as TextAsset)?.text.Split(limiters, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string GetSaveString(string filepath) {
        return Path.Combine("sv" + GameManager.saveNumber, filepath);
    }
}

[Serializable]
public class SaveObject<T> {
    private Dictionary<string, T> dictionary;

    public SaveObject() {
        dictionary = new Dictionary<string, T>();
    }

    public SaveObject(IDictionary<string, T> d) {
        dictionary = new Dictionary<string, T>();
        AddAll(d);

    }
    

    public void Add(string s, T o) {
        dictionary.Add(s, o);
    }

    public T Get(string s) {
        return dictionary[s];
    }

    public void AddAll(IDictionary<string, T> d) {
        foreach (KeyValuePair<string, T> o in d) {
            Add(o.Key, o.Value);
        }
    }
    
    
    public Dictionary<string, T> GetDictionary() {
        return dictionary;
    }

    public void Clear() {
        dictionary = new Dictionary<string, T>();
    }
    
}
