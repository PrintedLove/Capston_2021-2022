using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Text.RegularExpressions;


public class BackEndNickname : MonoBehaviour
{
    public InputField nickNameInput;

    //�ѱ�,����,���ڸ� �Է�
    private bool CheckNickName()
    {
        // �ѱ�, ����, ���� ���� �˻�
        return Regex.IsMatch(nickNameInput.text, "^[0-9a-zA-Z��-�R]*$");
    }

    public void OnclickCreateName()
    {
        // �ѱ�, ����, ���ڷθ� �г����� ��������� üũ
        if (CheckNickName() == false)
        {
            Debug.Log("�г����� �ѱ�, ����, ���ڸ� ����");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(nickNameInput.text);

        if (BRO.IsSuccess())
        {
            Debug.Log("�г��� ���� �Ϸ�");
        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("�̹� �ߺ��� �г����� �ִ� ���");
                    break;

                case "400":
                    if (BRO.GetMessage().Contains("too long")) Debug.Log("20�� �̻��� �г����� ���");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("�г��ӿ� ��/�� ������ �ִ°��");
                    break;

                default:
                    Debug.Log("���� ���� ���� �߻�: " + BRO.GetErrorCode());
                    break;
            }
        }
    
    }
}
