using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MyEditor : Editor
{

    [MenuItem("MyEditor/CleanRegistry")]
    static void CleanRegistry()
    {
        PlayerPrefs.DeleteAll();
        AssetDatabase.Refresh();
    }

    [MenuItem("MyEditor/Main Scene set public")]
    static void SetPublic()
    {
        //MainUIView muv = GameObject.Find("MainUICanvas").GetComponent<MainUIView>();

        //Transform levelParent = GameObject.Find("Content").transform;
    
        //int levelcount = 5;

        //muv.text_progress = new Text[levelcount];
        //muv.text_name = new Text[levelcount];
        //muv.img_lock = new Image[levelcount];
        //muv.levelStars = new Transform[levelcount];

        //for (int i = 0; i < levelcount; i++)
        //{
       
        //    //Progress_text
        //    muv.text_progress[i] = levelParent.Find("Level_" + (i + 1)).Find("LevelState").Find("Progress_text").GetComponent<Text>();
        //    //name
        //    muv.text_name[i] = levelParent.Find("Level_" + (i + 1)).Find("LevelState").Find("name").GetComponent<Text>();
        //    //star_icon
        //    muv.levelStars[i] = levelParent.Find("Level_" + (i + 1)).Find("LevelState").Find("star_icon");
        //    //lock
        //    muv.img_lock[i] = levelParent.Find("Level_" + (i + 1)).Find("LevelState").Find("lock").GetComponent<Image>();
        //}

    }
}
