using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController), typeof(InputHandler))]
public class Player : MonoBehaviour
{
    private InputHandler m_InputHandler;
    private PhysicsController m_PhysicsController;

    [SerializeField]
    private GameObject m_Spirit;

    private float m_MoveSpeed = 6;
    private float m_AccelerationTimeGrounded = 0.1f;
    private float m_AccelerationTimeAirborne = 0.2f;
    private float m_VelocityXSmoothing;
    private float m_VelocityYSmoothing;

    [SerializeField]
    private float m_JumpHeigth = 4;
    [SerializeField]
    private float m_timeToJumpApex = 0.4f;
    private float m_JumpVelocity;
    private float m_Gravity;

    private Vector3 m_Velocity;

    void Start()
    {
        m_Spirit.SetActive(false);

        m_InputHandler = GetComponent<InputHandler>();

        m_PhysicsController = GetComponent<PhysicsController>();

        m_Gravity = -(2 * m_JumpHeigth) / Mathf.Pow(m_timeToJumpApex, 2);
        m_JumpVelocity = Mathf.Abs(m_Gravity) * m_timeToJumpApex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.name != "Spirit" && m_InputHandler.PlayerControlled)
        {
            m_Spirit.SetActive(true);
            m_Spirit.transform.position = transform.position + new Vector3(0, 2);
            m_InputHandler.PlayerControlled = false;
        }

        GameObject[] collisionObj = new GameObject[4];
        collisionObj[0] = m_PhysicsController.m_CollisionInfo.gAbove;
        collisionObj[1] = m_PhysicsController.m_CollisionInfo.gBelow;
        collisionObj[2] = m_PhysicsController.m_CollisionInfo.gLeft;
        collisionObj[3] = m_PhysicsController.m_CollisionInfo.gRight;
        
        for (int i = 0; i < collisionObj.Length; i++)
        {
            if(collisionObj[i] != null && Input.GetKeyDown(KeyCode.E) && gameObject.name == "Spirit")
            {
                m_Spirit.SetActive(false);
                m_Spirit.GetComponent<InputHandler>().ResetControls();
                collisionObj[i].GetComponent<InputHandler>().PlayerControlled = true;
            }
        }

        if (m_PhysicsController.m_CollisionInfo.above || m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = 0;
        }

        Vector2 input = new Vector2(m_InputHandler.GetAxis(m_InputHandler.m_InputDir[0], m_InputHandler.m_InputDir[1]), m_InputHandler.GetAxis(m_InputHandler.m_InputDir[2], m_InputHandler.m_InputDir[3]));
        
        if(m_InputHandler.m_InputDir[2] && m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = m_JumpVelocity;
        }

        float targetVelocityX = input.x * m_MoveSpeed;
        float targetVelocityY = input.y * m_MoveSpeed;

        m_Velocity.x = Mathf.SmoothDamp(m_Velocity.x, targetVelocityX, ref m_VelocityXSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);

        if (gameObject.name != "Spirit")
        {
            m_Velocity.y += m_Gravity * Time.deltaTime;
        }
        else
        {
            m_Velocity.y = Mathf.SmoothDamp(m_Velocity.y, targetVelocityY, ref m_VelocityYSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);
        }
        m_PhysicsController.Move(m_Velocity * Time.deltaTime);
    }
}