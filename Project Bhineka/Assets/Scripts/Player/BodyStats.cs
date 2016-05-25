using UnityEngine;
using System.Collections;

public class BodyStats : MonoBehaviour
{
    private MainStats m_MainStats;
    public MainStats GetMainStats
    {
        get { return m_MainStats; }
    }

    private Player m_Player;

    [SerializeField]
    private int test;
    [SerializeField]
    private float[] timer;

    void Start()
    {
        StartStats();

        //Control hunger and thirst
        Coroutine hungerRoutine = StartCoroutine(HungerControl(20f));
        Coroutine thirstRoutine = StartCoroutine(ThirstControl(10f));

        Coroutine badConditionControl = StartCoroutine(BadConditionControl(2f));
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

        AliveCheck();

        //Control hunger and thirst
        //ConditionControl(ref m_MainStats.hunger, 1, 20, 0);
        //ConditionControl(ref m_MainStats.thirst, 1, 10, 1);

        //Decrease health by hunger, thirst or infection
        //BadCondition(ref m_MainStats.health, m_MainStats.hunger, 90, 2); 
        //BadCondition(ref m_MainStats.health, m_MainStats.thirst, 80, 3); 
        //BadCondition(ref m_MainStats.health, m_MainStats.infection, 30, 4); 
    }

    private void StartStats()
    {
        m_MainStats.health = 5;
        m_MainStats.conciousness = 0;

        m_MainStats.hunger = 59;
        m_MainStats.thirst = 59;
        m_MainStats.infection = 0;

        m_MainStats.attackPower = 1;
        m_MainStats.defensePower = 1;
    }

    private void IncreaseStat(ref int stat, int value, int min, int max)
    {
        if (stat >= min && stat <= max)
        {
            stat += value;
        }
    }
    private void DecreaseStat(ref int stat, int value, int min, int max)
    {
        if (stat >= min && stat <= max)
        {
            stat -= value;
        }
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

    private void BadCondition(ref int stat, int checkStat, int badCondition)
    {
        if (checkStat >= badCondition)
        {
            DecreaseStat(ref stat, 2, 0, 100);
        }
    }
    private void AliveCheck()
    {
        if(m_MainStats.health <= 0)
        {
            if (transform.parent != null)
            {
                m_Player = transform.parent.gameObject.GetComponent<Player>();
            }
        }
    }

    private IEnumerator HungerControl(float time)
    {
        IncreaseStat(ref m_MainStats.hunger, 1, 0, 100);
        yield return new WaitForSeconds(time / Time.timeScale);
        StartCoroutine(HungerControl(time));
    }
    private IEnumerator ThirstControl(float time)
    {
        IncreaseStat(ref m_MainStats.thirst, 1, 0, 100);
        yield return new WaitForSeconds(time / Time.timeScale);
        StartCoroutine(ThirstControl(time));
    }
    private IEnumerator BadConditionControl(float time)
    {
        BadCondition(ref m_MainStats.health, m_MainStats.hunger, 70);
        BadCondition(ref m_MainStats.health, m_MainStats.thirst, 60);
        BadCondition(ref m_MainStats.health, m_MainStats.infection, 30); 
        yield return new WaitForSeconds(time / Time.timeScale);
        StartCoroutine(BadConditionControl(time));
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
