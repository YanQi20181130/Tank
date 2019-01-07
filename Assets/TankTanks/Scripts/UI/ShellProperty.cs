using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellProperty : MonoBehaviour {
    public string m_id;
    public Text m_name;
    public string m_level;
    public Transform parent_levelStar;

    public GameObject btn_update;
    public Text m_level_Price;

    public float m_minDis;
    public float m_maxDis;
    public float m_damageRange;
    public float m_damage;

    public Slider m_levelSlider;
    public Text text_levelSlider;
    public int m_experienceValue;

    //private void Start()
    //{
    //    EventTriggerListener.Get(btn_update).onClick = BtnMethod;
    //}

    //private void BtnMethod(GameObject go)
    //{
    //    //update the shell
    //    ShellManager.Instance.AddLevel(int.Parse(m_id),go);
    //}
}
