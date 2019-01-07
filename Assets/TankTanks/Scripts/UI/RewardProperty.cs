using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardProperty : MonoBehaviour {

    public GameObject btn_openChest;
    public Text reasonText;
    public RawImage img_chest;
    public RawImage unlockEnv_1;
    public RawImage unlockEnv_2;

    public Transform tar_stone;
    public Transform tar_coin;

    public RewardType rewardType;

    public Transform parent_rewardObj;
    // Use this for initialization
    void Start () {
        EventTriggerListener.Get(btn_openChest).onClick = RewardManager.Instance.OpenChestMethod;
	}
	
    public void InitProperty()
    {
        reasonText.text = "";
        img_chest.gameObject.SetActive(false);
        unlockEnv_1.gameObject.SetActive(false);
        unlockEnv_2.gameObject.SetActive(false);

        foreach (Transform item in parent_rewardObj)
        {
            item.gameObject.SetActive(false);
        }
    }
}
