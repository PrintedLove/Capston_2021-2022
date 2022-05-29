using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndGameInfo : MonoBehaviour
{
    

    void Start()
    {
       
    }
    public void OnClickInsertData()
    {
        int charLevel = GameManager.Instance.getLevel();
        string charname = GameManager.Instance.getName();
        int charEXP = GameManager.Instance.getExp();
        int charMaxEXP = GameManager.Instance.getmaxExp();
        int charHP = GameManager.Instance.getHp();
        int charMaxHP = GameManager.Instance.getmaxHp();
        int charMP = GameManager.Instance.getMp();
        int charMaxmp = GameManager.Instance.getmaxMp();
        int charSTR = GameManager.Instance.getSTR();
        int charINT = GameManager.Instance.getINT();
        int charFIT = GameManager.Instance.getFIT();
        int charAPPoint = GameManager.Instance.getAPPoint();

        Param param = new Param();

        param.Add("Level",charLevel);
        param.Add("Name", charname);
        param.Add("EXP", charEXP);
        param.Add("MaxEXP", charMaxEXP);
        param.Add("HP", charHP);
        param.Add("MaxHP", charMaxHP);
        param.Add("MP", charMP);
        param.Add("MaxMP", charMaxmp);
        param.Add("STR",charSTR);
        param.Add("INT",charINT);
        param.Add("FIT",charFIT);
        param.Add("APPoint", charAPPoint);

        Dictionary<string, int> character = new Dictionary<string, int>
        {
            { "Level",charLevel },
            { "EXP", charEXP },
            { "MaxEXP", charMaxEXP },
            { "HP", charHP },
            { "MaxHP", charMaxHP },
            { "MP", charMP },
            { "MaxMP", charMaxmp },
            { "STR",charSTR },
            { "INT",charINT },
            { "FIT",charFIT },
            { "APPoint", charAPPoint }
        };

        param.Add("character", character);
        BackendReturnObject BRO = Backend.GameData.Insert("character",param);

        if (BRO.IsSuccess())
        {
            Debug.Log("indate:" + BRO.GetInDate());
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
