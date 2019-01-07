using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerControl : MonoBehaviour {



    public Text t_guide;
    public Text t_playerName1;
    public Text t_playerName2;

    public RawImage img_1;
    public RawImage img_2;

    public Button btn_deleteImg_1;
    public Button btn_deleteImg_2;
    public Button btn_OK;

    private void Start()
    {

        EventTriggerListener.Get(btn_deleteImg_1.gameObject).onClick = BtnMethod;
        EventTriggerListener.Get(btn_deleteImg_2.gameObject).onClick = BtnMethod;
        EventTriggerListener.Get(btn_OK.gameObject).onClick = BtnMethod;

        AppDelegate.OpenTwoPlayerEvent += OpenTwoPlayerWindow;
        AppDelegate.TPChangeImgEvent += ChangeImg;
    }

    private void BtnMethod(GameObject go)
    {
        if(go== btn_deleteImg_1.gameObject)
        {
            btn_deleteImg_1.interactable = false;
            DeleteImg(1, img_1);
        }
        else if(go == btn_deleteImg_2.gameObject)
        {
            btn_deleteImg_2.interactable = false;
            DeleteImg(2, img_2);
        }
        else if(go==btn_OK.gameObject)
        {
            AppDelegate.Instance.OnSetHeroNameEvent(GlobalControl.heroDateList[GameProperty.HERO1_ID].m_name +
                " VS "+ 
                GlobalControl.heroDateList[GameProperty.HERO2_ID].m_name);

            MainSceneManager.m_Instance.TriggerHero(false, true);

            Debug.Log(GameProperty.HERO1_ID+" , "+ GameProperty.HERO2_ID);
        }
    }

    public void OpenTwoPlayerWindow(bool isOpen)
    {
        if (this != null)
        {
            gameObject.SetActive(isOpen);

            ChangeGuideText(1);
            t_playerName1.text = "";
            t_playerName2.text = "";
            img_1.enabled = false;
            img_2.enabled = false;
            btn_deleteImg_1.interactable = false;
            btn_deleteImg_2.interactable = false;
            btn_OK.interactable = false;
        }
    }

    private void ChangeImg(int _id)
    {
        if (img_1.enabled==false)
        {
            ChangeImg1(_id);
        }
        else
        {
            ChangeImg2(_id);
        }
    }


    private void ChangeImg1(int _id)
    {
        btn_deleteImg_1.interactable = true;

        img_1.texture = Resources.Load("textures/hero_" + _id) as Texture;

        img_1.enabled = true;

        t_playerName1.text = GlobalControl.heroDateList[_id].m_name;

        GameProperty.HERO1_ID = _id;

        if (img_2.enabled)
        {
            ChangeGuideText(3);
        }
       else
        {
            ChangeGuideText(2);
        }
    }

    private void ChangeImg2(int _id)
    {
        btn_deleteImg_2.interactable = true;

        img_2.texture = Resources.Load("textures/hero_" + _id) as Texture;

        img_2.enabled = true;

        t_playerName2.text = GlobalControl.heroDateList[_id].m_name;

        GameProperty.HERO2_ID = _id;

        if (img_1.enabled)
        {
            ChangeGuideText(3);
        }
        else
        {
            ChangeGuideText(1);
        }
    }

    private void DeleteImg(int _id,RawImage _img)
    {
        if(_id==1)
        {
            GameProperty.HERO1_ID = -1;
            t_playerName1.text = "";
            ChangeGuideText(1);
        }
        else if(_id==2)
        {
            GameProperty.HERO2_ID = -1;
            t_playerName2.text = "";
            ChangeGuideText(2);
        }

        _img.texture = null;
        _img.enabled = false;
    }
    private void ChangeGuideText(int i)
    {
        switch(i)
        {
            case 1:
                t_guide.text = "Choose the hero of player1";
                btn_deleteImg_1.interactable = true;
                btn_OK.interactable = false;
                break;

            case 2:
                t_guide.text = "Choose the hero of player2";
                btn_deleteImg_2.interactable = true;
                btn_OK.interactable = false;
                break;

            case 3:
                t_guide.text = "Press the [ok] button to return";
                btn_OK.interactable = true;
                break;

        }
    }
}
