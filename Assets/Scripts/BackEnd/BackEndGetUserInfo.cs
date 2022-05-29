using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndGetUserInfo : MonoBehaviour
{
    public void OnClickGetUserInfor()
    {
        BackendReturnObject BRO = Backend.BMember.GetUserInfo();

        if (BRO.IsSuccess())
        {
            Debug.Log(BRO.GetReturnValue());
        }
        else
        {
            Debug.Log("���������߻�" + BRO.GetErrorCode());
        }
    }
}
