using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public PlayerMovement playerStats; // Attach your stamina/health script here
    private float saveTimer = 0f;
    private float saveInterval = 60f; // Save every 60 seconds

    void Start()
    {
        LoadGame();
    }

    void Update()
    {
        saveTimer += Time.deltaTime;
        if (saveTimer >= saveInterval)
        {
            AutoSave();
            saveTimer = 0f;
        }
    }

    void OnApplicationQuit()
    {
        AutoSave();
    }

    public void AutoSave()
    {
        SaveData data = new SaveData
        {
            playerX = player.position.x,
            playerY = player.position.y,
            stamina = playerStats.Stamina,
            currentScene = SceneManager.GetActiveScene().name
        };
        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            player.position = new Vector2(data.playerX, data.playerY);
            playerStats.Stamina = data.stamina;
            // You can also reload the scene if it's different
            // SceneManager.LoadScene(data.currentScene);
        }
    }
}
