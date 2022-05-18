using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class CustomSignUp : MonoBehaviour
{
    public InputField idInput;
    public InputField passInput;

    public void OnclickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(idInput.text, passInput.text, "test1");


        if (BRO.IsSuccess())
        {
            Debug.Log("ȸ�����ԿϷ�");
        }
        else
        {
            string error = BRO.GetStatusCode();
            switch (error)
            {
                case "409":
                    Debug.Log("�ߺ��� customId�� ����");
                    break;
                default:
                    Debug.Log("�������뿡��" + BRO.GetMessage());
                    break;
            }


        }
    }

    public void OnclickLogin()
    {
        BackendReturnObject BRO = Backend.BMember.CustomLogin(idInput.text, passInput.text);

        if (BRO.IsSuccess())
        {
            Debug.Log("�α��� �Ϸ�");
        }

        else
        {
            string error = BRO.GetStatusCode();

            switch (error)
            {
                case "401":
                    Debug.Log("���̵� �Ǵ� ��й�ȣ�� Ʋ�ȴ�.");
                    break;

                case "403":
                    Debug.Log("���ܵ� ����"+BRO.GetErrorCode());
                    break;
                default:
                    Debug.Log("�������뿡��" + BRO.GetMessage());
                    break;
            }
        }
    }
}
