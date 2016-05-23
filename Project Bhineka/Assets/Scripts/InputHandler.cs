using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private bool m_PlayerControlled;
    public bool PlayerControlled
    {
        get { return m_PlayerControlled; }
        set { m_PlayerControlled = value; }
    }

    [SerializeField]
    private KeyCode[] m_KeyCode;
    [SerializeField]
    public bool[] m_InputDir;

    void Start()
    {
        m_InputDir = new bool[m_KeyCode.Length];
    }

    void Update()
    {
        if (m_PlayerControlled)
        {
            PlayerControls();
        }
    }

    private void PlayerControls()
    {
        for (int i = 0; i < m_KeyCode.Length; i++)
        {
            if (Input.GetKeyDown(m_KeyCode[i]))
            {
                m_InputDir[i] = true;
            }
            else if (Input.GetKeyUp(m_KeyCode[i]))
            {
                m_InputDir[i] = false;
            }
        }
    }

    public void ResetControls()
    {
        for (int i = 0; i < m_KeyCode.Length; i++)
        {
            m_InputDir[i] = false;
        }
    }

    public float GetAxis(bool minus, bool plus)
    {
        if(minus)
        {
            return -1f;
        }

        if(plus)
        {
            return 1f;
        }

        return 0;
    }
}