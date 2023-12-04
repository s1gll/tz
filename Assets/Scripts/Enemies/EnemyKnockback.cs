using UnityEngine;


public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 2f;
    [SerializeField] private float _knockbackDuration = 0.2f;

    private bool _isKnockback = false;
    private Vector2 _knockbackDirection;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isKnockback)
        {
            _rb.velocity = _knockbackDirection * _knockbackForce;
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        _knockbackDirection = direction.normalized;
        _isKnockback = true;
        Invoke("StopKnockback", _knockbackDuration);
    }

    private void StopKnockback()
    {
        _isKnockback = false;
        _rb.velocity = Vector2.zero;
    }
}
