using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _poolSize = 1000;

    private Queue<GameObject> _enemyPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _enemyPool = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.SetActive(false);
            _enemyPool.Enqueue(enemy);
        }
    }

    public GameObject SpawnFromPool(Vector3 position)
    {
        if (_enemyPool.Count == 0) return null;

        GameObject enemyToSpawn = _enemyPool.Dequeue();

        enemyToSpawn.SetActive(true);
        enemyToSpawn.transform.position = position;

        return enemyToSpawn;
    }

    public void ReturnToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemyPool.Enqueue(enemy);
    }
}