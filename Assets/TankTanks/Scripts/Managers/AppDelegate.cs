using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AppDelegate :Singleton<AppDelegate>
{

    public delegate void GameStartDelegate();
    public static event GameStartDelegate gameStartEvent;
    public void OnGameStart()
    {
        if (gameStartEvent != null) { gameStartEvent(); }
    }

    public delegate void AlertDelegate(string s, Action<GameObject> e, GameObject obj);
    public static event AlertDelegate alertEvent;
    public void OnAlert(string s, Action<GameObject> e, GameObject obj)
    {
        if (alertEvent != null) { alertEvent(s,e,obj); }
    }

    public delegate void AlertBoolDelegate(string s, Action<bool> e, bool _bool);
    public static event AlertBoolDelegate alertBoolEvent;
    public void OnAlertBool(string s, Action<bool> e, bool _bool)
    {
        if (alertBoolEvent != null) { alertBoolEvent(s, e, _bool); }
    }


    public delegate void ResurrectionDelegate();
    public static event ResurrectionDelegate resurrectionEvent;
    public void OnResurrectionEvent()
    {
        if (resurrectionEvent != null) { resurrectionEvent(); }
    }

    public delegate void CreateNewSceneDelegate();
    public static event CreateNewSceneDelegate createNewSceneEvent;
    public void OncreateNewSceneEvent()
    {
        if (createNewSceneEvent != null) { createNewSceneEvent(); }
    }


    public delegate void LogDelegate(string log,Vector3 pos,float time);
    public static event LogDelegate logEvent;
    public void OnLogEvent(string log, Vector3 pos, float time)
    {
        if (logEvent != null) { logEvent(log, pos, time); }
    }

    public delegate void ChangeBGDelegate(int i);
    public static event ChangeBGDelegate changeBGEvent;
    public void OnChangeBGEvent(int i)
    {
        if (changeBGEvent != null) { changeBGEvent(i); }
    }


    public delegate void ReadyDelegate();
    public static event ReadyDelegate readyEvent;
    public void OnReadyEvent()
    {
        if (readyEvent != null) { readyEvent(); }
    }


    /// <summary>
    /// roport score
    /// </summary>
    public delegate void ReportScoreDelegate(int score,int ID);
    public static event ReportScoreDelegate reportScoreEvent;
    public void OnReportScoreEvent(int score, int ID)
    {
        if (reportScoreEvent != null) { reportScoreEvent(score, ID); }
    }

    /// <summary>
    /// show leaderboard
    /// </summary>
    public delegate void ShowLBDelegate();
    public static event ShowLBDelegate showLBEvent;
    public void OnShowLBEvent()
    {
        if (showLBEvent != null) { showLBEvent(); }
    }

    public delegate void SetHeroNameDelegate(string s);
    public static event SetHeroNameDelegate setHeroNameEvent;
    public void OnSetHeroNameEvent(string s)
    {
        if (setHeroNameEvent != null) { setHeroNameEvent(s); }
    }

    public delegate void SetStoryNameDelegate(string s);
    public static event SetStoryNameDelegate setStoryNameEvent;
    public void OnSetStoryNameEvent(string s)
    {
        if (setStoryNameEvent != null) { setStoryNameEvent(s); }
    }

    public delegate void SetEnvNameDelegate(string s);
    public static event SetEnvNameDelegate setEnvNameEvent;
    public void OnSetEnvNameEvent(string s)
    {
        if (setEnvNameEvent != null) { setEnvNameEvent(s); }
    }

    //---------------------game ui
    public delegate void ChangeGasUIDelegate(float f);
    public static event ChangeGasUIDelegate changeGasUIEvent;
    public void OnChangeGasUI(float f)
    {
        if (changeGasUIEvent != null) { changeGasUIEvent(f); }
    }

    public delegate void ChangeHP0UIDelegate(float f);
    public static event ChangeHP0UIDelegate changeHP0Event;
    public void OnChangeHP0UI(float f)
    {
        if (changeHP0Event != null) { changeHP0Event(f); }
    }

    public delegate void ChangeHP1UIDelegate(float f);
    public static event ChangeHP1UIDelegate changeHP1Event;
    public void OnChangeHP1UI(float f)
    {
        if (changeHP1Event != null) { changeHP1Event(f); }
    }

    public delegate void FireDelegate();
    public static event FireDelegate fireEvent;
    public void OnFire()
    {
        if (fireEvent != null) { fireEvent(); }
    }

    public delegate void ChangeForceDelegate(float f);
    public static event ChangeForceDelegate changeForceEvent;
    public void OnChangeForce(float f)
    {
        if (changeForceEvent != null) { changeForceEvent(f); }
    }

    public delegate void SetJoyDelegate(GameObject go);
    public static event SetJoyDelegate SetJoyEvent;
    public void OnSetJoy(GameObject f)
    {
        if (SetJoyEvent != null) { SetJoyEvent(f); }
    }

    public delegate void GameLoopDelegate();
    public static event GameLoopDelegate GameLoopEvent;
    public void OnChangeGameLoop()
    {
        if (GameLoopEvent != null) { GameLoopEvent(); }
    }

    public delegate void OpenTwoPlayerDele(bool isOpen);
    public static event OpenTwoPlayerDele OpenTwoPlayerEvent;
    public void OnOpenTwoPlayer(bool isOpen)
    {
        if (OpenTwoPlayerEvent != null) { OpenTwoPlayerEvent(isOpen); }
    }

    public delegate void TwoPlayerChangeImgDele(int _id);
    public static event TwoPlayerChangeImgDele TPChangeImgEvent;
    public void OnTPChangeImg(int _id)
    {
        if (TPChangeImgEvent != null) { TPChangeImgEvent(_id); }
    }


    public delegate void ChangeShellDele(int _id);
    public static event ChangeShellDele ChangeShellEvent;
    public void OnChangeShell(int _id)
    {
        if (ChangeShellEvent != null) { ChangeShellEvent(_id); }
    }

    public delegate void DeleteShellDele(PlayerType pt, int _id);
    public static event DeleteShellDele DeleteShellEvent;
    public void OnDeleteShell(PlayerType pt, int _id)
    {
        if (DeleteShellEvent != null) { DeleteShellEvent(pt , _id); }
    }

    public delegate void ActiveAIDele(Transform target);
    public static event ActiveAIDele ActiveAIEvent;
    public void OnActiveAI(Transform target)
    {
        if (ActiveAIEvent != null) { ActiveAIEvent(target); }
    }

    public delegate void HitPaintMeshDele(MeshCollider meshCollider,Vector3 point);
    public static event HitPaintMeshDele hitPaintMeshDeleEvent;
    public void OnHitPaintMesh(MeshCollider meshCollider,Vector3 point)
    {
        if (hitPaintMeshDeleEvent != null) { hitPaintMeshDeleEvent(meshCollider,point); }
    }
}
