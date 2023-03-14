using UnityEngine;

public class Creature : MonoBehaviour, IDestructable
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float health = 100;
    protected Animator animator;
    protected Rigidbody2D rigidbody;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    private void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public float Health
    {
        get => health;
        set => health = value;
    }

    public virtual void Die()
    {
        GameController.Instance.Killed(this);
    }

    public void RecieveHit(float dmg)
    {
        Health -= dmg;
        GameController.Instance.Hit(this);
        if (Health <= 0) Die();
    }
    
    protected void DoHit(Vector3 hitPosition, float hitRadius, float hitDamage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);

        for (int i = 0; i < hits.Length; i++)
        {
            if (!GameObject.Equals(hits[i].gameObject, gameObject))
            {
                IDestructable destructable = hits[i].gameObject.GetComponent<IDestructable>();

                if (destructable != null)
                {
                    destructable.RecieveHit(hitDamage);
                }
            }
        }
    }
}