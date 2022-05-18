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

    
}
