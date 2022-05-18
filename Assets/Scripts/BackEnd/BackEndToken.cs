using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndToken : MonoBehaviour
{
    public void OnClickRefreshToken()
    {
        //��ū�� ��߱� �޴� �ڵ�
        Backend.BMember.RefreshTheBackendToken();
    }

    public bool OnClickIsTokenAlive()
    {
        //��ȿ�� ��ū�̸� true, �ƴϸ� false ����
        return Backend.BMember.IsAccessTokenAlive().GetMessage() == "Success"?true:false;
    }
}
