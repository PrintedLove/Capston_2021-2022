using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    

    public void NewGame()
    {
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

}
