using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;


public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private void Awake(){ instance = this; }

    // �������� ���� ����
    public int startStage = 0;

    public int clearStage = 0;

    public int endStage = 8;

    public string getIndate;


    public void OnClickInsertData()
    {
        int clearstage = clearStage;

        Param param = new Param();

        param.Add("ClearStage", clearstage);

        Dictionary<string, int> stage = new Dictionary<string, int>
        {
            {"ClearStage",clearstage }
        };

        param.Add("stage", stage);
        BackendReturnObject BRO = Backend.GameData.Insert("stage", param);

        if (BRO.IsSuccess())
        {
            Debug.Log("indate:" + BRO.GetInDate());
            getIndate = BRO.GetInDate();

        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "404":
                    Debug.Log("�������� �ʴ� tableName�� ���");
                    break;

                case "412":
                    Debug.Log("��Ȱ��ȭ �� tableName�� ���");
                    break;

                case "413":
                    Debug.Log("�ϳ��� row( column���� ���� )�� 400KB�� �Ѵ� ���");
                    break;

                default:
                    Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
                    break;
            }
        }
    }


}
