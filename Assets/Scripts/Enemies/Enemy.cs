using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject ItemDropPrefab;
    public GameObject DamageTextPrefab;

    private EnemyDeath _death;
    private Player _player;
    private AudioSource _audioSource;

    private Animator _animator;

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


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _target = GetComponent<Target>();

        _death = GetComponentInChildren<EnemyDeath>();
        _death.gameObject.SetActive(false);

        _currentHealth = maxHealth;
    }

    private void Start()
    {
        _targeter = FindObjectOfType<Targeter>();
        _player = FindObjectOfType<Player>();
    }

    public void ReceiveAttack(Attack attack)
    {
        PlayDamageAnimation();
        PlayDamageSound(attack);
        DisplayDamage(attack.BaseDamage);

        _currentHealth -= attack.BaseDamage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageAnimation()
    {
        _animator.Play("Damage");
    }

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

        Object.Destroy(gameObject);
    }


    void Update()
    {
        Vector3 lookVector = transform.position - _player.transform.position;
        lookVector.y = 0f;

        transform.rotation = Quaternion.LookRotation(lookVector * -1);
    }
}
