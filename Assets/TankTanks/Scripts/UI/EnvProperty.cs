using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvProperty : MonoBehaviour {

    public string m_id;
    public Text m_name;
    public Text m_level;
    public Text m_price;
    public string m_unlocked;
    public Image img_lock;
    public GameObject obj_price;
    public GameObject m_tournament;
    public string m_tournamentLock;
    public GameObject m_toggle;

    private void Start()
    {
        EventTriggerListener.Get(gameObject).onClick = BtnMethod;

    }

    private void BtnMethod(GameObject go)
    {
        EnvManager.Instance.SetEnv(m_id);
    }
}
