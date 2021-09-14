using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum GameState//게임 기본 세팅
{
    menu,
    inGame,
    gameover
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public bool isPause = false;
    public GameState currentGameState = GameState.menu; // 게임 시작시 설정 값 변수.
    private static GameManager _instance;
    private GameObject player;
    private SpriteRenderer playerRenderer;

    private string activelevel; // 레벨설정
    private string myname; // 캐릭터 이름
    private int maxHp; // 최대 체력
    private int maxMp; // 최대 마나
    private int maxExp; // 최대 경험치
    private int HP; // 현재 체력
    private int MP; // 현재 마나
    private int STR;  // 힘( 공격력 체력증가)
    private int INT; //  지능( 주문력 마나 증가)
    private int FIT; // 체력( 이속 및 체력 마나 회복량 증가)
    private int EXP;  //경험치
    



    public void StartGame()//게임 시작 함수
    {
        SetGameState(GameState.inGame);
    }
    public void GameOver()//게임 종료 함수
    {

    }
    public void BackToMenu() // 메뉴 함수
    {

    }
    void Start()
    {
        currentGameState = GameState.menu;// 시작시 게임상태 변경
    }

    void SetGameState(GameState newGameState)// 게임 상태
    {
        if (newGameState == GameState.menu) // 새게임시
        {
            
        }
        else if (newGameState == GameState.inGame) // 게임 진행시
        {
            activelevel = "Level 1"; // 레벨 세팅
            myname = "Charater"; // 이름 세팅
            maxHp = 50; // 체력세팅
            maxMp = 200; // 마나 세팅
            maxExp = 300; // 최대경험치 세팅(레벨업시 증가)
            HP = maxHp; // 시작시 최대체력으로 세팅
            MP = maxMp; // 시작시 최대마나로 세팅
            STR = 2; // 힘 세팅
            INT = 10; // 지능 세팅
            FIT = 2; // 체력 세팅
            EXP = 0; // 경험치 세팅
        }
        else if (newGameState == GameState.gameover) // 게임 종료시
        {

        }
    }

    public string getLevel() // 레벨불러오기
    {
        return activelevel;
    }

    public void setLevel(string newlevel) // 레벨업시 사용
    {
        activelevel = newlevel;
    }

    public string getName() // 이름 불러오기
    {
        return myname;
    }

    public int SetExp() // 현재 경험치 불러오기
    {
        return EXP;
    }

    public int SetMaxExp() // 최대 경험치 불러오기
    {
        return maxExp;
    }

    public int getexp(int newExp) // 얻은 경험치 불러오기
    {
        EXP += newExp;
        return EXP;
    }
    public int getHp() //HP불러오기
    {
        return HP;
    }
    public int getmaxHp() //MAX HP불러오기
    {
        return maxHp;
    }
    public int getMp() //MP 불러오기
    {
        return MP;
    }
    public int getmaxMp() //MAX MP 불러오기
    {
        return maxMp;
    }

    public int getSTR(int newSTR) // STR 증가값 불러오기
    {
        STR += newSTR;
        return STR;
    }
    public int getINT(int newINT) // INT 증가값 불러오기
    {
        INT += newINT;
        return INT;
    }
    public int getFIT(int newFIT) // FIT 증가값 불러오기
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

    private void Update()
    {
        //전체화면 토글
        if (Input.GetKeyDown(KeyCode.Escape))
            Screen.fullScreen = !Screen.fullScreen;
        if (Input.GetButtonDown("m"))// z키 입력시 게임 시작
        {
            StartGame();
        }
       
    }
}

public class GameContorl
{
    public void IsPause()
    {
        GameManager.Instance.isPause = true;
    }
}
