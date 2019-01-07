using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 *     public string m_id;
    public Text m_name;
    public Text m_level;
    public Text m_price;
    public string m_unlocked;
    public Image img_lock;
    public GameObject obj_price;
 */
public class EnvManager : DDOLSingleton<EnvManager> {

    List<EnvProperty> envList = new List<EnvProperty>();

    public void InitEnv(Transform parent_envUI)
    {
    
        for (int i = 0; i < 3; i++)
        {
            envList.Add(parent_envUI.Find("env_" + i).GetComponent<EnvProperty>());
        }

        for (int i = 0; i < envList.Count; i++)
        {
            if (envList[i].m_id == GlobalControl.envDateList[i].m_id)
            {
                envList[i].m_id = GlobalControl.envDateList[i].m_id;
                envList[i].m_name.text = GlobalControl.envDateList[i].m_name;
                envList[i].m_level.text = GlobalControl.envDateList[i].m_unlockLevel;
                envList[i].m_price.text = GlobalControl.envDateList[i].m_price;
                envList[i].m_tournament.SetActive(false);
                envList[i].m_unlocked = GlobalControl.envDateList[i].m_unlocked;
                if (envList[i].m_unlocked == "1")
                {
                    envList[i].img_lock.gameObject.SetActive(false);
                    envList[i].obj_price.SetActive(false);
                    envList[i].GetComponent<Toggle>().interactable = true;
                }
                else
                {
                    envList[i].img_lock.gameObject.SetActive(true);
                    envList[i].obj_price.SetActive(true);
                    envList[i].GetComponent<Toggle>().interactable = false;
                }

                envList[i].m_tournamentLock = GlobalControl.envDateList[i].m_tournamentLock;
                for (int ii = 0; ii < envList[i].m_tournament.transform.childCount; ii++)
                {
                    if (envList[i].m_tournamentLock.Split('_')[ii]=="1")
                    {
                        envList[i].m_tournament.transform.GetChild(ii).GetComponent<Button>().interactable = true;
                    }
                  else
                    {
                        envList[i].m_tournament.transform.GetChild(ii).GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        SetEnv("0");
    }

    internal void SetEnv(string _id)
    {

        GameProperty.ENVID = int.Parse(_id);


        AppDelegate.Instance.OnSetEnvNameEvent(envList[GameProperty.ENVID].m_name.text);

        MainSceneManager.m_Instance.TriggerEnv(false);
    }

    internal void OpenEnvWindow(StoryType type)
    {
        foreach (var item in envList)
        {
            item.m_tournament.SetActive(false);
            item.m_toggle.SetActive(true);
        }

        switch (type)
        {
            case StoryType.computer:
                break;
            case StoryType.friend:
                break;
            case StoryType.tournament:
                foreach (var item in envList)
                {
                    item.m_tournament.SetActive(true);
                    item.m_toggle.SetActive(false);
                }
                break;
        }

        GameProperty.STORYTYPE = type;
    }

    public EnvControl GetCurrentEnv()
    {
        GameObject env = GameObject.Find("Env_" + GameProperty.ENVID);// Instantiate(Resources.Load("prefabs/Env/Env_" + GameProperty.ENVID) as GameObject);

        //env.transform.position = Vector3.zero;

        env.GetComponent<EnvControl>().InitSpawnPos();

        return env.GetComponent<EnvControl>();
    }
}
