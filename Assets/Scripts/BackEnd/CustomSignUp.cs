using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class CustomSignUp : MonoBehaviour
{
    public InputField idInput;
    public InputField passInput;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private Fade fd;
    [SerializeField] private GameObject joinPopup;
    [SerializeField] private GameObject joinFail;
    

    public void OnclickSignUp() //ȸ������ ��ư�� ���
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomSignUp(idInput.text, passInput.text, "test1");
       

        if(backendReturnObject.IsSuccess() == true)
        {
            Debug.Log("[������ ȸ������ ����.]");
            joinPopup.SetActive(true);
        }
        else
        {
            BackEndManager.MyInstance.ShowErrorUI(backendReturnObject);
            joinFail.SetActive(true);
        }
        Debug.Log("������");
    }

    public void OnclickLogin() // �α��� ��ư�� ���
    {
        BackendReturnObject backendReturnObject = Backend.BMember.CustomLogin(idInput.text, passInput.text);

        if (backendReturnObject.IsSuccess()==true)
        {
            Debug.Log("�α��� �Ϸ�");
            GameManager.Instance.Ingame();
            Invoke("HideMainMenu", 1.2f);
            fd.FadeOutIn();
        }

        else
        {
            BackEndManager.MyInstance.ShowErrorUI(backendReturnObject);
            joinFail.SetActive(true);
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

    public void HideMainMenu()
    {
        MainMenuPanel.SetActive(false);
    }
}
