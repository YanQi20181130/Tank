using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellManager : DDOLSingleton<ShellManager>
{
    public int shellCount = 0;
    int maxValue = 10;

    public void InitShell(Transform parent_shellUI, bool ss)
    {
        
        //添加UI到scene中
        for (int i = 0; i < 6; i++)
        {

            GameObject ui_shell = Instantiate(Resources.Load("prefabs/UI/shell_") as GameObject);
            ui_shell.name = "shell_" + i;

            ui_shell.transform.SetParent(parent_shellUI, false);
            ShellProperty shellprop = ui_shell.GetComponent<ShellProperty>();
            shellprop.m_id = shellCount.ToString();

            shellprop.m_name.text = GlobalControl.shellDateList[shellCount].m_name;
            shellprop.m_level = GlobalControl.shellDateList[shellCount].m_level;

            int curLevel = int.Parse(shellprop.m_level);

            // star based by level
            for (int istar = 0; istar < shellprop.parent_levelStar.childCount; istar++)
            {
                if (istar < curLevel)
                {
                    shellprop.parent_levelStar.GetChild(istar).gameObject.SetActive(true);
                }
                else
                {
                    shellprop.parent_levelStar.GetChild(istar).gameObject.SetActive(false);
                }
            }

            // price
            if(curLevel<=4)
            shellprop.m_level_Price.text = GlobalControl.shellDateList[shellCount].m_level_Price.Split('_')[curLevel];

            // damage
            shellprop.m_damage = float.Parse(GlobalControl.shellDateList[shellCount].m_damage.Split('_')[curLevel]);

            // min distance
            shellprop.m_minDis = float.Parse(GlobalControl.shellDateList[shellCount].m_minDis);

            // max distance
            shellprop.m_maxDis = float.Parse(GlobalControl.shellDateList[shellCount].m_maxDis);

            // damage range
            shellprop.m_damageRange = float.Parse(GlobalControl.shellDateList[shellCount].m_damageRange);

            // experienceValue;
            shellprop.m_experienceValue = GlobalControl.shellDateList[shellCount].m_experienceValue;

            

            shellprop.m_levelSlider.maxValue = maxValue;
            if (shellprop.m_experienceValue <= maxValue)
            {
                shellprop.text_levelSlider.text = shellprop.m_experienceValue + " / "+ maxValue;
                shellprop.m_levelSlider.value = shellprop.m_experienceValue;
            }
            else
            {
                shellprop.text_levelSlider.text = shellprop.m_experienceValue  + " / "+ maxValue;
                shellprop.m_levelSlider.value = maxValue;
            }

            //判断是否可以升级
            if (shellprop.m_experienceValue >= maxValue &&int.Parse( shellprop.m_level)<5)
            {
                shellprop.btn_update.SetActive(true);
            }
            else
            {
                shellprop.btn_update.SetActive(false);
            }

            // image
            loadSprite(ui_shell.GetComponent<Image>(), shellprop.m_id);

            // btn
            EventTriggerListener.Get(ui_shell).onClick = BtnMethod;

            GlobalControl.shellPropList.Add(shellprop);

            shellCount++;
        }

    }

    private void loadSprite(Image icon_shell, string id)
    {
        Texture2D m_Tex = Resources.Load("textures/shell_" + id) as Texture2D;

        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(0, 0));

        icon_shell.sprite = tempSprite;
    }

    private void BtnMethod(GameObject go)//go : shall image ui
    {
        ShowShellWindow(go.GetComponent<ShellProperty>().m_id);
    }

    public void ShowShellWindow(string id)
    {
        ShellWindow sw;
        if (GameObject.Find("Canvas_shellWindow"))
        {
            sw = GameObject.Find("Canvas_shellWindow").GetComponent<ShellWindow>();
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("prefabs/UI/Canvas_shellWindow"));

            sw = obj.GetComponent<ShellWindow>();

            sw.name = "Canvas_shellWindow";
        }

        sw.Show(id);
    }

    /// <summary>
    ///  called after a war
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void AddExperienceValue(int id,int num)
    {
        //update xml of hero experience value
        DataManager.Instance.UpdateXml("ShellDate", "shell_menu", id.ToString(), UpdateType.experienceValue_shell, num);

    }

    public void AddLevel(int id)
    {
        // 判断金币数量
        int updatePrice = 0;
        foreach (var item in GlobalControl.shellDateList)
        {
            if(item.m_id==id.ToString())
            {
                if (int.Parse(item.m_level) <= 4)
                {
                    updatePrice = int.Parse(item.m_level_Price.Split('_')[int.Parse(item.m_level)]);
                }
                else
                {

                }
                break;
            }
        }
        if (GlobalControl.COIN < updatePrice)
        {
            AppDelegate.Instance.OnAlertBool("you don't have enough coins, you can buy in the shop!", MainSceneManager.m_Instance.TriggerShop, true);
            return;
        }

        // update xml 
        DataManager.Instance.UpdateXml("ShellDate", "shell_menu", id.ToString(), UpdateType.level_shell, 1);

        // minus experience 
        AddExperienceValue(id, -maxValue);

        // update GlobalControl.shellDateList
        DataManager.Instance.InitXmlShell();

        // update shell ui view
        foreach (var item in GlobalControl.shellDateList)
        {
            if(item.m_id==id.ToString())
            {
                Debug.Log("id= "+id);
                foreach (var sp in GlobalControl.shellPropList)
                {
                    if(sp.m_id== item.m_id)
                    {
                        sp.m_level = item.m_level;
                        if (item.m_experienceValue <= maxValue)
                        {
                            sp.m_levelSlider.value = item.m_experienceValue;
                        }
                        else
                        {
                            sp.m_levelSlider.value = maxValue;
                        }

                        sp.text_levelSlider.text = item.m_experienceValue + " / " + maxValue;

                        int curLevel = int.Parse(sp.m_level);

                        // star based by level
                        for (int istar = 0; istar < sp.parent_levelStar.childCount; istar++)
                        {
                            if (istar < curLevel)
                            {
                                sp.parent_levelStar.GetChild(istar).gameObject.SetActive(true);
                            }
                            else
                            {
                                sp.parent_levelStar.GetChild(istar).gameObject.SetActive(false);
                            }
                        }

                        //判断是否可以升级
                        if (sp.m_experienceValue >= maxValue)
                        {
                            sp.btn_update.SetActive(true);
                        }
                        else
                        {
                            sp.btn_update.SetActive(false);
                        }

                        // price
                        if (curLevel <= 4)
                            sp.m_level_Price.text = item.m_level_Price.Split('_')[curLevel];

                        // damage
                        sp.m_damage = float.Parse(item.m_damage.Split('_')[curLevel]);
                        break;
                    }
                }



                ShowShellWindow(id.ToString());
                break;
            }
        }

        // cost money
        MoneyManager.Instance.ChangeMoney(RewardType.coin, -updatePrice);
    }
}