
using UnityEngine;


   public class Damager : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.CompareTag("Enemy"))
    {
        return;
    }
    TryDoDamage(collision);
    }
      protected virtual void OnTriggerStay2D(Collider2D collision)
    {
    if (collision.CompareTag("Enemy"))
    {
        return;
    }
    TryDoDamage(collision);
    }


    protected bool TryDoDamage(Collider2D collision)
    {
        bool hasEnemyHealth = collision.TryGetComponent<Enemy>(out var enemy);
        bool hasPlayerHealth = collision.TryGetComponent<Player>(out var player);

        bool otherHealth = !collision.CompareTag(tag);

        if (otherHealth)
        {
            if (hasEnemyHealth)
            {
                enemy.TakeDamage(_damage);
                return true;
            }
            else if (hasPlayerHealth)
            {
                player.TakeDamage(_damage);
                 return true;
            }
        }
         return false;
    }
}
