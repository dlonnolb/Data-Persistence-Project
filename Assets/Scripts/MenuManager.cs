using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string actualName;
    public int actualScore;
    public string bestName;
    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDatos();
    }

    [System.Serializable]

    class SaveData
    {
        public int bestScore;
        public string bestName;
    }

    public void SaveDatos()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestName = bestName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/Data-Persistence-Project.json", json);
    }
    public void LoadDatos()
    {
        string path = Application.persistentDataPath + "/Data-Persistence-Project.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.bestName;
            bestScore = data.bestScore;
        }
    }
}
