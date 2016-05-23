using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController))]
public class Player : MonoBehaviour
{
    private InputHandler m_InputHandler;
    private PhysicsController m_PhysicsController;

    private float m_MoveSpeed = 6;
    private float m_AccelerationTimeGrounded = 0.1f;
    private float m_AccelerationTimeAirborne = 0.2f;
    private float m_VelocityXSmoothing;

    [SerializeField]
    private float m_JumpHeigth = 4;
    [SerializeField]
    private float m_timeToJumpApex = 0.4f;
    private float m_JumpVelocity;
    private float m_Gravity;

    private Vector3 m_Velocity;

    void Start()
    {
        m_InputHandler = GetComponent<InputHandler>();
        m_PhysicsController = GetComponent<PhysicsController>();

        m_Gravity = -(2 * m_JumpHeigth) / Mathf.Pow(m_timeToJumpApex, 2);
        m_JumpVelocity = Mathf.Abs(m_Gravity) * m_timeToJumpApex;
    }

    void Update()
    {
        if(m_PhysicsController.m_CollisionInfo.above || m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = 0;
        }

        Vector2 input = new Vector2(m_InputHandler.AxisInput(m_InputHandler.m_InputDir[0], m_InputHandler.m_InputDir[1]), Input.GetAxisRaw("Vertical"));
        
        if(m_InputHandler.m_InputDir[2] && m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = m_JumpVelocity;
        }

        float targetVelocityX = input.x * m_MoveSpeed;
        m_Velocity.x = Mathf.SmoothDamp(m_Velocity.x, targetVelocityX, ref m_VelocityXSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);
        m_Velocity.y += m_Gravity * Time.deltaTime;
        m_PhysicsController.Move(m_Velocity * Time.deltaTime);
    }
}