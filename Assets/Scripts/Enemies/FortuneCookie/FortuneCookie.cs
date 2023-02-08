using UnityEngine;

public class FortuneCookie : Enemy
{
    public AudioClip CastSound;
    public GameObject spellPrefab;

    [SerializeField]
    private float _spellYOffset = 3f;

    [SerializeField]
    private float _spellZOffset = 3f;

    [field: SerializeField]
    public float RetreatSpeed { get; private set; } = 10f;

    [field: SerializeField]
    public float RetreatDistance { get; private set; } = 10f;

    [field: SerializeField]
    public float RetreatTolerance { get; private set; } = .1f;

    public override EnemyBaseState GetDetectPlayerState() => new RetreatState(this);

    public void Cast()
    {
        GameObject spell = Instantiate(spellPrefab, transform.position, Quaternion.identity);

        spell.transform.position = new Vector3(
            spell.transform.position.x,
            spell.transform.position.y + _spellYOffset,
            spell.transform.position.z
        );

        spell.transform.Translate(transform.forward * _spellZOffset);

        Projectile projectile = spell.GetComponent<Projectile>();

        _audioSource.PlayOneShot(CastSound);

        projectile.Init(transform.forward);
    }
}
