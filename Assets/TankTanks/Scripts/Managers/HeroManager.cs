using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
/*
public class HeroDate
{
    public string m_id;
    public string m_name;
    public string m_level;
    public string m_gas;
    public string m_price;
    public string m_hp;
    public string m_unlocked;

}
public class HeroProperty 
{
    public string m_id;
    public Text m_name  ;
    public Text m_level ;
    public Text m_gas   ;
    public Text m_price ;
    public Text m_hp    ;
    public int[] m_shellID;
    public string m_unlocked;
    public Image img_lock;
    public GameObject obj_price;
}
    */
public class HeroManager : DDOLSingleton<HeroManager>
{

    int maxValue = 10;
    public void InitHeros(Transform parent_heroUI)
    {
        ShellManager.Instance.shellCount = 0;
        //添加UI到scene中
        for (int i = 0; i < GlobalControl.heroDateList.Count; i++)
        {

            GameObject heroUI = Instantiate(Resources.Load("prefabs/UI/hero_") as GameObject);

            heroUI.name = "hero_" + i;

            heroUI.transform.SetParent(parent_heroUI, false);

            HeroProperty heroProp = heroUI.GetComponent<HeroProperty>();
            SetHeroView(heroProp,GlobalControl.heroDateList[i]);
  
            GlobalControl.heroPropList.Add(heroProp);
            //初始化 shell
            ShellManager.Instance.InitShell(heroProp.parent_shall, true);
        }

        ChooseHero(0);
    }

    /// <summary>
    ///  called after a war
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void AddExperienceValue(int id, int num)
    {
        //update xml of hero experience value
        DataManager.Instance.UpdateXml("HeroDate", "hero_menu", id.ToString(), UpdateType.experienceValue_hero, num);
    }
    /// <summary>
    /// hero 升级
    /// </summary>
    /// <param name="id"></param>
    public void AddLevel(int id)
    {
        if (GlobalControl.COIN < 2000)
        {
            AppDelegate.Instance.OnAlertBool("you don't have enough coins, you can buy in the shop!", MainSceneManager.m_Instance.TriggerShop, true);
            return;
        }
        // update xml 
        DataManager.Instance.UpdateXml("HeroDate", "hero_menu", id.ToString(), UpdateType.level_hero, 1);

        // minus experience 
        AddExperienceValue(id, -maxValue);

        // update global control
        DataManager.Instance.InitXmlData();

        // update hero ui view
        foreach (var item in GlobalControl.heroDateList)
        {
            if (item.m_id == id.ToString())
            {
                HeroProperty heroProp = GlobalControl.heroPropList[id];
                SetHeroView(heroProp,item);
                break;
            }
        }

        // cost money
        MoneyManager.Instance.ChangeMoney(RewardType.coin, -2000);
    }
    public void BuyHero(int id)
    {

        foreach (var item in GlobalControl.heroDateList)
        {
            if (item.m_id == id.ToString())
            {
                if(item.m_price.Split(' ')[1]=="coins")
                {
                    if (GlobalControl.COIN < int.Parse(item.m_price.Split(' ')[0]))
                    {
                        AppDelegate.Instance.OnAlertBool("you don't have enough coins, you can buy in the shop!", MainSceneManager.m_Instance.TriggerShop, true);
                        return;
                    }
                    else
                    {
                        // cost money
                        MoneyManager.Instance.ChangeMoney(RewardType.coin, -1* int.Parse(item.m_price.Split(' ')[0]));
                    }
                }
                else
                {
                    if (GlobalControl.STONE < int.Parse(item.m_price.Split(' ')[0]))
                    {
                        AppDelegate.Instance.OnAlertBool("you don't have enough coins, you can buy in the shop!", MainSceneManager.m_Instance.TriggerShop, true);
                        return;
                    }
                    else
                    {
                        // cost money
                        MoneyManager.Instance.ChangeMoney(RewardType.stone, -1* int.Parse(item.m_price.Split(' ')[0]));
                    }
                }
                break;
            }
        }
        
        // update xml 
        DataManager.Instance.UpdateXml("HeroDate", "hero_menu", id.ToString(), UpdateType.lockState_hero, 1);

        // update global control
        DataManager.Instance.InitXmlData();

        // update hero ui view
        foreach (var item in GlobalControl.heroDateList)
        {
            if (item.m_id == id.ToString())
            {
                HeroProperty heroProp = GlobalControl.heroPropList[id];
                SetHeroView(heroProp, item);
                break;
            }
        }
    }
    /// <summary>
    /// 配置全部heroUI
    /// </summary>
    /// <param name="heroProp"></param>
    /// <param name="item"></param>
    private void SetHeroView(HeroProperty heroProp,HeroData item)
    {
        heroProp.m_id = item.m_id;
        heroProp.m_name.text = item.m_name;
        heroProp.m_level.text = item.m_level;
        heroProp.m_gas.text = item.m_gas;
        heroProp.m_price.text = item.m_price;
        if (item.m_price.Split(' ')[1]=="coins")
        {
            heroProp.m_moneyImg.texture = Resources.Load("chest/coin") as Texture;
        }
        else
        {
            heroProp.m_moneyImg.texture = Resources.Load("chest/stone") as Texture;
        }
        heroProp.m_hp.text = item.m_hp;
        //experience value
        heroProp.m_experienceValue = item.m_experienceValue;


        heroProp.m_levelSlider.maxValue = maxValue;
        if (heroProp.m_experienceValue <= maxValue)
        {
            heroProp.text_levelSlider.text = heroProp.m_experienceValue + " / " + maxValue;
            heroProp.m_levelSlider.value = heroProp.m_experienceValue;
        }
        else
        {
            heroProp.text_levelSlider.text = heroProp.m_experienceValue + " / " + maxValue;
            heroProp.m_levelSlider.value = maxValue;
        }

        heroProp.m_unlocked = item.m_unlocked;

        if (heroProp.m_unlocked == "1")
        {
            heroProp.btn_choose.gameObject.SetActive(true);
            //判断是否可以升级
            if (heroProp.m_experienceValue >= maxValue)
            {
                heroProp.btn_buy.SetActive(true);
            }
            else
            {
                heroProp.btn_buy.SetActive(false);
            }
        }
        else
        {
            heroProp.btn_choose.gameObject.SetActive(false);
            heroProp.btn_buy.SetActive(true);
        }
    }

    /// <summary>
    /// choose button method
    /// </summary>
    /// <param name="_id"></param>
    internal void ChooseHero(int _id)
    {
       

        if(GameProperty.STORYTYPE==StoryType.friend)
        {
            AppDelegate.Instance.OnTPChangeImg(_id);
        }
        else
        {
            GameProperty.HERO1_ID = _id;

            AppDelegate.Instance.OnSetHeroNameEvent(GlobalControl.heroDateList[GameProperty.HERO1_ID].m_name);

            MainSceneManager.m_Instance.TriggerHero(false, false);
        }

    }


    public GameObject GetTank(int _id)
    {
        GameObject tankObj = Instantiate(Resources.Load("prefabs/Heros/hero_" + _id) as GameObject);
        return tankObj;
    }
}
