using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour
{
     [SerializeField] protected float _maxHealth;

    public float Health { get; protected set; }

    public bool IsDead => Health <= 0;
       protected void Awake()
    {
        Health = _maxHealth;
    }
    public virtual void TakeDamage(float damage)
    {

    }
}
