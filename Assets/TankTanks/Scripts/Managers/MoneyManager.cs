using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager> {

    private MoneyProperty mp=null;
    public void InitMoney()
    {
        if(mp == null) { mp = GameObject.FindObjectOfType<MoneyProperty>(); }

        mp.text_coin.text = GlobalControl.COIN.ToString();
        mp.text_stone.text = GlobalControl.STONE.ToString();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    public void ChangeMoney(RewardType type,int money)
    {

        if(type== RewardType.coin)
        {
            TweenControl.Instance.ScaleFrom(mp.text_coin.transform, Vector3.one*1.3f,0.3f,0,1,DG.Tweening.LoopType.Restart,DG.Tweening.Ease.OutCubic,null);
            GlobalControl.COIN += money;
            PlayerPrefs.SetInt("coin", GlobalControl.COIN);
            mp.text_coin.text = GlobalControl.COIN.ToString();
        }
        else if(type== RewardType.stone)
        {
            TweenControl.Instance.ScaleFrom(mp.text_stone.transform, Vector3.one * 1.3f, 0.3f, 0, 1, DG.Tweening.LoopType.Restart, DG.Tweening.Ease.OutCubic, null);
            GlobalControl.STONE += money;
            PlayerPrefs.SetInt("stone", GlobalControl.STONE);
            mp.text_stone.text = GlobalControl.STONE.ToString();
        }
    }

}
