using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _maxSpawns;
    [SerializeField] float _spawnDelay;
    [SerializeField] Comet _cometPrefab;
    [SerializeField] SpaceShip _shipPrefab;

    List<Transform> _spawners = new List<Transform>();
    PlayerHealth _player;
    float _spawnTimer;
    bool isSpawning;

    void Awake()
    {
        var spawners = GetComponentsInChildren<Transform>();
        foreach (var spawner in spawners)
        {
            _spawners.Add(spawner);
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(IncreaseSpawnRate), 60, 60);
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > _spawnDelay && !isSpawning)
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        isSpawning = true;

        for (int i = 0; i < _maxSpawns; i++)
        {
            Transform spawner = _spawners[Random.Range(0, _spawners.Count)];
            float probability = Random.value;

            if (probability <= .1f)
                Instantiate(_shipPrefab, spawner.position, Quaternion.identity);
            else
                Instantiate(_cometPrefab, spawner.position, Quaternion.identity);

            yield return new WaitForSeconds(Random.value);
        }
        isSpawning = false;
        _spawnTimer = 0;
    }

    void IncreaseSpawnRate()
    {
        _maxSpawns++;
    }
}
