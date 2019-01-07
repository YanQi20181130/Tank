using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {
    public static MainSceneManager m_Instance;
    public Canvas c_begin   ;
    public Canvas c_hero    ;
    public Canvas c_help    ;
    public Canvas c_shop    ;
    public Canvas c_warnning;
    public Canvas c_scene   ;
    public Canvas c_me      ;
    public Canvas c_top     ;
    public Canvas c_story   ;

    private List<GameObject> obj_heros=new List<GameObject>();
    private int curHeroID=0;
    public Transform parent_heroUI;
    public Transform parent_envUI;
    public Transform parent_storyUI;

    public Transform parent_heroObjs;

    // Use this for initialization
    private void Awake()
    {
        m_Instance = this;
        Application.targetFrameRate = 60; // MAGIC LINE!!
        for (int i = 0; i < parent_heroObjs.childCount; i++)
        {
            obj_heros.Add(parent_heroObjs.GetChild(i).gameObject);
        }

        GlobalControl.shellPropList = new List<ShellProperty>();
        GlobalControl.heroPropList = new List<HeroProperty>();
    }
    private void OnEnable()
    {
        AppDelegate.changeBGEvent += ChangeHero;
    }
    private void OnDisable()
    {
        AppDelegate.changeBGEvent -= ChangeHero;
    }
    void Start()
    {
        Debug.Log("当前文件路径:" + Application.persistentDataPath);
        c_begin.enabled = false;
        c_hero.enabled = false;
        c_help.enabled = false;
        c_shop.enabled = false;
        c_warnning.enabled = false;
        c_scene.enabled = false;
        c_me.enabled = false;
        c_top.enabled = false;
        c_story.enabled = false;

        //read xml
        DataManager.Instance.InitCoinAndStone();
        DataManager.Instance.InitXmlData();
        DataManager.Instance.InitXmlShell();


        //init heros
        HeroManager.Instance.InitHeros(parent_heroUI);
        EnvManager.Instance.InitEnv(parent_envUI);
        StoryManager.Instance.InitStory(parent_storyUI);
        MoneyManager.Instance.InitMoney();

        StartCoroutine(BeginToShowHero());
    }

    private IEnumerator BeginToShowHero()
    {
        c_begin.enabled = true;
        yield return new WaitForSeconds(0.5f);
        c_begin.enabled = false;
        c_top.enabled = true;
        AppDelegate.Instance.OnChangeBGEvent(0);
        StartCoroutine(IdleRot());
    }

    private IEnumerator IdleRot()
    {
        //AniManager.Instance.PlayAni(obj_heros[curHeroID].GetComponent<TankShooting>().objAni, AniType.idle);
        yield return new WaitForSeconds(2.5f);
        //AniManager.Instance.PlayAni(obj_heros[curHeroID].GetComponent<TankShooting>().objAni, AniType.rot);
        //StartCoroutine(IdleRot());
        
    }

    private void InitGameProperty()
    {
        GameProperty.HERO1_ID = 0;
        GameProperty.ENVID = 0;
        GameProperty.STORYTYPE = StoryType.computer;
        GameProperty.TOURID = 0;
    }
    public void TriggerHelp(bool open)
    {
        c_help.enabled = open;
    }

    public void TriggerShop(bool open)
    {
        c_shop.enabled = open;
    }

    public void TriggerWarnning(bool open)
    {
        c_warnning.enabled = open;
    }

    public void TriggerEnv(bool open)
    {
        c_scene.enabled = open;
    }
    public void CloseEnv()
    {
        c_scene.enabled = false;
   
    }

    public void TriggerMe(bool open)
    {
        c_me.enabled = open;
    }

    public void TriggerHero(bool open,bool openTwoPlayer)
    {
        c_hero.enabled = open;

        if (openTwoPlayer)
        {
            AppDelegate.Instance.OnOpenTwoPlayer(true);
        }
        else
        {
            AppDelegate.Instance.OnOpenTwoPlayer(false);
        }
    }

    public void ChangeHero(int heroID)
    {
        foreach (var item in obj_heros)
        {
            item.SetActive(false);
        }
        obj_heros[heroID].SetActive(true);
        curHeroID = heroID;
        StartCoroutine(IdleRot());
    }

    public void TriggerStory(bool open)
    {
        c_story.enabled = open;
    }


    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Env_"+GameProperty.ENVID);
    }

    #region test
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            RewardManager.Instance.ShowReward(RewardType.coin,100);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RewardManager.Instance.ShowReward(RewardType.stone, 100);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RewardManager.Instance.ShowReward(RewardType.unlockEnv_1, 1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RewardManager.Instance.ShowReward(RewardType.unlockEnv_2, 2);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //DataManager.Instance.UpdateXml("HeroDate","hero_menu");
            //DataManager.Instance.UpdateXml("HeroDate", "env_menu");
            //DataManager.Instance.UpdateXml("HeroDate", "story_menu");
            //DataManager.Instance.UpdateXml("ShellDate", "shell_menu");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    #endregion
}
