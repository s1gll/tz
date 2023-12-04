using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

 

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _moveSpeed;
    private EnemyKnockback _enemyKnockback;
    public float Health { get; protected set; }
    public bool IsDead => Health <= 0;

    protected Transform target;
    public UnityEvent OnDied;
    public UnityEvent OnTakeDamage;

    protected bool isMovingToPlayer = true; 
    protected bool isMovingLeft = false; 
    protected float movementRange = 0.6f; 

    protected void Awake()
    {
        Health = _maxHealth;
    }

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
        _enemyKnockback = GetComponent<EnemyKnockback>();
        OnTakeDamage.AddListener(ApplyKnockback);
    }

    private void ApplyKnockback()
    {
        Vector2 knockbackDirection = (transform.position - target.position).normalized;
        _enemyKnockback.ApplyKnockback(knockbackDirection);
    }

    private void Update()
    {
        if (isMovingToPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            MoveAroundPlayer();
        }
    }

    protected void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;
        CheckForFlipping(direction);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            isMovingToPlayer = false;
       
        }
    }

    protected void MoveAroundPlayer()
    {
        float movementStep = _moveSpeed * Time.deltaTime;
        float playerDirection = isMovingLeft ? -0.3f : 0.3f;
        transform.Translate(Vector3.left * playerDirection * movementStep);

    
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer > movementRange)
        {
            isMovingLeft = !isMovingLeft;
             if (Vector3.Distance(transform.position, target.position) > 0.4f)
        {
            isMovingToPlayer = true;
       
        } 
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, _maxHealth);

        OnTakeDamage.Invoke();

        if (IsDead)
        {
            OnDead();
        }
    }

    protected virtual void CheckForFlipping(Vector2 direction)
    {
        bool movingLeft = direction.x < 0;
        bool movingRight = direction.x > 0;

        float commonScaleX = Mathf.Abs(transform.localScale.x);

        if (movingLeft)
        {
            transform.localScale = new Vector3(-commonScaleX, transform.localScale.y);
        }
        else if (movingRight)
        {
            transform.localScale = new Vector3(commonScaleX, transform.localScale.y);
        }
    }

    protected virtual void OnDead()
    {
        OnDied?.Invoke();
        gameObject.SetActive(false);
    }

    
}
