using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndSDK : MonoBehaviour // �̱������� ����� �ı����� �ʴ� ���ӿ�����Ʈ�� ������ �ʱ�ȭ �ڵ�, �������� �Լ�
{
    private static BackEndSDK instance = null;
    public static BackEndSDK MyInstance { get => instance; set => instance = value; }
    void Awake()
    {   
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        InitBackEnd();
    }

    // �ڳ� �ʱ�ȭ
    
    private void InitBackEnd()
    {
        Backend.Initialize(BRO =>
        {
            Debug.Log("�ڳ� �ʱ�ȭ ���� " + BRO);

            // ����
            if (BRO.IsSuccess())
            {
                Debug.Log(Backend.Utils.GetServerTime());
            }

            // ����
            else
            {
                ShowErrorUI(BRO);
            }
        });
    }

    public void ShowErrorUI(BackendReturnObject backendReturn)
    {
        int statusCode = int.Parse(backendReturn.GetStatusCode());

        switch (statusCode)
        {
            case 401:
                Debug.Log("ID or Password Error");
                break;
            case 403:
                Debug.Log(backendReturn.GetErrorCode());
                break;
            case 404:
                Debug.Log("game not found");
                break;
            case 408:
                // Ÿ�Ӿƿ� ����(�������� ������ �ʰų�, ��Ʈ��ũ ���� ���� �ִ� ���)
                // ��û ����
                Debug.Log(backendReturn.GetMessage());
                break;

            case 409:
                Debug.Log("Duplicated customId, �ߺ��� customId �Դϴ�");
                break;

            case 410:
                Debug.Log("bad refreshToken, �߸��� refreshToken �Դϴ�");
                break;

            case 429:
                // �����ͺ��̽� �Ҵ緮�� �ʰ��� ���
                // �����ͺ��̽� �Ҵ緮 ������Ʈ ���� ���
                Debug.Log(backendReturn.GetMessage());
                break;

            case 503:
                // ������ ���������� �۵����� �ʴ� ���
                Debug.Log(backendReturn.GetMessage());
                break;

            case 504:
                // Ÿ�Ӿƿ� ����(�������� ������ �ʰų�, ��Ʈ��ũ ���� ���� �ִ� ���)
                Debug.Log(backendReturn.GetMessage());
                break;
        }
    }
    

    
}
