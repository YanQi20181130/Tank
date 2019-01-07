using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum FinishType
{
    player1Win,
    player2Win,
    winComputer,
    loseComputer,
    loseEndless
}
public class FinishText : Singleton<FinishText>
{
    public string player1Win = "player1 Win the game !";
    public string player2Win = "player2 Win the game !";
    public string winComputer = "you win !";
    public string loseComputer = "you lose of Computer !";
    public string loseEndless = "you lose in the Endless !";
    
}


public class FinishManager : DDOLSingleton<FinishManager>
{
    private GameObject canvasFinish = null;
    private FinishProperty fp;

    public void HeroDeath(PlayerType _pt)
    {
        switch (_pt)
        {
            case PlayerType.player:
                if (GameProperty.STORYTYPE == StoryType.computer)
                {
                    ShowFinish(FinishType.loseComputer);
                }
                else if (GameProperty.STORYTYPE == StoryType.friend)
                {
                    ShowFinish(FinishType.player2Win);
                }
                else if (GameProperty.STORYTYPE == StoryType.tournament)
                {
                    ShowFinish(FinishType.loseEndless);
                }
                break;

            case PlayerType.player2:
                ShowFinish(FinishType.player1Win);

                break;

            case PlayerType.enemy:
                if (GameProperty.STORYTYPE == StoryType.computer)
                {
                    ShowFinish(FinishType.winComputer);

                    RewardManager.Instance.ShowReward(RewardType.coin, 200);

                }
                else if (GameProperty.STORYTYPE == StoryType.tournament)
                {
                    Debug.Log("Finish one enemy in endless mode ! ");
                }

                break;
        }
    }



    private void ShowFinish(FinishType _finishType)
    {
        if (canvasFinish == null)
        {
            canvasFinish = Instantiate(Resources.Load("prefabs/UI/Canvas_Finish") as GameObject);
            fp = canvasFinish.GetComponent<FinishProperty>();
        }
        fp.InitProperty();
        canvasFinish.GetComponent<Canvas>().enabled = true;

        switch (_finishType)
        {
            case FinishType.player1Win:
                fp.text_head.text = FinishText.Instance.player1Win;
                fp.text_reward.text = "200 coins";

                break;

            case FinishType.player2Win:

                fp.text_head.text = FinishText.Instance.player2Win;
                fp.text_reward.text = "200 coins";
                break;

            case FinishType.winComputer:
                fp.text_head.text = FinishText.Instance.winComputer;
                fp.text_reward.text = "200 coins";
                break;

            case FinishType.loseComputer:
                fp.text_head.text = FinishText.Instance.loseComputer;
                fp.text_reward.text = "100 coins";
                fp.btn_resurrection.gameObject.SetActive(true);
                break;

            case FinishType.loseEndless:
                fp.text_head.text = FinishText.Instance.loseEndless;
                fp.text_reward.text = "coins";
                fp.btn_resurrection.gameObject.SetActive(true);
                break;
        }

    }

    private void CloseFinishCanvas()
    {
        canvasFinish.GetComponent<Canvas>().enabled = false;
    }
}