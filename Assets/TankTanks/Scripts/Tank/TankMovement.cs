using UnityEngine;
using UnityEngine.UI;


public class TankMovement : MonoBehaviour
{
    public int m_PlayerID = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 6f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 90;            // How fast the tank turns in degrees per second.
    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
    public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.

    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    public Transform m_turret;              // Reference used to move the tank.

    public float m_MovementInputValue;         // The current value of the movement input.
    public float m_TurnInputValue;             // The current value of the turn input.

    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
    private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks
    public PlayerType m_playType;
    public float m_StartingGas;
    public float m_curGas;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        m_turret = GetComponent<TankAni>().m_turret;


    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        m_turret.localEulerAngles = Vector3.zero;

        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        ResetGas();

        // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
        // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
        // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Play();
        }
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
        for (int i = 0; i < m_particleSystems.Length; ++i)
        {
            m_particleSystems[i].Stop();
        }
    }


    private void Start()
    {
        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        EngineAudio();
    }


    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        switch(m_playType)
        {
            case PlayerType.player:
                if(GlobalControl.PLAYERTURN==1)
                {
                    Move();
                    Turn();
                }
                break;
            case PlayerType.player2:
                if (GlobalControl.PLAYERTURN == 2)
                {
                    Move();
                    Turn();
                }
                break;
        }
      
    }


    private void Move()
    {
        if (m_curGas > 0)
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

            if(m_MovementInputValue!=0)
            {
                m_curGas -= 1f;
                AppDelegate.Instance.OnChangeGasUI(m_curGas);
            }
         
        }
    }

    private void ResetGas()
    {
        m_StartingGas =float.Parse( GlobalControl.heroDateList[m_PlayerID].m_gas);
        m_curGas = m_StartingGas;
        AppDelegate.Instance.OnChangeGasUI(m_StartingGas);
    }

    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        if(m_curGas>0)
        {
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        }
        else
        {
            m_turret.localRotation = m_Rigidbody.rotation * turnRotation;
        }
        
    }
}