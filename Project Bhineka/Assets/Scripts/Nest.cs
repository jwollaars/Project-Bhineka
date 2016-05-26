using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nest : MonoBehaviour
{
    private List<GameObject> m_NestMembers = new List<GameObject>();

    [SerializeField]
    private GameObject m_CreaturePrefab;
    [SerializeField]
    private int m_SpawnCreatures = 3;

    void Start()
    {
        m_SpawnCreatures = Random.Range(1, 4);

        for (int i = 0; i < m_SpawnCreatures; i++)
        {
            float randomOffset = Random.Range(-2f, 2f);
            Vector2 spawnPos = new Vector2(transform.position.x + randomOffset, transform.position.y+1);

            GameObject creature = Instantiate(m_CreaturePrefab, spawnPos, transform.rotation) as GameObject;

            float randomScale = Random.Range(-0.5f, 0.5f);
            creature.transform.localScale = new Vector3(creature.transform.localScale.x + randomScale, creature.transform.localScale.y + randomScale, 1);

            creature.name = "Creature";
            creature.transform.parent = gameObject.transform;
            //creature.GetComponent<CreatureController>().Start();
            m_NestMembers.Add(creature);
        }
    }
}
