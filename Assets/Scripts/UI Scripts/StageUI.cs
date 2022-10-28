using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class StageUI : MonoBehaviour
{
    private SceneChange IngameSceneChange;

    GameObject[] EnemyCount;
    private int EnemyCountNum = 0;
    private bool StageClear = false;

    [Space(8)]
    public Button _Stage1;
    public Button _Stage2;
    public Button _Stage3;
    public Button _Stage4;
    public Button _Stage5;
    public Button _Stage6;
    public Button _Stage7;
    public Button _Stage8;
    [SerializeField]
    public int CurrentStage;

    private void Start()
    {
        IngameSceneChange = this.gameObject.GetComponent<SceneChange>();
        _Stage1.onClick.AddListener(() => StageChange(1));
        _Stage2.onClick.AddListener(() => StageChange(2));
        _Stage3.onClick.AddListener(() => StageChange(3));
        _Stage4.onClick.AddListener(() => StageChange(4));
        _Stage5.onClick.AddListener(() => StageChange(5));
        _Stage6.onClick.AddListener(() => StageChange(6));
        _Stage7.onClick.AddListener(() => StageChange(7));
        _Stage8.onClick.AddListener(() => StageChange(8));
    }
    public void StageChange(int CrtStage)
    {
        if (CrtStage <= GameManager.Instance.GetClearStage() + 1)
        {
            IngameSceneChange.CurrentSceneChange();
            GameObject.Find("Dynamic UI").transform.Find("Stage Panel").gameObject.SetActive(false);
        }
        CurrentStage = CrtStage;
        Debug.Log(CurrentStage);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Dungeon Scene") //����Ŭ����� ���;��ϴ� �̺�Ʈ �Է�
        {
            StartCoroutine(EnemyCountCheck());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            IngameSceneChange.CurrentScene();
        }
    }
    void Cleardungeon() // ���� Ŭ����� �� ����
    {
        GameManager.Instance.SetClearStage(CurrentStage);
        IngameSceneChange.CurrentScene();
        Debug.Log(GameManager.Instance.GetClearStage());
    }

    public IEnumerator EnemyCountCheck()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCountNum = Int32.Parse(EnemyCount.Length.ToString());
        if (EnemyCountNum == 0)
        {
            StageClear = true;
            if (StageClear == true)
            {
                Cleardungeon();

                StageClear = false;
            }
        }
        else
        {
            EnemyCountNum = Int32.Parse(EnemyCount.Length.ToString());
        }

        yield break;
    }
}