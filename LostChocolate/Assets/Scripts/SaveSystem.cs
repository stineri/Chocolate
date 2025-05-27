using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("Game autosaved.");
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        Debug.LogWarning("No save file found.");
        return null;
    }
}
