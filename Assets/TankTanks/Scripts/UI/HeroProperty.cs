using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroProperty : MonoBehaviour {
    public string m_id;
    public Text m_name;
    public Text m_level;
    public Text m_gas;
    public Text m_price;
    public Text m_hp;
    public int[] m_shellID;
    public string m_unlocked;
    public GameObject btn_choose;
    public GameObject btn_buy;
    public Transform parent_shall;

    public Slider m_levelSlider;
    public Text text_levelSlider;
    public int m_experienceValue;
    public RawImage m_moneyImg;

    private void Start()
    {
        EventTriggerListener.Get(btn_buy).onClick = BtnMethod;
        EventTriggerListener.Get(btn_choose).onClick = BtnMethod;

    }

    private void BtnMethod(GameObject go)
    {
        if(go==btn_buy)
        {
            if (m_unlocked == "0")
            {
                // buy hero
                HeroManager.Instance.BuyHero(int.Parse(m_id));
            }
            else
            {
                //buy or update the hero
                HeroManager.Instance.AddLevel(int.Parse(m_id));
            }
        }
        else if(go==btn_choose)
        {
            HeroManager.Instance.ChooseHero(int.Parse(m_id));
        }
    }
}
