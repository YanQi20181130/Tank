using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellWindow : MonoBehaviour {
    private int m_id;
    public Text m_name;
    public string m_level;
    public Transform parent_levelStar;

    public Image icon_shell;

    public GameObject btn_update;
    private GameObject m_shellProp_btnUpdate;
    public Text m_price;

    public Text m_minDis;
    public Text m_maxDis;
    public Text m_damageRange;
    public Text m_damage;

    public GameObject btn_close;

    private void Start()
    {
        EventTriggerListener.Get(btn_update).onClick = btnMethod;
        EventTriggerListener.Get(btn_close).onClick = btnMethod;
    }

    private void btnMethod(GameObject go)
    {
        if (go == btn_close)
        {
            GetComponent<Canvas>().enabled = false;
        }
        else if (go == btn_update)
        {
            ShellManager.Instance.AddLevel(m_id);
        }

    }

    public void Show(string id)
    {
    
        m_id =int.Parse(id);

        foreach (var item in GlobalControl.shellDateList)
        {
            if (item.m_id == id)
            {
                m_name.text = item.m_name;

                if (int.Parse(item.m_level) <= 4)
                    m_price.text = item.m_level_Price.Split('_')[int.Parse(item.m_level)];

                m_damage.text = "POWER :" + item.m_damage.Split('_')[int.Parse(item.m_level)];

                m_minDis.text ="MIN DISTANCE :"+ item.m_minDis;
                m_maxDis.text = "MAX DISTANCE :" + item.m_maxDis;
                m_damageRange.text = "DAMAGE RANGE :" + item.m_damageRange;
                LoadShellSprite(id);

                //判断是否可以升级
                if (item.m_experienceValue>= 10 && int.Parse(item.m_level) < 5)
                {
                    btn_update.SetActive(true);
                }
                else
                {
                    btn_update.SetActive(false);
                }

                for (int istar = 0; istar <parent_levelStar.childCount; istar++)
                {
                    if (istar < int.Parse(item.m_level))
                    {
                        parent_levelStar.GetChild(istar).gameObject.SetActive(true);
                    }
                    else
                    {
                        parent_levelStar.GetChild(istar).gameObject.SetActive(false);
                    }
                }

                break;
            }
        }

        GetComponent<Canvas>().enabled = true;
    }

    private void LoadShellSprite(string id)
    {
        Texture2D m_Tex = Resources.Load("textures/shell_"+id) as Texture2D;

        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(0, 0));

        icon_shell.sprite = tempSprite;
    }
}
