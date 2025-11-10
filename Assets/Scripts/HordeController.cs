using System.Collections.Generic;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemiesToSpawn = 1000;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Camera _camera;

    private Vector2 _screenBounds;

    private List<EnemyController> _spawnedEnemies = new List<EnemyController>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _screenBounds = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
        SpawnEnemies();
    }

    private void Update()
    {
        Vector3 playerPos = _playerTransform.position;

        //some optimization, better to pass once player position than access transform.position multiple times
        for (int i = 0; i < _spawnedEnemies.Count; i++)
        {
            EnemyController enemy = _spawnedEnemies[i];

            if (enemy != null && enemy.gameObject.activeInHierarchy)
            {
                enemy.Move(playerPos, _screenBounds);
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-_screenBounds.x, _screenBounds.x),
                Random.Range(-_screenBounds.y, _screenBounds.y),
                0);
            GameObject spawnedEnemy = ObjectPooler.Instance.SpawnFromPool(spawnPosition);
            //could use pooled objects for better performance
            EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
            enemyController.Initialize(_playerTransform, _camera);
            _spawnedEnemies.Add(enemyController);
            enemyController.OnCollided += RemoveEnemyFromList;
        }
    }

    public void RemoveEnemyFromList(EnemyController enemyController)
    {
        _spawnedEnemies.Remove(enemyController);
    }
}