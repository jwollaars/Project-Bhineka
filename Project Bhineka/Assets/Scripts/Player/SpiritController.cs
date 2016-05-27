using UnityEngine;
using System.Collections;

public class SpiritController : MonoBehaviour
{
    private Renderer m_Renderer;
    private InputHandler m_InputHandler;
    private PhysicsController m_PhysicsController;
    private CameraBehaviour m_CameraBehaviour;

    private GameObject m_GameManager;
    private CharacterUI m_CharacterUI;

    private float m_MoveSpeed;
    private float m_AccelerationTimeGrounded = 0.1f;
    private float m_AccelerationTimeAirborne = 0.2f;
    private float m_VelocityXSmoothing;
    private float m_VelocityYSmoothing;

    [SerializeField]
    private float m_JumpHeigth = 0;
    [SerializeField]
    private float m_timeToJumpApex = 0.0f;
    private float m_JumpVelocity;
    private float m_Gravity;

    private Vector3 m_Velocity;

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_InputHandler = GetComponent<InputHandler>();
        m_PhysicsController = GetComponent<PhysicsController>();
        m_CameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();

        m_GameManager = GameObject.Find("GameManager");
        m_CharacterUI = m_GameManager.GetComponent<CharacterUI>();

        CalculateBasicMovement();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] collisionObj = new GameObject[4];
        collisionObj[0] = m_PhysicsController.m_CollisionInfo.gAbove;
        collisionObj[1] = m_PhysicsController.m_CollisionInfo.gBelow;
        collisionObj[2] = m_PhysicsController.m_CollisionInfo.gLeft;
        collisionObj[3] = m_PhysicsController.m_CollisionInfo.gRight;

        for (int i = 0; i < collisionObj.Length; i++)
        {
            if (collisionObj[i] != null && Input.GetKeyDown(KeyCode.E))
            {
                EnterCreature(collisionObj[i]);
            }
        }

        Vector2 input = new Vector2(m_InputHandler.GetAxis(m_InputHandler.m_InputDir[0], m_InputHandler.m_InputDir[1]), m_InputHandler.GetAxis(m_InputHandler.m_InputDir[2], m_InputHandler.m_InputDir[3]));
        float targetVelocityX = input.x * m_MoveSpeed;
        float targetVelocityY = input.y * m_MoveSpeed;
        m_Velocity.x = Mathf.SmoothDamp(m_Velocity.x, targetVelocityX, ref m_VelocityXSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);
        m_Velocity.y = Mathf.SmoothDamp(m_Velocity.y, targetVelocityY, ref m_VelocityYSmoothing, (m_PhysicsController.m_CollisionInfo.below) ? m_AccelerationTimeGrounded : m_AccelerationTimeAirborne);
        m_PhysicsController.Move(m_Velocity * Time.deltaTime);
    }

    public void EnterCreature(GameObject creature)
    {
        m_InputHandler.ResetControls(); 
        gameObject.transform.SetParent(creature.transform);
        gameObject.SetActive(false);
        CreatureController cc = creature.GetComponent<CreatureController>();
        InputHandler ih = creature.GetComponent<InputHandler>();
        AIBehaviour ai = creature.GetComponent<AIBehaviour>();

        cc.SetSpirit(gameObject);
        cc.CalculateStats();
        ih.PlayerControlled = true;
        ai.ChangeState(ai.GetSpiritState);

        m_CharacterUI.UpdateBodyInfo();

    }

    private void CalculateBasicMovement()
    {
        m_Gravity = -(2 * m_JumpHeigth) / Mathf.Pow(m_timeToJumpApex, 2);
        m_JumpVelocity = Mathf.Abs(m_Gravity) * m_timeToJumpApex;

        m_MoveSpeed = 4;
    }
}
