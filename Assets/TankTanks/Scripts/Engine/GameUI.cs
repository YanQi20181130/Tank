
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Button btn_fire;
    public Button btn_shell;
    public Slider slider_gas;
    public Slider slider_force;// shell distance, max dis - min dis 
    public Slider slider_hp_0;
    public Slider slider_hp_1;

    public JoyStick m_Joystick;

    public GameObject window_shellList;

    public Transform parent_shellList1;
    public Transform parent_shellList2;

    public List<ShellProperty> shellList1 = new List<ShellProperty>();
    public List<ShellProperty> shellList2 = new List<ShellProperty>();

    // Use this for initialization
    void Awake()
    {
        EventTriggerListener.Get(btn_fire.gameObject).onClick = BtnMethod;
        EventTriggerListener.Get(btn_shell.gameObject).onClick = BtnMethod;
    }
    public void InitGameUIStart()
    {
        int playerID_0;
        int playerID_1;

        playerID_0 = GameProperty.HERO1_ID;
        playerID_1 = GameProperty.HERO2_ID;

        slider_gas.maxValue = float.Parse(GlobalControl.heroDateList[playerID_0].m_gas);
        slider_gas.value = slider_gas.maxValue;

        slider_hp_0.maxValue = float.Parse(GlobalControl.heroDateList[playerID_0].m_hp);
        slider_hp_0.value = slider_hp_0.maxValue;

        slider_hp_1.maxValue = float.Parse(GlobalControl.heroDateList[playerID_1].m_hp);
        slider_hp_1.value = slider_hp_1.maxValue;

        InitShell(parent_shellList1, playerID_0, shellList1);
        if (GameProperty.STORYTYPE == StoryType.friend)
        {
            InitShell(parent_shellList2, playerID_1, shellList2);
        }

        OnChangeShell(int.Parse(shellList1[0].m_id));

        window_shellList.SetActive(false);
        parent_shellList1.gameObject.SetActive(false);
        parent_shellList2.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        AppDelegate.changeGasUIEvent += OnGasChange;
        AppDelegate.changeHP0Event += OnHP0Change;
        AppDelegate.changeHP1Event += OnHP1Change;
        AppDelegate.SetJoyEvent += OnSetJoyPlayer;
        AppDelegate.ChangeShellEvent += OnChangeShell;
        AppDelegate.DeleteShellEvent += DeleteOneShell;
    }

    private void OnDisable()
    {
        AppDelegate.changeGasUIEvent -= OnGasChange;
        AppDelegate.changeHP0Event -= OnHP0Change;
        AppDelegate.changeHP1Event -= OnHP1Change;
        AppDelegate.SetJoyEvent -= OnSetJoyPlayer;
        AppDelegate.ChangeShellEvent -= OnChangeShell;
        AppDelegate.DeleteShellEvent -= DeleteOneShell;
    }

    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="go"></param>
    private void OnSetJoyPlayer(GameObject go)
    {
        m_Joystick.SetPlayerMovement(go);
    }

    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="curGas"></param>
    private void OnGasChange(float curGas)
    {
        slider_gas.value = curGas;
    }

    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="f"></param>
    private void OnHP0Change(float f)
    {
        slider_hp_0.value = f;
    }

    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="f"></param>
    private void OnHP1Change(float f)
    {
        slider_hp_1.value = f;
    }

    /// <summary>
    ///  UI Slider control this function
    /// </summary>
    public void OnForceSliderChange()
    {

        AppDelegate.Instance.OnChangeForce(float.Parse(GlobalControl.shellDateList[GameManager.selectedShellID].m_minDis) + slider_force.value);

    }

    /// <summary>
    /// delegate
    /// </summary>
    /// <param name="_shellID"></param>
    public void OnChangeShell(int _shellID)
    {
        //btn_shell.image
        slider_force.maxValue = float.Parse(GlobalControl.shellDateList[_shellID].m_maxDis) - float.Parse(GlobalControl.shellDateList[_shellID].m_minDis);
        slider_force.value = 0;

        loadSprite(btn_shell.image, _shellID.ToString());

        GameManager.selectedShellID = _shellID;
    }

    public void ToggleControl(bool ison)
    {
        btn_fire.gameObject.SetActive(ison);
        btn_shell.gameObject.SetActive(ison);
        slider_gas.gameObject.SetActive(ison);
        slider_force.gameObject.SetActive(ison);
        m_Joystick.gameObject.SetActive(ison);
    }

    private void BtnMethod(GameObject go)
    {
        if (go == btn_fire.gameObject)
        {
            AppDelegate.Instance.OnFire();
            ToggleControl(false);
        }
        else if (go == btn_shell.gameObject)
        {
            Debug.Log("open shell list");
            window_shellList.SetActive(true);
            if (GlobalControl.PLAYERTURN == 1)
            {
                parent_shellList1.gameObject.SetActive(true);
                parent_shellList2.gameObject.SetActive(false);
            }
            else
            {
                parent_shellList1.gameObject.SetActive(false);
                parent_shellList2.gameObject.SetActive(true);
            }
        }
        else if (go.name.Contains("shell"))
        {
            OnChangeShell(int.Parse(go.GetComponent<ShellProperty>().m_id));
            window_shellList.SetActive(false);
        }
    }

    // -------------------------------------------------------------- shell
    private int shellCount = 0;

    public void InitShell(Transform parent_shellUI, int heroID, List<ShellProperty> _spList)
    {
        shellCount = heroID * 6;
        //添加UI到scene中
        for (int i = 0; i < 6; i++)
        {
            GameObject ui_shell;

            if (parent_shellUI.Find("shell_" + shellCount) != null)
            {
                ui_shell = parent_shellUI.Find("shell_" + shellCount).gameObject;
                ui_shell.SetActive(true);
            }
            else
            {
                ui_shell = Instantiate(Resources.Load("prefabs/UI/shell_") as GameObject);
            }

            ui_shell.name = "shell_" + shellCount;

            ui_shell.transform.SetParent(parent_shellUI, false);
            ShellProperty shellprop = ui_shell.GetComponent<ShellProperty>();
            shellprop.m_id = shellCount.ToString();

            shellprop.m_name.text = GlobalControl.shellDateList[shellCount].m_name;
            shellprop.m_level = GlobalControl.shellDateList[shellCount].m_level;

            int curLevel = int.Parse(shellprop.m_level);

            // star based by level
            for (int istar = 0; istar < shellprop.parent_levelStar.childCount; istar++)
            {
                if (istar < curLevel)
                {
                    shellprop.parent_levelStar.GetChild(istar).gameObject.SetActive(true);
                }
                else
                {
                    shellprop.parent_levelStar.GetChild(istar).gameObject.SetActive(false);
                }
            }

            // price

            shellprop.m_level_Price.text = "";

            // damage
            shellprop.m_damage = float.Parse(GlobalControl.shellDateList[shellCount].m_damage.Split('_')[curLevel]);

            // min distance
            shellprop.m_minDis = float.Parse(GlobalControl.shellDateList[shellCount].m_minDis);

            // max distance
            shellprop.m_maxDis = float.Parse(GlobalControl.shellDateList[shellCount].m_maxDis);

            // damage range
            shellprop.m_damageRange = float.Parse(GlobalControl.shellDateList[shellCount].m_damageRange);

            // experienceValue;
            shellprop.m_experienceValue = GlobalControl.shellDateList[shellCount].m_experienceValue;


            shellprop.text_levelSlider.gameObject.SetActive(false);
            shellprop.m_levelSlider.gameObject.SetActive(false);

            //判断是否可以升级
            shellprop.btn_update.SetActive(false);

            // image
            loadSprite(ui_shell.GetComponent<Image>(), shellprop.m_id);

            // btn
            EventTriggerListener.Get(ui_shell).onClick = BtnMethod;

            _spList.Add(shellprop);

            shellCount++;
        }

    }

    private void loadSprite(Image icon_shell, string id)
    {
        Texture2D m_Tex = Resources.Load("textures/shell_" + id) as Texture2D;

        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(0, 0));

        icon_shell.sprite = tempSprite;
    }

    private void DeleteOneShell(PlayerType _playerType, int _id)
    {
        if (_playerType == PlayerType.player)
        {
            foreach (var item in shellList1)
            {
                if (item.m_id == _id.ToString())
                {
                    item.gameObject.SetActive(false);
                    shellList1.Remove(item);
                    if (shellList1.Count == 0)
                    {

                        InitShell(parent_shellList1, GameProperty.HERO1_ID, shellList1);

                        OnChangeShell(int.Parse(shellList1[0].m_id));
                    }
                    break;
                }
            }
        }
        else
        {
            foreach (var item in shellList2)
            {
                if (item.m_id == _id.ToString())
                {
                    item.gameObject.SetActive(false);
                    shellList2.Remove(item);
                    if (shellList2.Count == 0)
                    {

                        InitShell(parent_shellList2, GameProperty.HERO2_ID, shellList2);

                        OnChangeShell(int.Parse(shellList2[0].m_id));
                    }
                    break;
                }
            }
        }

    }
}