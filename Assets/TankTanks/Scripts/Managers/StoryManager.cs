using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *    public string m_id;
    public Text m_name;
    public string m_canSelectScene;
 */
public enum StoryType
{
    computer,
    friend,
    tournament
}
public class StoryManager : Singleton<StoryManager> {

    private List<StoryProperty> storyList;

    public void InitStory(Transform parent_storyUI)
    {
        storyList = new List<StoryProperty>();

        for (int i = 0; i < 3; i++)
        {
            storyList.Add(parent_storyUI.Find("story_" + i).GetComponent<StoryProperty>());
        }

        for (int i = 0; i < storyList.Count; i++)
        {
            if (storyList[i].m_id == GlobalControl.storyDateList[i].m_id)
            {
                storyList[i].m_id = GlobalControl.storyDateList[i].m_id;
                storyList[i].m_name.text = GlobalControl.storyDateList[i].m_name;
                storyList[i].m_canSelectScene = GlobalControl.storyDateList[i].m_canSelectScene;
            }
        }
        SetStory(storyList[0].storyT, storyList[0].m_name.text);
    }

    public void SetStory(StoryType storyT,string s)
    {
        GameProperty.STORYTYPE = storyT;

        AppDelegate.Instance.OnSetStoryNameEvent(s);

        MainSceneManager.m_Instance.TriggerStory(false);

        if(storyT==StoryType.friend)
        {
            GameProperty.HERO1_ID = -1;
            GameProperty.HERO2_ID = -1;
            AppDelegate.Instance.OnSetHeroNameEvent(null);
            MainSceneManager.m_Instance.TriggerHero(true, true);
        }
    }
}
