using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using System;

public class InventoryManager : MonoBehaviour
{
    public InventoryManager instance;

    private void Awake()
    {
        { instance = this; }
    }

    public int HpSmallPotion;
    public int HpBigPotion;
    public int MpSmallPotion;
    public int MpBigPotion;
    public int Item;


    public void OnclickInsertInventoryData()
    {
        //int charHpSmall = ���⿡ Inventory���� �Ѱ��ִ� ��; 
        //int charHpBig = ���⿡ Inventory���� �Ѱ��ִ� ��; 
        //int charMpSmall = ���⿡ Inventory���� �Ѱ��ִ� ��; 
        //int charMpBig = ���⿡ Inventory���� �Ѱ��ִ� ��; 
        //int charItem = ���⿡ Inventory���� �Ѱ��ִ� ��; 

        Param param = new Param();
        //param.Add("HPSmallPotion", charHpSmall);
        //param.Add("HPBigPotion", charHpBig);
        //param.Add("MPSmallPotion", charMpSmall);
        //param.Add("MPBigPotion", charMpBig);
    }


}
