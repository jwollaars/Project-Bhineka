using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Spirit;
    private SpiritStats m_SpiritStats;
    [SerializeField]
    private GameObject m_Body;
    private BodyStats m_BodyStats;

    [SerializeField]
    private Text m_Level;
    [SerializeField]
    private Text m_Experience;

    [SerializeField]
    private Text m_Hunger;
    [SerializeField]
    private Text m_Thirst;
    [SerializeField]
    private Text m_Infection;

    [SerializeField]
    private Text m_Health;

    void Start()
    {
        m_SpiritStats = m_Spirit.GetComponent<SpiritStats>();

        UpdateBodyInfo();
    }
     
    void Update()
    {
        m_Level.text = "Level: " + m_SpiritStats.GetMainStats.level;
        m_Experience.text = "Experience: " + m_SpiritStats.GetMainStats.experience;

        if (m_Body != null)
        {
            m_Hunger.text = "Hunger: " + m_BodyStats.GetMainStats.hunger; 
            m_Thirst.text = "Thirst: " + m_BodyStats.GetMainStats.thirst;
            m_Infection.text = "Infection: " + m_BodyStats.GetMainStats.infection;
            m_Health.text = "Health: " + m_BodyStats.GetMainStats.health; 
        }
        else
        {
            m_Hunger.text = "No physical body";
            m_Thirst.text = "No physical body";
            m_Infection.text = "No physical body";
            m_Health.text = "No physical body"; 
        }
    }

    public void UpdateBodyInfo()
    {
        if (m_Spirit.transform.parent != null)
        {
            m_Body = m_Spirit.transform.parent.gameObject;
            m_BodyStats = m_Body.GetComponent<BodyStats>();
        }
        else
        {
            m_Body = null;
        }
    }
}
