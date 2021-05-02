using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private enum Wave
    {
        FIRST,
        SECOND, 
        THIRD,
    }

    private Wave wave = Wave.FIRST;

    [SerializeField]
    float timetoWaitOnWaves = 60f;
    [SerializeField]
    GameObject[] spawnPoints = new GameObject[4];
    [SerializeField]
    GameObject wolfPrefab = null;

    [SerializeField]
    List<GameObject> _wolves = new List<GameObject>();

    int _wolvesToSpawn = 1;
    float _currentTime = 60.0f;
    

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        SpawnRoutine();
    }

    void SpawnRoutine()
    {
        if (_currentTime >= timetoWaitOnWaves)
        {
            SpawnWolf();
            _currentTime = 0;
            wave++;
            switch (wave)
            {
                case Wave.FIRST:
                    _wolvesToSpawn = 1;
                    break;
                case Wave.SECOND:
                    _wolvesToSpawn = 1;
                    break;
                case Wave.THIRD:
                    _wolvesToSpawn = 2;
                    break;
            }

        }

        _currentTime += Time.fixedUnscaledDeltaTime;
    }

    void SpawnWolf()
    {
        for(int i = 0; i<_wolvesToSpawn; i++)
        {
            var spawnPointToUse = spawnPoints[Random.Range(0, 3)];

            var newWolf = Instantiate(wolfPrefab, spawnPointToUse.transform);

            _wolves.Add(newWolf); 
        }
    }   
}
