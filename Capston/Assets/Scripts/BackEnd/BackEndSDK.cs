using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndSDK : MonoBehaviour
{
    void Start()
    {   
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����!");
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }
    void Update()
    {
        Backend.AsyncPoll();
    }

    public void CustomSignUp()
    {
        string id = "test1";
        string password = "1234";

        var bro = Backend.BMember.CustomSignUp(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("���Լ���");
        }
        else
        {
            Debug.LogError("����!");
            Debug.Log(bro);
        }
    }
}
