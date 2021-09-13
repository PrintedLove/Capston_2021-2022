using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{

    [SerializeField]  private Slider PlayerHealthSlider;
    [SerializeField]  private Slider PlayerManaSlider;
    [SerializeField]  private Slider PlayerExpSlider;
    [SerializeField]  private Text HpText;
    [SerializeField]  private Text MpText;
    [SerializeField]  private Text ExpText;


    private void Start()
    {
        
    }

    private void Update()//HP, MP, ����ġ �� ����
    {
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        //HP��
        int MaxHp = GameManager.Instance.getmaxHp();
        int Hp = GameManager.Instance.getHp();

        PlayerHealthSlider.maxValue = MaxHp;
        PlayerHealthSlider.value = Hp;

        HpText.text = Hp + " / " + MaxHp;

        //MP��
        int MaxMp = GameManager.Instance.getmaxMp();
        int Mp = GameManager.Instance.getMp();

        PlayerManaSlider.maxValue = MaxMp;
        PlayerManaSlider.value = Mp;

        MpText.text = Mp + " / " + MaxMp;

        //EXP��
        int MaxExp = GameManager.Instance.SetMaxExp();
        int Exp = GameManager.Instance.SetExp();

        PlayerExpSlider.maxValue = MaxExp;
        PlayerExpSlider.value = Exp;

        ExpText.text = Exp + " / " + MaxExp;
    }
}
