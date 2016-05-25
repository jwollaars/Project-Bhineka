using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour
{
    private GameObject m_PlayerControlled;
    public GameObject PlayerControlled
    {
        get { return m_PlayerControlled; }
        set { m_PlayerControlled = value; }
    }
}
