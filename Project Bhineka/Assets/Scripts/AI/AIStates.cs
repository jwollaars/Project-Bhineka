using UnityEngine;
using System.Collections;

public class SpiritState : AIState
{
    private AIBehaviour m_AIBehaviour;

    public SpiritState(AIBehaviour aiBehaviour)
    {
        m_AIBehaviour = aiBehaviour;
    }

    public void Enter()
    {
        Debug.Log("SpiritState");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

public class IdleState : AIState
{
    private AIBehaviour m_AIBehaviour;

    private Coroutine m_IdleRoutine;
    private float m_MaxDistance = 3;

    public IdleState(AIBehaviour aiBehaviour)
    {
        m_AIBehaviour = aiBehaviour;
    }

    public void Enter() 
    {
        m_IdleRoutine = m_AIBehaviour.StartCoroutine(IdleRoutine(Random.Range(0.2f, 1f)));
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        m_AIBehaviour.StopCoroutine(m_IdleRoutine);
    }

    private IEnumerator IdleRoutine(float time)
    {
        if (m_AIBehaviour.transform.position.x > m_AIBehaviour.m_NestXPos + m_MaxDistance)
        {
            m_AIBehaviour.m_InputHandler.m_InputDir[0] = true;
        }
        else if (m_AIBehaviour.transform.position.x < m_AIBehaviour.m_NestXPos - m_MaxDistance)
        {
            m_AIBehaviour.m_InputHandler.m_InputDir[1] = true;
        }
        else
        {
            m_AIBehaviour.m_InputHandler.m_InputDir[Random.Range(0, 2)] = true;
        }

        yield return new WaitForSeconds(time / Time.timeScale);
        m_IdleRoutine = m_AIBehaviour.StartCoroutine(WaitRoutine(time));
    }

    private IEnumerator WaitRoutine(float time)
    {
        m_AIBehaviour.m_InputHandler.m_InputDir[0] = false;
        m_AIBehaviour.m_InputHandler.m_InputDir[1] = false;
        yield return new WaitForSeconds(Random.Range(1f, 4f) / Time.timeScale);
        m_IdleRoutine = m_AIBehaviour.StartCoroutine(IdleRoutine(Random.Range(0.2f, 1f)));
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