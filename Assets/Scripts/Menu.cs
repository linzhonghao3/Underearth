using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public GameObject soundSetting;

    public GameObject aboutPanel;

    public GameObject archivePanel;

    public GameObject archiveBtn;

    public GameObject noArchive;

    void Start() {
        soundSetting.SetActive(false);
        aboutPanel.SetActive(false);
        archivePanel.SetActive(false);
        noArchive.SetActive(false);
    }
    public void startgame() {
        // 删除旧存档
        deleteArchives();

        // 进入新手关
        SceneManager.LoadScene("UnderGround");
    }

    public void quit() {
        Application.Quit();
    }

    public void about() {
        aboutPanel.SetActive(true);
    }

    public void setting() {
        soundSetting.SetActive(true);
    }

    public void load() {
        archivePanel.SetActive(true);

        // 存档显示文字
        string strArchive = "";
        Text text = archiveBtn.transform.Find("Text").GetComponent<Text>();

        // 获取上次存档时间
        if(File.Exists(Application.persistentDataPath + "/game_SaveData/player.txt")) {
            archiveBtn.SetActive(true);
            FileInfo file = new FileInfo(Application.persistentDataPath + "/game_SaveData/player.txt");
            strArchive = "默认存档：" + file.LastWriteTime;
            noArchive.SetActive(false);
            Debug.Log("读取存档，上次存档时间：" + file.LastWriteTime);
        }else{
            archiveBtn.SetActive(false);
            noArchive.SetActive(true);
            Debug.Log("读取存档，暂无存档。");
        }

        text.text = strArchive;
    }

    public void setSound() {

    }

    public void closeSettingPanel() {
        soundSetting.SetActive(false);
    }

    public void closeAboutPanel() {
        aboutPanel.SetActive(false);
    }

    public void closeArchivePanel() {
        archivePanel.SetActive(false);
    }

    public void loadArchive() {
        SceneManager.LoadScene("KengDao");
    }

    // 删除旧存档
    private void deleteArchives() {
        if(File.Exists(Application.persistentDataPath + "/game_SaveData/player.txt")) {
            File.Delete(Application.persistentDataPath + "/game_SaveData/player.txt");
        }
    }
}
