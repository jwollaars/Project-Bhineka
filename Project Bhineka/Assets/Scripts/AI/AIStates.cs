using UnityEngine;
using System.Collections;

public class IdleState : AIState
{
    private AIBehaviour m_AIBehaviour;

    public IdleState(AIBehaviour aiBehaviour)
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