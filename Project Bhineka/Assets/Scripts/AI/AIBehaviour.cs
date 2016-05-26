﻿using UnityEngine;
using System.Collections;

public class AIBehaviour : MonoBehaviour
{
    public InputHandler m_InputHandler;
    public CreatureController m_CreatureController;

    public AIState m_CurrentState;

    private IdleState m_IdleState;
    private RoamingState m_RoamingState;

    void Start()
    {
        m_InputHandler = GetComponent<InputHandler>();
        m_CreatureController = GetComponent<CreatureController>();

        m_IdleState = new IdleState(this);
        m_RoamingState = new RoamingState(this);

        m_CurrentState = m_IdleState;
        if (m_CurrentState != null && !m_InputHandler.PlayerControlled)
        {
            m_CurrentState.Enter();
        }
    }

    void Update()
    {
        if(m_CurrentState != null && !m_InputHandler.PlayerControlled)
        {
            m_CurrentState.Execute();
        }
    }

    public void ChangeState(AIState aiState)
    {
        m_CurrentState.Exit();
        m_CurrentState = aiState;
        m_CurrentState.Enter();
        m_CurrentState.Execute();
    }
}

public interface AIState
{
    void Enter();
    void Execute();
    void Exit();
}