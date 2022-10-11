using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private SceneChange MainMenuChanage;

    public void GameLogin()
    {
        Invoke("HideMainMenu", 1.2f);
        MainMenuChanage.CurrentSceneChange();
    }

    public void LoadGame()
    {
        // �ҷ����� ��ɱ��� �ؾ���.
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ����Ƽ ������ �� ����
#else
        Application.Quit(); // ����� ����
#endif
    }
    public void HideMainMenu()
    {
        MainMenuPanel.SetActive(false);
    }
}
