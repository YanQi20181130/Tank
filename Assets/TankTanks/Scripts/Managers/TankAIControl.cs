using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAIControl : MonoBehaviour {


   private NavMeshAgent agent;
       [SerializeField] private bool activeAI;                        // whether the AI is currently actively driving or stopped.
    [SerializeField] private Transform m_Target;                    // 'target' the target object to aim for.
    [SerializeField] private float m_ReachTargetThreshold = 2;      // proximity to target to consider we 'reached' it, and stop driving.
    [SerializeField] private float m_AimAngleTargetThreshold = 3;      // proximity to target to consider we 'reached' it, and stop driving.
    [SerializeField] private float m_AimForceThreshold = 3;      // proximity to target to consider we 'reached' it, and stop driving.
    private bool m_ReachTarget=false;
    private bool m_AimedTarget=false;
    private Quaternion lookRotation { set; get; }

    private TankMovement m_movement;    // Reference to actual car controller we are controlling
    private Rigidbody m_Rigidbody;
    private Transform m_transform;
    private int playerID;
    private int shellStartNum;
    private int curShellNum;

    private void Awake()
    {
        activeAI = false;
        m_transform = transform;
        // get the car controller reference
        m_movement = GetComponent<TankMovement>();
        playerID = m_movement.m_PlayerID;

        shellStartNum = 6 * playerID;
        curShellNum = shellStartNum;

        m_Rigidbody = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        if(agent==null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
    }
    private void OnEnable()
    {
        AppDelegate.ActiveAIEvent += ActiveAI;
    }
    private void OnDisable()
    {
        AppDelegate.ActiveAIEvent -= ActiveAI;
    }
    private void FixedUpdate()
    {
        if (m_Target == null || !activeAI)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        else
        {
            if(!m_ReachTarget)
            {
                AIMove();
            }
            

            if (m_ReachTarget && !m_AimedTarget)
            {
                AIRotate();
            }

            if(m_ReachTarget && m_AimedTarget)
            {

                AIAimForce();

                AIFire();
            }
            //Debug.Log("m_ReachTarget= " + m_ReachTarget + " ,m_AimedTarget = " + m_AimedTarget);
        }
    }

    private void ActiveAI(Transform target)
    {
        m_Target = target;

        agent.Warp(transform.position);

        agent.SetDestination(m_Target.position);

        AISelectShell();

        m_ReachTarget = false;

        m_AimedTarget = false;

        activeAI = true;
       
    }

    private void AISelectShell()
    {
        GameManager.selectedShellID = curShellNum;
    }

    private void AIMove()
    {
        float dis_shell_max =float.Parse( GlobalControl.shellDateList[GameManager.selectedShellID].m_maxDis);
        float dis_shell_min =float.Parse( GlobalControl.shellDateList[GameManager.selectedShellID].m_minDis);

        float dis_hero = Vector3.Distance(m_transform.position, m_Target.position);
        
        if(dis_hero> dis_shell_max) // too far
        {
           // Debug.Log("too far" + " ,dis_hero = " + dis_hero + " ,dis_shell_max = " + dis_shell_max);
            if (Mathf.Abs(dis_shell_max - dis_hero) > m_ReachTargetThreshold)
            {
               
                agent.isStopped = false;
                agent.destination = m_Target.position;

                m_ReachTarget = false;
                m_AimedTarget = false;
                // gas
                if (m_movement.m_curGas > 0)
                {
                    m_movement.m_curGas -= 1f;
                }
                else
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    m_ReachTarget = true;
                }
            }
            else
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                m_ReachTarget = true;
            }
        }
        else if(dis_hero< dis_shell_min) // too near
        {
            
            Vector3 tempTarPos = new Vector3( (m_Target.position.x+15) * -1 , m_Target.position.y, (m_Target.position.z +15) * -1 );

            Debug.Log("too near! dis = " + dis_hero);

            if (Mathf.Abs(dis_shell_min - dis_hero) > m_ReachTargetThreshold)
            {
                agent.isStopped = false;
             
                agent.destination = tempTarPos;

                m_ReachTarget = false;
                m_AimedTarget = false;

                // gas
                if (m_movement.m_curGas > 0)
                {
                    m_movement.m_curGas -= 1f;
                }
                else
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    m_ReachTarget = true;
                }
            }
            else
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                m_ReachTarget = true;
            }
        }


    }

    private void AIRotate()
    {
        lookRotation = Quaternion.LookRotation(m_Target.position - m_transform.position);
        if (Mathf.Abs( Quaternion.Angle(m_transform.rotation, lookRotation)) >= m_AimAngleTargetThreshold)
        {
            m_AimedTarget = false;
            m_transform.rotation = Quaternion.RotateTowards(m_transform.rotation, lookRotation, 100 * Time.deltaTime);
        }
        else
        {
            m_AimedTarget = true;
        }
    }



    private void AIAimForce()
    {
        float dis_shell_max = float.Parse(GlobalControl.shellDateList[GameManager.selectedShellID].m_maxDis);
        float dis_shell_min = float.Parse(GlobalControl.shellDateList[GameManager.selectedShellID].m_minDis);

        float dis_hero = Vector3.Distance(m_transform.position, m_Target.position);

        if (dis_hero > dis_shell_max) // too far
        {
            AppDelegate.Instance.OnChangeForce(float.Parse(GlobalControl.shellDateList[GameManager.selectedShellID].m_maxDis));
        }
        else if(dis_hero < dis_shell_min)
        {
            AppDelegate.Instance.OnChangeForce(float.Parse(GlobalControl.shellDateList[GameManager.selectedShellID].m_minDis));
        }
        else
        {
            AppDelegate.Instance.OnChangeForce(dis_hero+Random.Range(-m_AimForceThreshold, m_AimForceThreshold));
        }
    }

    private void AIFire()
    {
        Debug.Log(" AI fire ! ");

        AppDelegate.Instance.OnFire();

        activeAI = false;

        curShellNum++;

        if(curShellNum - shellStartNum>=6)
        {
            curShellNum = shellStartNum;
        }
    }

}
