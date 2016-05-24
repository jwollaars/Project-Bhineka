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

    void Update()
    {
        CapStat(m_MainStats.level, 0, 100);
        CapStat(m_MainStats.experience, 0, m_MainStats.levelUpExperience);
        CapStat(m_MainStats.spiritPower, 0, 100);

        CapStat(m_SubStats.stamina, 0, 100);
        CapStat(m_SubStats.strength, 0, 100);
        CapStat(m_SubStats.defense, 0, 100);
        CapStat(m_SubStats.jumpPower, 0, 5);
        CapStat(m_SubStats.dashPower, 0, 5);
        CapStat(m_SubStats.levitatePower, 0, 5);
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

    private void IncreaseStat(int stat, int value)
    {
        stat += value;
    }
    private void DecreaseStat(int stat, int value)
    {
        stat -= value;
    }
    private void CapStat(int stat, int min, int max)
    {
        if (stat < min)
        {
            stat = min;
        }
        else if (stat > max)
        {
            stat = max;
        }
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