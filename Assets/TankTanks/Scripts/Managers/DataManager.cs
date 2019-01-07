using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

#region Data Class

public class HeroData
{
    public string m_id;
    public string m_name;
    public string m_level;
    public string m_gas;
    public string m_price;
    public string m_hp;
    public string m_unlocked;
    public int m_experienceValue;
}

public class ShellData
{
    public string m_id;
    public string m_name;
    public string m_level_Price;
    public string m_damage;
    public string m_minDis;
    public string m_maxDis;
    public string m_damageRange;
    public string m_level;
    public int m_experienceValue;
    public float m_lunchTime;
    public int m_easeType;
    public int m_pathNum;
    public int m_bulletCount;
}

public class EnvData
{
    public string m_id;
    public string m_name;
    public string m_price;
    public string m_stoneRange;
    public string m_coinRange;
    public string m_unlockLevel;
    public string m_unlocked;
    public string m_tournamentLock;
}

public class StoryData
{
    public string m_id;
    public string m_name;
    public string m_canSelectScene;
}

public enum UpdateType
{
    level_hero,
    level_shell,
    experienceValue_hero,
    experienceValue_shell,
    lockState_hero,
    lockState_env,
}

#endregion

public class DataManager : Singleton<DataManager>
{
    #region File manager

   
    public static readonly string PathURL =
#if UNITY_ANDROID   //安卓
    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE  //iPhone
	Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台
	"file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif
    /**
* path：文件创建目录
* name：文件的名称
*  info：写入的内容
*/
    void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();

        }
        else
        {
            //如果此文件存在则删除
            File.Delete(path + "//" + name);

            //再创建
            sw = t.CreateText();

            //sw = t.AppendText();

        }
        //以行的形式写入信息
        sw.WriteLine(info);

        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }



    /**
     * 读取文本文件
     * path：读取文件的路径
     * name：读取文件的名称
     */
    ArrayList LoadFile(string path, string name)
    {
        //使用流的形式读取
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + name);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            arrlist.Add(line);
        }
        //关闭流
        sr.Close();
        //销毁流
        sr.Dispose();
        //将数组链表容器返回
        return arrlist;
    }

    /// <summary>
    /// 只在用户第一次启动应用时执行一次
    /// </summary>
    public void StoreXmlFiles()
    {

        TextAsset ta = Resources.Load("HeroDate") as TextAsset;
        CreateFile(Application.persistentDataPath, "HeroDate.XML", ta.text);

        TextAsset tas = Resources.Load("ShellDate") as TextAsset;
        CreateFile(Application.persistentDataPath, "ShellDate.XML", tas.text);

    }
    #endregion

    public void InitXmlData()
    {
        GlobalControl.heroDateList = new List<HeroData>();
        GlobalControl.envDateList = new List<EnvData>();
        GlobalControl.storyDateList = new List<StoryData>();

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Application.persistentDataPath + "/HeroDate.XML");
        XmlNodeList heroNodeList = xmldoc.SelectNodes(@"root/hero_menu/hero");
        foreach (XmlNode heroNode in heroNodeList)
        {

            HeroData hd = new HeroData();
            foreach (XmlNode heroPara in heroNode)
            {
                if (heroPara.Name == "id")
                {
                    hd.m_id = heroPara.InnerText;
                }
                else if (heroPara.Name == "name")
                {
                    hd.m_name = heroPara.InnerText;
                }
                else if (heroPara.Name == "level")
                {
                    hd.m_level = heroPara.InnerText;
                }
                else if (heroPara.Name == "price")
                {
                    hd.m_price = heroPara.InnerText;
                }
                else if (heroPara.Name == "gas")
                {
                    hd.m_gas = heroPara.InnerText;
                }
                else if (heroPara.Name == "hp")
                {
                    hd.m_hp = heroPara.InnerText;
                }
                else if (heroPara.Name == "unlock")
                {
                    hd.m_unlocked = heroPara.InnerText;
                }
                else if (heroPara.Name == "ExperienceValue")
                {
                    hd.m_experienceValue =int.Parse( heroPara.InnerText);
                }
            }
            GlobalControl.heroDateList.Add(hd);
        }

        XmlNodeList envNodeList = xmldoc.SelectNodes(@"root/env_menu/env");
        foreach (XmlNode envNode in envNodeList)
        {
            EnvData sd = new EnvData();
            foreach (XmlNode envPara in envNode)
            {
                if (envPara.Name == "id")
                {
                    sd.m_id = envPara.InnerText;
                }
                else if (envPara.Name == "name")
                {
                    sd.m_name = envPara.InnerText;
                }
                else if (envPara.Name == "price")
                {
                    sd.m_price = envPara.InnerText;
                }
                else if (envPara.Name == "stoneRange")
                {
                    sd.m_stoneRange = envPara.InnerText;
                }
                else if (envPara.Name == "coinRange")
                {
                    sd.m_coinRange = envPara.InnerText;
                }
                else if (envPara.Name == "unlockLevel")
                {
                    sd.m_unlockLevel = envPara.InnerText;
                }
                else if (envPara.Name == "unlock")
                {
                    sd.m_unlocked = envPara.InnerText;
                }
                else if (envPara.Name == "tournamentLock")
                {
                    sd.m_tournamentLock = envPara.InnerText;
                }
            }
            GlobalControl.envDateList.Add(sd);
        }
        XmlNodeList storyNodeList = xmldoc.SelectNodes(@"root/story_menu/story");
        foreach (XmlNode storyNode in storyNodeList)
        {
            StoryData sd = new StoryData();
            foreach (XmlNode storyPara in storyNode)
            {
                if (storyPara.Name == "id")
                {
                    sd.m_id = storyPara.InnerText;
                }
                else if (storyPara.Name == "name")
                {
                    sd.m_name = storyPara.InnerText;
                }

                else if (storyPara.Name == "canSelectScene")
                {
                    sd.m_canSelectScene = storyPara.InnerText;
                }

            }

            GlobalControl.storyDateList.Add(sd);
        }

    }
    public void InitXmlShell()
    {
        GlobalControl.shellDateList = new List<ShellData>();
 
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Application.persistentDataPath+ "/ShellDate.XML");
        XmlNodeList shellNodeList = xmldoc.SelectNodes(@"root/shell_menu/shell");
        foreach (XmlNode node in shellNodeList)
        {
            ShellData sd = new ShellData();
            foreach (XmlNode shellPara in node)
            {
                if (shellPara.Name == "id")
                {
                    sd.m_id = shellPara.InnerText;
                }
                else if (shellPara.Name == "name")
                {
                    sd.m_name = shellPara.InnerText;
                }
                else if (shellPara.Name == "level_Price")
                {
                    sd.m_level_Price = shellPara.InnerText;
                }
                else if (shellPara.Name == "level_Damage")
                {
                    sd.m_damage = shellPara.InnerText;
                }
                else if (shellPara.Name == "minDis")
                {
                    sd.m_minDis = shellPara.InnerText;
                }
                else if (shellPara.Name == "maxDis")
                {
                    sd.m_maxDis = shellPara.InnerText;
                }
                else if (shellPara.Name == "damageRange")
                {
                    sd.m_damageRange = shellPara.InnerText;
                }
                else if (shellPara.Name == "level")
                {
                    sd.m_level = shellPara.InnerText;

                }
                else if (shellPara.Name == "ExperienceValue")
                {
                    sd.m_experienceValue = int.Parse(shellPara.InnerText);
                }
                else if (shellPara.Name == "easeType")
                {
                    sd.m_easeType = int.Parse(shellPara.InnerText);
                }
                else if (shellPara.Name == "lunchTime")
                {
                    sd.m_lunchTime = float.Parse(shellPara.InnerText);
                }
                else if (shellPara.Name == "pathNum")
                {
                    sd.m_pathNum = int.Parse(shellPara.InnerText);
                }
                else if (shellPara.Name == "bulletCount")
                {
                    sd.m_bulletCount = int.Parse(shellPara.InnerText);
                }
                
            }
            GlobalControl.shellDateList.Add(sd);
        }
    }
    public void InitCoinAndStone()
    {
        if (!PlayerPrefs.HasKey("oldplayer"))
        {
            // newer 
            PlayerPrefs.SetString("oldplayer", "1");
            PlayerPrefs.SetInt("coin", 0);
            PlayerPrefs.SetInt("stone", 0);

            StoreXmlFiles();

            RewardManager.Instance.ShowReward(RewardType.coin,500);
        }
        GlobalControl.COIN = PlayerPrefs.GetInt("coin");
        GlobalControl.STONE = PlayerPrefs.GetInt("stone");


    }

    #region Update XML

 
    public void UpdateXml(string _fileName,string _menuName,string _id, UpdateType _updateType,int num)
    {
        string filepath = Application.persistentDataPath + "/"+ _fileName + ".XML";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode(@"root/"+ _menuName).ChildNodes;
            foreach (XmlElement xe in nodeList)// all hero
            {
                if (xe.GetAttribute("id") == _id)
                {
                    foreach (XmlElement x1 in xe.ChildNodes)// children list of hero
                    {
                        switch (_updateType)
                        {
                            case UpdateType.level_hero:
                                UpdateLevelHero(x1);
                                break;

                            case UpdateType.level_shell:
                                UpdateLevelShell(x1);
                                break;

                            case UpdateType.experienceValue_hero:
                                UpdateExperienceValue_hero(x1, num);
                                break;
                            case UpdateType.experienceValue_shell:
                                UpdateExperienceValue_shell(x1, num);
                                break;

                            case UpdateType.lockState_hero:
                                UpdatelockState_hero(x1);
                                break;
                            case UpdateType.lockState_env:
                                UpdatelockState_env(x1);
                                break;
                        }
                    }
                    break;
                }
            }
            xmlDoc.Save(filepath);
          
        }
    }

    private void UpdateLevelHero(XmlElement x1)
    {
        if (x1.Name == "level")
        {
            x1.InnerText = (int.Parse(x1.InnerText) + 1).ToString();
        }
        else if (x1.Name == "gas")
        {
            x1.InnerText = (int.Parse(x1.InnerText) * 1.1f).ToString();
        }
        else if (x1.Name == "hp")
        {
            x1.InnerText = (int.Parse(x1.InnerText) * 1.1f).ToString();
        }
    }
    private void UpdateLevelShell(XmlElement x1)
    {
        if (x1.Name == "level")
        {
            x1.InnerText = (int.Parse(x1.InnerText) + 1).ToString();
        }
    }
    private void UpdateExperienceValue_hero(XmlElement x1,int num)
    {
        if (x1.Name == "ExperienceValue")
        {
            x1.InnerText = (int.Parse(x1.InnerText) + num).ToString();
        }
    }
    private void UpdateExperienceValue_shell(XmlElement x1, int num)
    {
        if (x1.Name == "ExperienceValue")
        {
            x1.InnerText = (int.Parse(x1.InnerText) + num).ToString();
        }
    }
    private void UpdatelockState_hero(XmlElement x1)
    {
        if (x1.Name == "unlock")
        {
            x1.InnerText = "1";
        }
    }
    private void UpdatelockState_env(XmlElement x1)
    {
        if (x1.Name == "unlock")
        {
            x1.InnerText = "1";
        }
    }

    #endregion
}

