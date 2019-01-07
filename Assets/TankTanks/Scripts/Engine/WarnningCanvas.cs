using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class WarnningCanvas : MonoBehaviour {

    public GameObject btn_yes;
    public GameObject btn_no;

    public Text alerText;

    public Action<GameObject> callback;

    public Action<bool> callbackBool;

    private GameObject tempobj;
    private bool tempBool;

    private void OnEnable()
    {
        AppDelegate.alertEvent += Alert;
        AppDelegate.alertBoolEvent += Alert;
    }

    private void OnDisable()
    {
        AppDelegate.alertEvent -= Alert;
        AppDelegate.alertBoolEvent -= Alert;
    }
    // Use this for initialization
    void Start()
    {
        EventTriggerListener.Get(btn_yes).onClick = Btnmethod;
        EventTriggerListener.Get(btn_no).onClick = Btnmethod;
    }

    private void Btnmethod(GameObject go)
    {
        if (go == btn_yes)
        {
            if (callback != null)
            {
                callback(tempobj);
            }
            else if(callbackBool!=null)
            {
                callbackBool(tempBool);
            }
        }
        else if (go == btn_no)
        {
           
        }
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void Alert(string s, Action<GameObject> e, GameObject obj)
    {
        if (e == null)
        {
            btn_no.SetActive(false);
        }
        else
        {
            btn_no.SetActive(true);
        }
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        callback = e;
        tempobj = obj;
        alerText.text = s;

    }

    // Update is called once per frame
    public void Alert(string s, Action<bool> e, bool _bool)
    {
        if (e == null)
        {
            btn_no.SetActive(false);
        }
        else
        {
            btn_no.SetActive(true);
        }
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        callbackBool = e;
        tempBool = _bool;
        alerText.text = s;

    }
}
