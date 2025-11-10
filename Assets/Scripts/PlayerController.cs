using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 _moveInput;

    [SerializeField]
    private Camera _camera;

    private Vector2 _screenBounds;
    private float _playerWidth, _playerHeight;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _screenBounds = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

        var spriteRenderer = GetComponent<SpriteRenderer>();
        _playerWidth = spriteRenderer.bounds.size.x / 2;
        _playerHeight = spriteRenderer.bounds.size.y / 2;
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = _rigidbody.position + _moveInput.normalized * moveSpeed * Time.fixedDeltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -_screenBounds.x + _playerWidth, _screenBounds.x - _playerWidth);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -_screenBounds.y + _playerHeight, _screenBounds.y - _playerHeight);

        _rigidbody.MovePosition(targetPosition);
    }
}