using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordDamage : Damager
{
   
       [SerializeField]  private Transform _attackPoint;
         [SerializeField] private Animator _animator;

     [SerializeField]  private LayerMask _enemyLayers;
     [SerializeField]  private float _attackRange;
    
 [SerializeField] private float _attackInterval = 0.2f;


private float _lastAttackTime;

private void Update()
{
    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
    {
        StartAttack(-1f);
    }
    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
    {
        StartAttack(1f); 
    }
}

private void StartAttack(float direction)
{
    if (Time.time >= _lastAttackTime + _attackInterval)
    {
        transform.localScale = new Vector3(direction, 1, 1);
        Attack();
        _animator.SetTrigger("Attack");
        _lastAttackTime = Time.time;
    }
}
    private void Attack()
    {

          Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(_attackPoint.position,_attackRange, _enemyLayers);

        bool anyDamaged = false;

        foreach (var target in hitEnemies)
        {
            anyDamaged |= TryDoDamage(target);
        }
    }
     private void OnDrawGizmosSelected()
    {
        if(_attackPoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_attackPoint.position,_attackRange);
    }
  
}

