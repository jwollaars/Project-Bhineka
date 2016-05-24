using UnityEngine;
using System.Collections;

public class BodyStats : MonoBehaviour
{
    private MainStats m_MainStats;
    public MainStats GetMainStats
    {
        get { return m_MainStats; }
    }

    void Start()
    {
        StartStats();
    }

    void Update()
    {
        CapStat(m_MainStats.health, 0, 100);
        CapStat(m_MainStats.conciousness, 0, 100);
        CapStat(m_MainStats.hunger, 0, 100);
        CapStat(m_MainStats.thirst, 0, 100);
        CapStat(m_MainStats.infection, 0, 100);
        CapStat(m_MainStats.attackPower, 0, 100);
        CapStat(m_MainStats.defensePower, 0, 100);
    }

    private void StartStats()
    {
        m_MainStats.health = 100;
        m_MainStats.conciousness = 0;

        m_MainStats.hunger = 0;
        m_MainStats.thirst = 0;
        m_MainStats.infection = 0;

        m_MainStats.attackPower = 1;
        m_MainStats.defensePower = 1;
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
        if(stat < min)
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
        public int health;
        public int conciousness;

        public int hunger;
        public int thirst;
        public int infection;

        public int attackPower;
        public int defensePower;
    }
}
