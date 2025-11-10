using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _objectToFleeFrom;
    private bool _isStopped = false, _initialized = false;

    [SerializeField]
    private float _moveSpeed = 3f;

    private Camera _camera;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Color _stoppedColor = Color.yellow;
    private Color _originalColor;

    public Action<EnemyController> OnReset, OnCollided;

    public void Initialize(Transform objectToFleeFrom, Camera camera)
    {
        _camera = camera;
        _objectToFleeFrom = objectToFleeFrom;
        _initialized = true;
    }

    public void Move(Vector3 playerPosition, Vector2 screenBounds)
    {
        if (_isStopped) return;

        Vector2 direction = (transform.position - playerPosition).normalized;

        transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y, screenBounds.y);
        transform.position = viewPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isStopped = true;
            _spriteRenderer.color = _stoppedColor;
            StartCoroutine(ResetAfterDelayCoroutine());
            OnCollided?.Invoke(this); //to stop from updating movement in HordeController
        }
    }

    private IEnumerator ResetAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(6f);
        _objectToFleeFrom = null;
        gameObject.SetActive(false);
        _isStopped = false;
        _initialized = false;
        _spriteRenderer.color = _originalColor;
        OnReset?.Invoke(this); //could return it to pool using this event but not implemented fully
    }
}