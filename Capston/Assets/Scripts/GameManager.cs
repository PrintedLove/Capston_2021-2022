using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GameState//���� �⺻ ����
{
    menu,
    inGame,
    gameover
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public bool isPause = false;
    public GameState currentGameState = GameState.menu; // ���� ���۽� ���� �� ����.
    private static GameManager _instance;
    private GameObject player;
    private SpriteRenderer playerRenderer;

    private int activelevel = 0; // ��������
    private int beforelevel;
    private string myname; // ĳ���� �̸�
    private int maxHp = 0; // �ִ� ü��
    private int maxMp = 0; // �ִ� ����
    private int maxExp = 0; // �ִ� ����ġ
    private int HP = 0; // ���� ü��
    private int MP = 0; // ���� ����
    private int STR = 0;  // ��( ���ݷ� ü������)
    private int INT = 0; //  ����( �ֹ��� ���� ����)
    private int FIT = 0; // ü��( �̼� �� ü�� ���� ȸ���� ����)
    private int EXP = 0;  //����ġ
    private int APPoint = 0;

    void start()
    {
        StartGame();
        FIT = 2;
    }



    public void StartGame()//���� ���� �Լ�
    {
        SetGameState(GameState.inGame);
    }
    public void GameOver()//���� ���� �Լ�
    {

    }
    public void BackToMenu() // �޴� �Լ�
    {

    }
    void Start()
    {
        //currentGameState = GameState.menu;// ���۽� ���ӻ��� ����
        StartGame();
    }

    void SetGameState(GameState newGameState)// ���� ����
    {
        if (newGameState == GameState.menu) // �����ӽ�
        {
            
        }
        else if (newGameState == GameState.inGame) // ���� �����
        {
            activelevel = 1; // ���� ����
            myname = "Charater"; // �̸� ����
            maxHp += 50; // ü�¼���
            maxMp += 200; // ���� ����
            maxExp += 300; // �ִ����ġ ����(�������� ����)
            HP += maxHp; // ���۽� �ִ�ü������ ����
            MP += maxMp; // ���۽� �ִ븶���� ����
            STR += 2; // �� ����
            INT += 10; // ���� ����
            FIT += 2; // ü�� ����
            EXP += 0; // ����ġ ����
        }
        else if (newGameState == GameState.gameover) // ���� �����
        {

        }
    }

    public int getLevel() // �����ҷ�����
    {
        return activelevel;
    }

    public void setLevel() // �������� ���
    {
        if (EXP >= maxExp) //���� ����ġ�� �ִ� ���� ������(������) ���� +1, ���� ����ġ 0���� �ʱ�ȭ
        {
            activelevel += 1;
            EXP = 0;
            APPoint += 3; //������ �� �����̱� ������ ���� ����Ʈ 3���� ����.
        }
    }

    public void UpSTR()
    {
        if (APPoint <= 0)
        {
            return;
        }
        else
        {
            STR += 1;
            APPoint -= 1;
        }
    }
    public void UpINT()
    {
        if (APPoint <= 0)
        {
            return;
        }
        else
        {
            INT += 1;
            APPoint -= 1;
        }
    }
    public void UpFIT()
    {
        if (APPoint <= 0)
        {
            return;
        }
        else
        {
            FIT += 1;
            APPoint -= 1;
        }
    }

    public string getName() // �̸� �ҷ�����
    {
        return myname;
    }

    public int getExp() // ���� ����ġ �ҷ�����
    {
        return EXP;
    }

    public int getmaxExp() // �ִ� ����ġ �ҷ�����
    {
        return maxExp;
    }

    public int getexp(int newExp) // ���� ����ġ �ҷ�����
    {
        EXP += newExp;
        return EXP;
    }
    public int getHp() //HP�ҷ�����
    {
        return HP;
    }
    public int getmaxHp() //MAX HP�ҷ�����
    {
        return maxHp;
    }
    public int getMp() //MP �ҷ�����
    {
        return MP;
    }
    public int getmaxMp() //MAX MP �ҷ�����
    {
        return maxMp;
    }
    public int getSTR() // STR�� �ҷ�����
    {
        return STR;
    }
    public int getINT() // INT�� �ҷ�����
    {
        return INT;
    }
    public int getFIT() // FIT�� �ҷ�����
    {
        return FIT;
    }
    public int setSTR(int newSTR) // STR ������ �����ϱ�
    {
        STR += newSTR;
        return STR;
    }
    public int setINT(int newINT) // INT ������ �����ϱ�
    {
        INT += newINT;
        return INT;
    }
    public int setFIT(int newFIT) // FIT ������ �����ϱ�
    {
        FIT += newFIT;
        return FIT;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);

        player = GameObject.FindWithTag("Player");
        playerRenderer = player.GetComponent<SpriteRenderer>();

        Screen.fullScreen = true;
    }

    //private void Update()
    //{
    //    //��üȭ�� ���
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //        Screen.fullScreen = !Screen.fullScreen;
    //    if (Input.GetButtonDown("z"))// zŰ �Է½� ���� ����
    //    {
    //        StartGame();
    //    }
       
    //}
}

public class GameContorl
{
    public void IsPause()
    {
        GameManager.Instance.isPause = true;
    }
}
