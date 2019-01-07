using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAni : MonoBehaviour {

    public int m_PlayerID = 1; // Used to identify the different players.
    public Transform m_turret;// for rotate      
    public Transform m_gun0; // for shoot
    public Transform m_gun1; // for shoot
    public Transform m_body; // for idle
    public PlayerType m_playType;

    private void OnEnable()
    {
        AniIdle();
    }
    private void AniIdle()
    {

    }

    private void AniGun0()
    {

    }

    private void AniGun1()
    {

    }

    private void AniTurret()
    {

    }
}
