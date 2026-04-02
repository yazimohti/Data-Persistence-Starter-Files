using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public int bestScore;
    public string bestUser;
    public Text bestScoreText;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
        bestScoreText.text = bestUser + ":" + bestScore;
    }
    [System.Serializable]
    public class SaveData
    {
        public int bestScore;
        public string bestUserName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestUserName = bestUser;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
            bestUser = data.bestUserName;
        }
    }

    public void ResetScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) File.Delete(path);
        bestScore = 0;
        bestUser = "Best";
    }
}
