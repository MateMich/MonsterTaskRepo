using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemiesToSpawn = 1000;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Camera _camera;

    private Vector2 _screenBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _screenBounds = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-_screenBounds.x, _screenBounds.x),
                Random.Range(-_screenBounds.y, _screenBounds.y),
                0);
            GameObject spawnedEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            //could use pooled objects for better performance
            spawnedEnemy.GetComponent<EnemyController>().Initialize(_playerTransform, _camera);
        }
    }
}