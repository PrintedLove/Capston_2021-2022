using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class CustomSignUp : MonoBehaviour
{
    public InputField idInput;
    public InputField passInput;

    public void OnclickSignUp() //회원가입 버튼에 사용
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomSignUp(idInput.text, passInput.text, "test1");

        if(backendReturnObject.IsSuccess() == true)
        {
            Debug.Log("[동기방식 회원가입 성공.]");
        }
        else
        {
            BackEndSDK.MyInstance.ShowErrorUI(backendReturnObject);
        }
        Debug.Log("동기방식");
    }

    public void OnclickLogin() // 로그인 버튼에 사용
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomLogin(idInput.text, passInput.text);

        if (backendReturnObject.IsSuccess()==true)
        {
            Debug.Log("로그인 완료");
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
