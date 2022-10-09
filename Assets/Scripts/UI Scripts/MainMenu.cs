using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private Fade mainFade;

    public void GameLogin()
    {

        mainFade.FadeOutIn();
        Invoke("HideMainMenu", 1.2f);
        Invoke("LoginSceneChange", 1.2f);
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

    public void LoginSceneChange()
    {
        SceneManager.LoadScene("Village Scene");
    }


}
