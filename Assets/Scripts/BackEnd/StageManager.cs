using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private void Awake(){ instance = this; }

    // �������� ���� ����
    public int startStage = 0;

    public int clearStage = 0;

    public int DBStage = 0;

    

    public string stagegetIndate;


    void Update()
    {
        
         GameManager.Instance.GetClearStage();
        
        
    }
    public void OnClickStageInsertData()
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
            stagegetIndate = BRO.GetInDate();

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

    public void OnClickGetPrivateContents()
    {
        BackendReturnObject BRO = Backend.GameData.GetMyData("stage", new Where());

        if (BRO.IsSuccess())
        {
            GetGameInfo(BRO.GetReturnValuetoJSON());
            Debug.Log("indate:" + BRO.GetInDate());
            stagegetIndate = BRO.GetInDate();


        }
        else
        {
            CheckError(BRO);
        }
    }

    void GetGameInfo(JsonData returnData)
    {

        if (returnData != null)
        {
            Debug.Log("�����Ͱ� �����մϴ�.");

            if (returnData.Keys.Contains("rows"))
            {
                Debug.Log("Pass");
                JsonData rows = returnData["rows"];
                for (int i = 0; i < rows.Count; i++)
                {
                    GetData(rows[i]);
                }
            }


            // row �� ���޹��� ���
            else if (returnData.Keys.Contains("rows"))
            {
                JsonData row = returnData["row"];
                Debug.Log("Check");
                GetData(row[0]);
            }
        }
        else
        {
            Debug.Log("�����Ͱ� �����ϴ�.");
        }


    }

    public void GetData(JsonData data)
    {

        DBStage = Int32.Parse(data["ClearStage"][0].ToString());
        Debug.Log(DBStage);
        SetDBStage(DBStage);
       
    }

    public void OnClickStageGameInfoUpdate()
    {
        Param param = new Param();
        int UpdateStage = GameManager.Instance.GetClearStage();
        

        param.Add("ClearStage", UpdateStage);

        Dictionary<string, int> stage = new Dictionary<string, int>
        {
            {"ClearStage",UpdateStage }
        };
        

        param.Add("stage", stage);
        Debug.Log(param);
        BackendReturnObject BRO1 = Backend.GameData.GetMyData("stage", new Where());
        BackendReturnObject BRO = Backend.GameData.Update("stage", BRO1.GetInDate(), param);

        if (BRO.IsSuccess())
        {
            Debug.Log("indate:" + BRO.GetInDate());
            Debug.Log("�����Ϸ�");
            

        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "405":
                    Debug.Log("param�� partition, gamer_id, inDate, updatedAt �װ��� �ʵ尡 �ִ� ���");
                    break;

                case "403":
                    Debug.Log("�ۺ����̺��� Ÿ�������� �����ϰ��� �Ͽ��� ���");
                    break;

                case "404":
                    Debug.Log("�������� �ʴ� tableName�� ���");
                    break;

                case "412":
                    Debug.Log("��Ȱ��ȭ �� tableName�� ���");
                    break;

                case "413":
                    Debug.Log("�ϳ��� row( column���� ���� )�� 400KB�� �Ѵ� ���");
                    break;
            }
        }

    }

    public int GetDBStage()
    {
        return DBStage;
    }
    public int SetDBStage(int stage)
    {
        DBStage = stage;
        return DBStage;
    }

    void CheckError(BackendReturnObject BRO)
    {
        switch (BRO.GetStatusCode())
        {
            case "200":
                Debug.Log("�ش� ������ �����Ͱ� ���̺� �����ϴ�.");
                break;

            case "404":
                if (BRO.GetMessage().Contains("gamer not found"))
                {
                    Debug.Log("gamerIndate�� �������� gamer�� indate�� ���");
                }
                else if (BRO.GetMessage().Contains("table not found"))
                {
                    Debug.Log("�������� �ʴ� ���̺�");

                }
                break;

            case "400":
                if (BRO.GetMessage().Contains("bad limit"))
                {
                    Debug.Log("limit ���� 100�̻��� ���");
                }

                else if (BRO.GetMessage().Contains("bad table"))
                {
                    // public Table ������ ��� �ڵ�� private Table �� �������� �� �Ǵ�
                    // private Table ������ ��� �ڵ�� public Table �� �������� �� 
                    Debug.Log("��û�� �ڵ�� ���̺��� �������ΰ� ���� �ʽ��ϴ�.");
                }
                break;

            case "412":
                Debug.Log("��Ȱ��ȭ�� ���̺��Դϴ�.");
                break;

            default:
                Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
                break;

        }
    }

}
