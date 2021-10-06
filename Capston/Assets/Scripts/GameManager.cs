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

    private string activelevel; // ��������
    private string myname; // ĳ���� �̸�
    private int maxHp; // �ִ� ü��
    private int maxMp; // �ִ� ����
    private int maxExp; // �ִ� ����ġ
    private int HP; // ���� ü��
    private int MP; // ���� ����
    private int STR;  // ��( ���ݷ� ü������)
    private int INT; //  ����( �ֹ��� ���� ����)
    private int FIT; // ü��( �̼� �� ü�� ���� ȸ���� ����)
    private int EXP;  //����ġ

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
        currentGameState = GameState.menu;// ���۽� ���ӻ��� ����
    }

    void SetGameState(GameState newGameState)// ���� ����
    {
        if (newGameState == GameState.menu) // �����ӽ�
        {
            
        }
        else if (newGameState == GameState.inGame) // ���� �����
        {
            activelevel = "Level 1"; // ���� ����
            myname = "Charater"; // �̸� ����
            maxHp = 50; // ü�¼���
            maxMp = 200; // ���� ����
            maxExp = 300; // �ִ����ġ ����(�������� ����)
            HP = maxHp; // ���۽� �ִ�ü������ ����
            MP = maxMp; // ���۽� �ִ븶���� ����
            STR = 2; // �� ����
            INT = 10; // ���� ����
            FIT = 2; // ü�� ����
            EXP = 0; // ����ġ ����
        }
        else if (newGameState == GameState.gameover) // ���� �����
        {

        }
    }

    public string getLevel() // �����ҷ�����
    {
        return activelevel;
    }

    public void setLevel(string newlevel) // �������� ���
    {
        activelevel = newlevel;
    }

    public string getName() // �̸� �ҷ�����
    {
        return myname;
    }

    public int SetExp() // ���� ����ġ �ҷ�����
    {
        return EXP;
    }

    public int SetMaxExp() // �ִ� ����ġ �ҷ�����
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

    public int getSTR(int newSTR) // STR ������ �ҷ�����
    {
        STR += newSTR;
        return STR;
    }
    public int getINT(int newINT) // INT ������ �ҷ�����
    {
        INT += newINT;
        return INT;
    }
    public int getFIT() // FIT�� �ҷ�����
    {
        return FIT;
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
