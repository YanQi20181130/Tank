using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopProperty : MonoBehaviour {

    public GameObject btn_setting;
    public GameObject btn_selectHero;
    public GameObject btn_selectStory;
    public GameObject btn_selectEnv;
    public GameObject btn_shop;
    public Text t_heroName;
    public Text t_storyName;
    public Text t_envName;
    private void Start()
    {
        EventTriggerListener.Get(btn_setting).onClick = BtnMethod;
        EventTriggerListener.Get(btn_selectHero).onClick = BtnMethod;
        EventTriggerListener.Get(btn_selectStory).onClick = BtnMethod;
        EventTriggerListener.Get(btn_selectEnv).onClick = BtnMethod;
        EventTriggerListener.Get(btn_shop).onClick = BtnMethod;
    }
    private void OnEnable()
    {
        AppDelegate.setHeroNameEvent += SetHeroName;
        AppDelegate.setStoryNameEvent += SetStoryName;
        AppDelegate.setEnvNameEvent += SetEnvName;
    }
    private void OnDisable()
    {
        AppDelegate.setHeroNameEvent -= SetHeroName;
        AppDelegate.setStoryNameEvent -= SetStoryName;
        AppDelegate.setEnvNameEvent -= SetEnvName;
    }
    private void BtnMethod(GameObject go)
    {
        if(go==btn_setting)
        {

        }
        else if(go==btn_selectHero)
        {
            MainSceneManager.m_Instance.TriggerHero(true,false);
        }
        else if(go==btn_selectStory)
        {
            MainSceneManager.m_Instance.TriggerStory(true);
        }
        else if (go == btn_selectEnv)
        {
            MainSceneManager.m_Instance.TriggerEnv(true);
        }
        else if (go == btn_shop)
        {
            MainSceneManager.m_Instance.TriggerShop(true);
        }
    }

    public void SetHeroName(string _name)
    {
        t_heroName.text = _name;
    }

    public void SetStoryName(string _name)
    {
        t_storyName.text = _name;
    }

    public void SetEnvName(string _name)
    {
        t_envName.text = _name;
    }
}
