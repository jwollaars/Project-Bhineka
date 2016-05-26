using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController), typeof(InputHandler))]
public class CreatureController : MonoBehaviour
{
    private Renderer m_Renderer;
    private InputHandler m_InputHandler;
    private PhysicsController m_PhysicsController;
    private CameraBehaviour m_CameraBehaviour;
    private AIBehaviour m_AIBehaviour;

    private GameObject m_GameManager;
    private CharacterUI m_CharacterUI;

    [SerializeField]
    private Material[] m_Materials;

    [SerializeField]
    private GameObject m_Spirit;
    private SpiritStats m_SpiritStats;

    private float m_MoveSpeed;
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

    public void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_InputHandler = GetComponent<InputHandler>();
        m_PhysicsController = GetComponent<PhysicsController>();
        m_AIBehaviour = GetComponent<AIBehaviour>();
        m_CameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();

        m_GameManager = GameObject.Find("GameManager");
        m_CharacterUI = m_GameManager.GetComponent<CharacterUI>();

        CalculateNormal();
    }

    void Update()
    {
        if (m_InputHandler.PlayerControlled && m_Renderer.material != m_Materials[1])
        {
            m_Renderer.material = m_Materials[1];
        }
        else if (!m_InputHandler.PlayerControlled && m_Renderer.material != m_Materials[0])
        {
            m_Renderer.material = m_Materials[0];
        }

        if (Input.GetKeyDown(KeyCode.E) && m_InputHandler.PlayerControlled)
        {
            Die();
        }

        if (m_PhysicsController.m_CollisionInfo.above || m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = 0;
        }

        Vector2 input = new Vector2(m_InputHandler.GetAxis(m_InputHandler.m_InputDir[0], m_InputHandler.m_InputDir[1]), m_InputHandler.GetAxis(m_InputHandler.m_InputDir[2], m_InputHandler.m_InputDir[3]));

        if (m_InputHandler.m_InputDir[2] && m_PhysicsController.m_CollisionInfo.below)
        {
            m_Velocity.y = m_JumpVelocity;
        }

        float targetVelocityX = input.x * m_MoveSpeed;
        m_Velocity.x = Mathf.SmoothDamp(m_Velocity.x, targetVelocityX, ref m_VelocityXSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);

        m_Velocity.y += m_Gravity * Time.deltaTime;
        m_PhysicsController.Move(m_Velocity * Time.deltaTime);
    }

    public void Die()
    {
        m_Spirit.SetActive(true);
        m_Spirit.transform.position = transform.position + new Vector3(0, 2);
        m_Spirit.transform.parent = null;

        m_CharacterUI.UpdateBodyInfo();

        m_InputHandler.PlayerControlled = false;
        m_InputHandler.ResetControls();

        m_AIBehaviour.ChangeState(m_AIBehaviour.m_CurrentState);

        CalculateNormal();

        m_CameraBehaviour.SetTarget(m_Spirit);
    }
    
    public void SetSpirit(GameObject gameObj)
    {
        m_Spirit = gameObj;
        m_SpiritStats = m_Spirit.GetComponent<SpiritStats>();
    }

    public void CalculateStats()
    {
        m_Gravity = -(2 * m_JumpHeigth) / Mathf.Pow(m_timeToJumpApex, 2);
        m_JumpVelocity = Mathf.Abs(m_Gravity) * m_timeToJumpApex * (m_SpiritStats.GetSubStats.jumpPower / 2);

        m_MoveSpeed = 2 * m_SpiritStats.GetSubStats.dashPower;

        //Debug.Log("Grav: " + m_Gravity);
        //Debug.Log("JumpVel: " + m_JumpVelocity);
        //Debug.Log("MoveSpeed: " + m_MoveSpeed);
    }
    private void CalculateNormal()
    {
        m_Gravity = -(2 * m_JumpHeigth) / Mathf.Pow(m_timeToJumpApex, 2);
        m_JumpVelocity = Mathf.Abs(m_Gravity) * m_timeToJumpApex;

        m_MoveSpeed = 4;
    }
}