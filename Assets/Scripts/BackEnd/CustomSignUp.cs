using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class CustomSignUp : MonoBehaviour
{
    public InputField idInput;
    public InputField passInput;

    public void OnclickSignUp() //ȸ������ ��ư�� ���
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomSignUp(idInput.text, passInput.text, "test1");

        if(backendReturnObject.IsSuccess() == true)
        {
            Debug.Log("[������ ȸ������ ����.]");
        }
        else
        {
            BackEndSDK.MyInstance.ShowErrorUI(backendReturnObject);
        }
        Debug.Log("������");
    }

    public void OnclickLogin() // �α��� ��ư�� ���
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomLogin(idInput.text, passInput.text);

        if (backendReturnObject.IsSuccess()==true)
        {
            Debug.Log("�α��� �Ϸ�");
        }

        else
        {
            BackEndSDK.MyInstance.ShowErrorUI(backendReturnObject);
        }
    }

    BackendReturnObject bro = new BackendReturnObject();
    bool isSucces = false;

    void Update()
    {
        if(isSucces)
        {
            isSucces = false;
            bro.Clear();

        }
    }
}
