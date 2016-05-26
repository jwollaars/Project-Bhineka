using UnityEngine;
using System.Collections;

public class IdleState : AIState
{
    private AIBehaviour m_AIBehaviour;
    private float m_Timer = 2f;

    public IdleState(AIBehaviour aiBehaviour)
    {
        m_AIBehaviour = aiBehaviour;
    }

    public void Enter() 
    {
        m_Timer = 2f + Random.Range(0f, 0.5f);
        m_AIBehaviour.m_InputHandler.m_InputDir[0] = true;
    }

    public void Execute()
    {
        m_Timer -= Time.deltaTime;

        if(m_Timer <= 0 && m_AIBehaviour.m_InputHandler.m_InputDir[0])
        {
            m_AIBehaviour.m_InputHandler.m_InputDir[0] = false;
            m_AIBehaviour.m_InputHandler.m_InputDir[1] = true;
            m_Timer = 2f + Random.Range(0f, 0.5f);
        }
        else if(m_Timer <= 0 && m_AIBehaviour.m_InputHandler.m_InputDir[1])
        {
            m_AIBehaviour.m_InputHandler.m_InputDir[0] = true;
            m_AIBehaviour.m_InputHandler.m_InputDir[1] = false;
            m_Timer = 2f + Random.Range(0f, 0.5f);
        }
    }

    public void Exit()
    {
        
    }
}

public class RoamingState : AIState
{
    private AIBehaviour m_AIBehaviour;

    public RoamingState(AIBehaviour aiBehaviour)
    {
        m_AIBehaviour = aiBehaviour;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        
    }
}