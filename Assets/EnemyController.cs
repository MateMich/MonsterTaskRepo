using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _objectToFleeFrom;
    private bool _isStopped = false, _initialized = false;

    [SerializeField]
    private float _moveSpeed = 3f;

    private Camera _camera;
    private Vector2 _screenBounds;
    private float _enemyWidth;
    private float _enemyHeight;

    public void Initialize(Transform objectToFleeFrom, Camera camera)
    {
        _camera = camera;
        _screenBounds = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));
        _objectToFleeFrom = objectToFleeFrom;
        _initialized = true;
    }

    private void Update()
    {
        if (_isStopped || !_initialized)
        {
            return;
        }

        Vector2 direction = (transform.position - _objectToFleeFrom.position).normalized;
        transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;

        ClampPositionToScreen();
    }

    private void ClampPositionToScreen()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -_screenBounds.x + _enemyWidth, _screenBounds.x - _enemyWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -_screenBounds.y + _enemyHeight, _screenBounds.y - _enemyHeight);
        transform.position = viewPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isStopped = true;
        }
    }
}