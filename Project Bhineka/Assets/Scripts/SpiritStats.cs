using UnityEngine;
using System.Collections;

public class SpiritStats : MonoBehaviour
{
    private MainStats m_MainStats;
    public MainStats GetMainStats
    {
        get { return m_MainStats; }
    }
    private SubStats m_SubStats;
    public SubStats GetSubStats
    {
        get { return m_SubStats; }
    }

    void Start()
    {
        StartStats();
    }

    private void StartStats()
    {
        m_MainStats.level = 1;
        m_MainStats.experience = 0;
        m_MainStats.levelUpExperience = 100 / 2 * m_MainStats.level;

        m_SubStats.jumpPower = 4;
        m_SubStats.dashPower = 4;
        m_SubStats.levitatePower = 5;
    }

    public struct MainStats
    {
        public int level;
        public int experience;
        public int levelUpExperience;
        public int spiritPower;
    }

    public struct SubStats
    {
        public int stamina;
        public int strength;
        public int defense;

        public int jumpPower;
        public int dashPower;
        public int levitatePower;
    }

    public struct ElementalStats
    {
        public int fire;
        public int electricity;
        public int water;
        public int wind;
        public int earth;
        public int darkness;
        public int light;
        public int fairy;
        public int dragon;
    }
}