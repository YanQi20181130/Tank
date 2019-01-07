using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishProperty : MonoBehaviour {
    public Text text_head;
    public Text text_reward;
    public Button btn_main;
    public Button btn_resurrection;

    // Use this for initialization
    void Start () {
        EventTriggerListener.Get(btn_main.gameObject).onClick = BtnMethod;
        EventTriggerListener.Get(btn_resurrection.gameObject).onClick = BtnMethod;

    }

    private void BtnMethod(GameObject go)
    {
        if(go==btn_main.gameObject)
        {
            SceneManager.LoadScene(0);
        }
        else if(go==btn_resurrection)
        {
            if (GlobalControl.STONE <10)// 10 stones
            {

                AppDelegate.Instance.OnAlertBool("you don't have enough coins, you can buy in the shop!",
                    MainSceneManager.m_Instance.TriggerShop, true);

                return;
            }
            else
            {
                // cost money
                MoneyManager.Instance.ChangeMoney(RewardType.stone, -10 );

                // resurrection
                AppDelegate.Instance.OnResurrectionEvent();
                GetComponent<Canvas>().enabled = false;
            }
        }
    }

    public void InitProperty()
    {
        text_head.text = "";
        text_reward.text = "";
        btn_resurrection.gameObject.SetActive(false);
    }
}
