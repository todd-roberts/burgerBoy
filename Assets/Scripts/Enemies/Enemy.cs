using UnityEngine;
using BurgerBoy;
using System.Collections;

public abstract class Enemy : StateMachine
{
    public Character Character;

    public Detection Detection;

    public CharacterController CharacterController;


    public GameObject ItemDropPrefab;

    [SerializeField]
    private float itemDropChance = 0.5f;

    [SerializeField]
    private float itemDropYOffset = 2f;
    public GameObject DamageTextPrefab;

    [SerializeField]
    private float _stunDuration = 1f;

    private EnemyDeath _death;
    protected Player _player;
    protected AudioSource _audioSource;

    private Target _target;

    private Targeter _targeter;

    [SerializeField]
    private AudioClip damageClip1;

    [SerializeField]
    private AudioClip damageClip2;

    [SerializeField]
    private float _damageTextYOffset = 5f;

    public int maxHealth;
    private int _currentHealth { get; set; }

    private float _currentGravity = 0f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _target = GetComponent<Target>();

        CharacterController = GetComponent<CharacterController>();

        _death = GetComponentInChildren<EnemyDeath>();
        _death.gameObject.SetActive(false);

        _currentHealth = maxHealth;
    }

    private void Start()
    {
        _targeter = FindObjectOfType<Targeter>();
        _player = FindObjectOfType<Player>();

        _globalState = new EnemyGlobalState(this);

        SwitchState(new EnemyIdleState(this));
    }

    public Vector3 GetPlayerPosition() => _player.transform.position;

    public abstract EnemyBaseState GetDetectPlayerState();

    public void ReceiveAttack(Attack attack)
    {
        SwitchState(new EnemyDamagedState(this));

        PlayDamageSound(attack);

        DisplayDamage(attack.BaseDamage);

        _currentHealth -= attack.BaseDamage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetStunDuration() => _stunDuration;

    private void PlayDamageSound(Attack attack)
    {
        AudioClip damageClip = attack.Name == AttackName.Power ? damageClip2 : damageClip1;

        _audioSource.PlayOneShot(damageClip);
    }

    private void DisplayDamage(int damage)
    {
        Vector3 textPosition = new Vector3(
            transform.position.x,
            transform.position.y + _damageTextYOffset,
            transform.position.z
        );

        GameObject text = Instantiate(DamageTextPrefab, textPosition, Quaternion.identity, transform.parent);

        DamageText dt = text.GetComponent<DamageText>();

        dt.Display(damage);
    }

    protected virtual void Die()
    {
        _targeter?.RemoveTarget(_target);

        _death.gameObject.SetActive(true);
        _death.transform.parent = transform.parent;
        _death.Perform();

        if (ItemDropPrefab != null && Random.Range(0f, 1f) <= itemDropChance)
        {
            Instantiate(ItemDropPrefab, transform.position + Vector3.up * itemDropYOffset, Quaternion.identity, transform.parent);
        }

        Object.Destroy(gameObject);
    }

    public void ApplyGravity()
    {
        if (CharacterController.isGrounded)
        {
            _currentGravity = 0;
        }
        else
        {
            _currentGravity += Constants.GRAVITY;
            CharacterController.Move(new Vector3(0, _currentGravity, 0) * Time.deltaTime);
        }
    }

}
