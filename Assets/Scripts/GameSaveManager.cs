using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class GameSaveManager : MonoBehaviour
{
    //public Save save;
    public Player player;

    public GameObject pausePanel;

    // 
    public GameSaveManager(Player player) {
        this.player = player;
    }
    //private string saveDataPath = Application.persistentDataPath + "/game_SaveData";
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        loadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel")) {
            pauseGame();
        }
    }

    public void saveGame() {
        string save_path = Application.persistentDataPath + "/game_SaveData";

        Save save = new Save();

        save.playerPosX = player.transform.position.x;
        save.playerPosY = player.transform.position.y;
        save.playerRotation = player.transform.eulerAngles.y;
        Debug.Log("rotation.y="+save.playerRotation);
        save.currentHP = player.currentHP;

        Debug.Log(Application.persistentDataPath);

        if(!Directory.Exists(save_path)) {
            Directory.CreateDirectory(save_path);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(save_path + "/player.txt");

        var player_json = JsonUtility.ToJson(save);

        formatter.Serialize(file, player_json);

        file.Close();
       
    }

    public void loadGame() {
        Debug.Log("game loaded");
        Save save = new Save();

        BinaryFormatter formatter = new BinaryFormatter();

        if(File.Exists(Application.persistentDataPath + "/game_SaveData/player.txt")) {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/player.txt",FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(file), save);
            
            player.transform.position = new Vector3(save.playerPosX, save.playerPosY, player.transform.position.z);
            player.currentHP = save.currentHP;
            file.Close();
        }else{
            Debug.Log("No save data exists.");
        }
    }

    // 暂停游戏
    public void pauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 返回游戏
    public void resumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // 游戏过程中 存档
    public void saveGameInGame() {
        saveGame();
        resumeGame();
    }

    // 游戏过程中 读档
    public void loadGameInGame() {
        loadGame();
        resumeGame();
    }

    // 返回主界面
    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
