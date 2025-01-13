using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ServerPlayerSpawnPoints : MonoBehaviour
{
    public static ServerPlayerSpawnPoints Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private List<GameObject> m_SpawnPoints;
    public GameObject GetRandomSpawnPoint()
    {
        if (m_SpawnPoints.Count == 0)
            return null;
        return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)];
    }
}