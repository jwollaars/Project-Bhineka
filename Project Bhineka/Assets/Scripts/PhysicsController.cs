using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PhysicsController : MonoBehaviour
{
    private float m_SkinWidth = 0.015f;

    public LayerMask m_CollisionMasks;

    [SerializeField]
    private int m_HorizontalRayCount = 4;
    [SerializeField]
    private int m_VerticalRayCount = 4;

    private float m_HorizontalRaySpacing;
    private float m_VerticalRaySpacing;

    private BoxCollider2D m_BoxCollider;
    private RaycastOrigins m_RaycastOrigins;
    public CollisionInfo m_CollisionInfo;

    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_RaycastOrigins = new RaycastOrigins();

        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        m_CollisionInfo.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    private void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + m_SkinWidth;

        for (int i = 0; i < m_HorizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? m_RaycastOrigins.bottomLeft : m_RaycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (m_HorizontalRaySpacing * 1);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, m_CollisionMasks);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - m_SkinWidth) * directionX;
                rayLength = hit.distance;

                m_CollisionInfo.left = directionX == -1;
                m_CollisionInfo.right = directionX == 1;

                if (m_CollisionInfo.left)
                {
                    m_CollisionInfo.gLeft = hit.collider.gameObject;
                }
                if (m_CollisionInfo.right)
                {
                    m_CollisionInfo.gRight = hit.collider.gameObject;
                }
            }
        }
    }

    private void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + m_SkinWidth;

        for (int i = 0; i < m_VerticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? m_RaycastOrigins.bottomLeft : m_RaycastOrigins.topLeft;
            rayOrigin += Vector2.right * (m_VerticalRaySpacing * 1 + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, m_CollisionMasks);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if(hit)
            {
                velocity.y = (hit.distance - m_SkinWidth) * directionY;
                rayLength = hit.distance;

                m_CollisionInfo.below = directionY == -1;
                m_CollisionInfo.above = directionY == 1;

                if(m_CollisionInfo.below)
                {
                    m_CollisionInfo.gBelow = hit.collider.gameObject;
                }
                if (m_CollisionInfo.above)
                {
                    m_CollisionInfo.gAbove = hit.collider.gameObject;
                }
            }
        }
    }

    private void UpdateRaycastOrigins()
    {
        Bounds bounds = m_BoxCollider.bounds;
        bounds.Expand(m_SkinWidth * -2);

        m_RaycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        m_RaycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        m_RaycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        m_RaycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = m_BoxCollider.bounds;
        bounds.Expand(m_SkinWidth * -2);

        m_HorizontalRayCount = Mathf.Clamp(m_HorizontalRayCount, 2, int.MaxValue);
        m_VerticalRayCount = Mathf.Clamp(m_VerticalRayCount, 2, int.MaxValue);

        m_HorizontalRaySpacing = bounds.size.y / (m_HorizontalRayCount - 1);
        m_VerticalRaySpacing = bounds.size.x / (m_VerticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public GameObject gAbove, gBelow;
        public GameObject gLeft, gRight;

        public void Reset()
        {
            above = below = false;
            left = right = false;

            gAbove = gBelow = null;
            gLeft = gRight = null;
        }
    }
}