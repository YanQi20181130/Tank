using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TankShooting : BulletControl
{
    public int m_PlayerID = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    //public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    //public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
    //public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


    //private string m_FireButton;                // The input axis that is used for launching shells.
    public float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    //private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    //private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
    public PlayerType m_playType;
    //------------------------------- shell list-----------------

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI

        AppDelegate.fireEvent += Fire;
        AppDelegate.changeForceEvent += OnchangeForce;
        AppDelegate.ChangeShellEvent += OnChangeShell;
    }

    private void OnDisable()
    {

        AppDelegate.fireEvent -= Fire;
        AppDelegate.changeForceEvent -= OnchangeForce;
        AppDelegate.ChangeShellEvent -= OnChangeShell;

    }

    private void OnchangeForce(float f)
    {
        m_CurrentLaunchForce = f;
        m_AimSlider.value = f;
    }

    private void OnChangeShell(int id)
    {
        m_CurrentLaunchForce = float.Parse(GlobalControl.shellDateList[id].m_minDis);
        m_AimSlider.minValue = 0;
        m_AimSlider.maxValue = 30;
        m_AimSlider.value = m_CurrentLaunchForce;
    }

    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        //m_Fired = true;
        float lunchTime = 4;
        int easeType = 1;
        int pathNum = 2;
        int bulletCount = 1;
        foreach (var item in GlobalControl.shellDateList)
        {
            if (item.m_id == GameManager.selectedShellID.ToString())
            {
                lunchTime = item.m_lunchTime;
                easeType = item.m_easeType;
                pathNum = item.m_pathNum;
                bulletCount = item.m_bulletCount;
                break;
            }
        }
        LunchBullet( m_FireTransform, GameManager.selectedShellID, lunchTime, (Ease)easeType, m_CurrentLaunchForce, pathNum, bulletCount);

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // delete this shell 
        AppDelegate.Instance.OnDeleteShell(m_playType, GameManager.selectedShellID);
    }
}

/*
private void Update()
{
    switch (m_playType)
    {
        case PlayerType.player:
            if (GlobalControl.PLAYERTURN == 1)
            {
                FireUpdate();
            }
            break;
        case PlayerType.enemy:
            if (GlobalControl.PLAYERTURN == 2)
            {
                FireUpdate();
            }
            break;
    }

}

private void FireUpdate()
{

    // The slider should have a default value of the minimum launch force.
    m_AimSlider.value = m_MinLaunchForce;

    // If the max force has been exceeded and the shell hasn't yet been launched...
    if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
    {
        // ... use the max force and launch the shell.
        m_CurrentLaunchForce = m_MaxLaunchForce;
        Fire();
    }
    // Otherwise, if the fire button has just started being pressed...
    else if (Input.GetButtonDown(m_FireButton))
    {
        // ... reset the fired flag and reset the launch force.
        m_Fired = false;
        m_CurrentLaunchForce = m_MinLaunchForce;

        // Change the clip to the charging clip and start it playing.
        m_ShootingAudio.clip = m_ChargingClip;
        m_ShootingAudio.Play();
    }
    // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
    else if (Input.GetButton(m_FireButton) && !m_Fired)
    {
        // Increment the launch force and update the slider.
        m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

        m_AimSlider.value = m_CurrentLaunchForce;
    }
    // Otherwise, if the fire button is released and the shell hasn't been launched yet...
    else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
    {
        // ... launch the shell.
        Fire();
    }
}
*/
