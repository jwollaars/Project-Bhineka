using UnityEngine;
using System.Collections;

public class BodyStats : MonoBehaviour
{
    private MainStats m_MainStats;
    public MainStats GetMainStats
    {
        get { return m_MainStats; }
    }

    [SerializeField]
    private int test;
    [SerializeField]
    private float[] timer;

    void Start()
    {
        timer = new float[6];
        StartStats();
    }

    void Update()
    {
        //Make sure stat is never lower than min or heigher than max
        CapStat(ref m_MainStats.health, 0, 100);
        CapStat(ref m_MainStats.conciousness, 0, 100);
        CapStat(ref m_MainStats.hunger, 0, 100);
        CapStat(ref m_MainStats.thirst, 0, 100);
        CapStat(ref m_MainStats.infection, 0, 100);
        CapStat(ref m_MainStats.attackPower, 0, 100);
        CapStat(ref m_MainStats.defensePower, 0, 100);

        //Control hunger and thirst
        ConditionControl(ref m_MainStats.hunger, 1, 60, 0);
        ConditionControl(ref m_MainStats.thirst, 1, 45, 1);

        //Decrease health by hunger, thirst or infection
        BadCondition(ref m_MainStats.health, m_MainStats.hunger, 90, 2); 
        BadCondition(ref m_MainStats.health, m_MainStats.thirst, 80, 3); 
        BadCondition(ref m_MainStats.health, m_MainStats.infection, 30, 4); 
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

    private void IncreaseStat(ref int stat, int value)
    {
        stat += value;
    }
    private void DecreaseStat(ref int stat, int value)
    {
        stat -= value;
    }

    private void CapStat(ref int stat, int min, int max)
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

    private void ConditionControl(ref int stat, int value, float time, int timerIndex)
    {
        if (timer[timerIndex] <= time)
        {
            timer[timerIndex] += Time.deltaTime;
        }
        else
        {
            IncreaseStat(ref stat, 1);
            timer[timerIndex] = 0;
        }
    }
    private void BadCondition(ref int stat, int checkStat, int badCondition, int timerIndex)
    {
        if (checkStat >= badCondition)
        {
            if (timer[timerIndex] <= 5.0f)
            {
                timer[timerIndex] += Time.deltaTime;
            }
            else
            {
                DecreaseStat(ref stat, 2);
                timer[timerIndex] = 0;
            }
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
