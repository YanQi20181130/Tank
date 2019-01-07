using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum RewardType
{
    coin,
    stone,
    unlockEnv_1,
    unlockEnv_2,
    other
}
public class RewardText:Singleton<RewardText>
{
    public string T_coin = "T_coin";
    public string T_stone = "T_stone";
    public string T_unlockEnv = "T_unlockEnv";
    public string T_unlockHero = "T_unlockHero";
    public string T_other = "T_other";
}

public class RewardManager : DDOLSingleton<RewardManager> {
    private GameObject canvasReward=null;
    private RewardProperty rp;
    private int number_reward;
    private float imgMoveToTarTime;
    private float delay_imgMoveToTar=0.2f;

    public void ShowReward(RewardType rewardType,int numberReward)
    {
        if(canvasReward==null)
        {
            canvasReward = Instantiate(Resources.Load("prefabs/UI/Canvas_Reward") as GameObject);
            rp = canvasReward.GetComponent<RewardProperty>();
        }
        rp.InitProperty();
        canvasReward.GetComponent<Canvas>().enabled = true;

        number_reward = numberReward;

        rp.rewardType = rewardType;
        
        switch (rewardType)
        {
            case RewardType.coin:
                rp.img_chest.texture = Resources.Load("chest/" + "chestCoin_0") as Texture;
                
                    break;

            case RewardType.stone:
                rp.img_chest.texture = Resources.Load("chest/" + "chestStone_0") as Texture;
                
                break;

            case RewardType.unlockEnv_1:
                rp.img_chest.texture = Resources.Load("chest/" + "chestEnv1_0") as Texture;
                break;

            case RewardType.unlockEnv_2:
                rp.img_chest.texture = Resources.Load("chest/" + "chestEnv2_0") as Texture;
                break;

            case RewardType.other:

                break;
        }

        rp.img_chest.gameObject.SetActive(true);

        rp.btn_openChest.SetActive(true);
    }

    public void OpenChestMethod(GameObject go)
    {
        go.SetActive(false);
        StartCoroutine( ChestAni());
    }

    private IEnumerator ChestAni()
    {
        Debug.Log("chest animation : "+ rp.rewardType);
        // shake the chest
        TweenControl.Instance.Shake(rp.img_chest.transform, 1.5f, 1, 0, 1, DG.Tweening.LoopType.Restart, DG.Tweening.Ease.OutFlash);
        yield return new WaitForSeconds(1);
        // open the chest

        switch (rp.rewardType)
        {
            case RewardType.coin:
                rp.reasonText.text = RewardText.Instance.T_coin + number_reward;
                rp.img_chest.texture = Resources.Load("chest/" + "chestCoin_1") as Texture;
                
                break;

            case RewardType.stone:
                rp.reasonText.text = RewardText.Instance.T_stone + number_reward;
                rp.img_chest.texture = Resources.Load("chest/" + "chestStone_1") as Texture;
        
                break;

            case RewardType.unlockEnv_1:
                rp.reasonText.text = RewardText.Instance.T_unlockEnv + number_reward;
                rp.img_chest.texture = Resources.Load("chest/" + "chestEnv1_1") as Texture;
                break;

            case RewardType.unlockEnv_2:
                rp.reasonText.text = RewardText.Instance.T_unlockHero + number_reward;
                rp.img_chest.texture = Resources.Load("chest/" + "chestEnv2_1") as Texture;
                break;

            case RewardType.other:
                //rp.reasonText.text = RewardText.Instance.T_other + number_reward;
                //rp.img_chest.texture = Resources.Load("chest/" + "chestOther_1") as Texture;
                break;
        }

        yield return new WaitForSeconds(0.5f);
        // coin\stone\unlock  animation
        RewardObjAni();

        yield return new WaitForSeconds(imgMoveToTarTime);
        CloseChestCanvas();
    }

    private void RewardObjAni()
    {
        int m_childCount = rp.parent_rewardObj.childCount;

        switch (rp.rewardType)
        {
            case RewardType.coin:

                Texture coinT= Resources.Load("chest/" + "coin") as Texture;

                imgMoveToTarTime = m_childCount * delay_imgMoveToTar + 1;

                for (int i = 0; i < m_childCount; i++)
                {
                    ImgMoveToTar(rp.parent_rewardObj.GetChild(i),i,coinT, rp.tar_coin.localPosition, RewardType.coin, number_reward/ m_childCount);
                }
                
                break;

            case RewardType.stone:
    
                Texture stoneT = Resources.Load("chest/" + "stone") as Texture;

                imgMoveToTarTime = m_childCount * delay_imgMoveToTar + 1;

                for (int i = 0; i < m_childCount; i++)
                {
                    ImgMoveToTar(rp.parent_rewardObj.GetChild(i), i, stoneT, rp.tar_stone.localPosition, RewardType.stone, number_reward / m_childCount);
                }
                break;

            case RewardType.unlockEnv_1:

                imgMoveToTarTime = 2;

                rp.unlockEnv_1.gameObject.SetActive(true);


                break;

            case RewardType.unlockEnv_2:

                imgMoveToTarTime = 2;

                rp.unlockEnv_2.gameObject.SetActive(true);
                break;

            case RewardType.other:

                break;
        }
    }

    private void ImgMoveToTar(Transform img,int i,Texture t,Vector3 tar,RewardType type,int money)
    {
        img.GetComponent<RawImage>().texture = t;
        img.gameObject.SetActive(true);
        img.localPosition = Vector3.zero;
        TweenControl.Instance.MoveTo(img, tar, 0.25f, i * delay_imgMoveToTar, 1, LoopType.Restart, Ease.InSine, MoneyManager.Instance.ChangeMoney, type, money);

    }

    private void CloseChestCanvas()
    {
        canvasReward.GetComponent<Canvas>().enabled = false;
    }
}
