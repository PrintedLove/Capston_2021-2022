using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class BackEndManager : MonoBehaviour // �̱������� ����� �ı����� �ʴ� ���ӿ�����Ʈ�� ������ �ʱ�ȭ �ڵ�, �������� �Լ�
{
    private static BackEndManager instance = null;
    public static BackEndManager MyInstance { get => instance; set => instance = value; }
    [SerializeField] private GameObject joinfail;
    [SerializeField] private Text join1;
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

        var bro = Backend.Initialize(true);
        Debug.Log("�ڳ� �ʱ�ȭ ����" + bro);
        if (bro.IsSuccess())
        {
            Debug.Log(Backend.Utils.GetServerTime());
        }

            // ����
        else
        {
           ShowErrorUI(bro);
        }
        

    }

    public void ShowErrorUI(BackendReturnObject backendReturn)
    {
        int statusCode = int.Parse(backendReturn.GetStatusCode());

        switch (statusCode)
        {
            case 401:
                Debug.Log("ID or Password Error");
                join1.text = "ID or Password Error";
                break;
            case 403:
                Debug.Log(backendReturn.GetErrorCode());
                join1.text = backendReturn.GetErrorCode();
                break;
            case 404:
                Debug.Log("game not found");
                join1.text = "game not found";
                break;
            case 408:
                // Ÿ�Ӿƿ� ����(�������� ������ �ʰų�, ��Ʈ��ũ ���� ���� �ִ� ���)
                // ��û ����
                Debug.Log(backendReturn.GetMessage());
                join1.text = backendReturn.GetMessage();
                break;

            case 409:
                Debug.Log("Duplicated customId, �ߺ��� customId �Դϴ�");
                join1.text = "Duplicated customId, �ߺ��� customId �Դϴ�";
                break;

            case 410:
                Debug.Log("bad refreshToken, �߸��� refreshToken �Դϴ�");
                join1.text = "bad refreshToken, �߸��� refreshToken �Դϴ�";
                break;

            case 429:
                // �����ͺ��̽� �Ҵ緮�� �ʰ��� ���
                // �����ͺ��̽� �Ҵ緮 ������Ʈ ���� ���
                Debug.Log(backendReturn.GetMessage());
                join1.text = backendReturn.GetMessage();
                break;

            case 503:
                // ������ ���������� �۵����� �ʴ� ���
                Debug.Log(backendReturn.GetMessage());
                join1.text = backendReturn.GetMessage();
                break;

            case 504:
                // Ÿ�Ӿƿ� ����(�������� ������ �ʰų�, ��Ʈ��ũ ���� ���� �ִ� ���)
                Debug.Log(backendReturn.GetMessage());
                join1.text = backendReturn.GetMessage();
                break;

        }

        joinfail.SetActive(true);
    }
    

    
}
