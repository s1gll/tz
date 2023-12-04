using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] protected float _maxHealth;

    public float Health { get; protected set; }

    public bool IsDead => Health <= 0;
      private bool _isInvulnerable = false;
    private float _invulnerabilityTimer = 0f;
    private const float _invulnerabilityDuration = 0.5f;
    private void Awake()
    {
        Health = _maxHealth;
    }
    private void Update()
    {
      if (_isInvulnerable)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            if (_invulnerabilityTimer <= 0)
            {
                _isInvulnerable = false;
            }
        }
    }

  public void TakeDamage(float damage)
    {
        if (!_isInvulnerable)
        {
            Health -= damage;
          Health = Mathf.Clamp(Health, -1, _maxHealth);

            _isInvulnerable = true;
            _invulnerabilityTimer = _invulnerabilityDuration;
        
        }

    }
}

