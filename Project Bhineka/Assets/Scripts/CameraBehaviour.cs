using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;
    public float m_DampTime = 0.15f;
    private Vector3 m_Velocity = Vector3.zero;

    void Start()
    {
        m_Target = GameObject.Find("Spirit");
    }

	void Update()
    {
        if (m_Target.transform)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(m_Target.transform.position);
            Vector3 delta = m_Target.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref m_Velocity, m_DampTime);
        }
    }

    public void SetTarget(GameObject gameObj)
    {
        m_Target = gameObj; 
    }
}