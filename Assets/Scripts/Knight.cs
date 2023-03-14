using UnityEngine;

public class Knight : Creature
{
    private static readonly int SpeedTrigger = Animator.StringToHash("Speed");
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static bool _onStair;

    [SerializeField] private float jumpForce;
    [SerializeField] private float stairSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float hitDelay;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform attackPoint;

    private Animator _animator;

    private bool _onGround = true;
    private Rigidbody2D _rigidbody;

    public bool OnStair
    {
        get => _onStair;
        set
        {
            if (value)
                _rigidbody.gravityScale = 0;
            else
                _rigidbody.gravityScale = 1;
            _onStair = value;
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        GameController.Instance.Knight = this;
        GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
    }

    private void Update()
    {
        _onGround = CheckGround();

        _animator.SetFloat(SpeedTrigger, Mathf.Abs(Input.GetAxis("Horizontal")));
        _animator.SetBool(JumpTrigger, !_onGround);

        var velocity = _rigidbody.velocity;
        velocity.x = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = velocity;

        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger(AttackTrigger);
            Invoke(nameof(Attack), hitDelay);
        }

        if (transform.localScale.x < 0)
        {
            if (Input.GetAxis("Horizontal") > 0) transform.localScale = Vector3.one;
        }
        else
        {
            if (Input.GetAxis("Horizontal") < 0) transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump") && _onGround) _rigidbody.AddForce(Vector2.up * jumpForce);

        if (OnStair)
        {
            velocity = _rigidbody.velocity;
            velocity.y = Input.GetAxis("Vertical") * stairSpeed;
            _rigidbody.velocity = velocity;
        }
    }

    private bool CheckGround()
    {
        var hits = Physics2D.LinecastAll(transform.position, groundCheck.position);

        for (var i = 0; i < hits.Length; i++)
            if (!Equals(hits[i].collider.gameObject, gameObject))
                return true;
        return false;
    }

    public void Attack()
    {
        DoHit(attackPoint.position, attackRange, Damage);
    }

    private void HandleOnUpdateHeroParameters(HeroParameters parameters)
    { 
        Health = parameters.MaxHealth;
        Damage = parameters.Damage;
        Speed = parameters.Speed;
    }
    
    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
    }
}