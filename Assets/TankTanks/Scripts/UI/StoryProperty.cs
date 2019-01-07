using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryProperty : MonoBehaviour {

    public string m_id;
    public Text m_name;
    public string m_canSelectScene;
    public StoryType storyT;
    private void Start()
    {
        EventTriggerListener.Get(gameObject).onClick = btnMethod;
    }

    private void btnMethod(GameObject go)
    {

        StoryManager.Instance.SetStory(storyT, m_name.text);
    }
}
