using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private AudioSource _audioSource;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Perform()
    {
        PlayDeathSound();
        _particleSystem.Play();
        //todo: destroy death object?
    }

    private void PlayDeathSound()
    {
        _audioSource.Play();
    }

    private void Update()
    {
        Cleanup();
    }

    private void Cleanup()
    {
        bool soundEnded = _audioSource.time >= _audioSource.clip.length;

        bool particlesEnded = _particleSystem.isStopped;

        if (soundEnded && particlesEnded)
        {
            Object.Destroy(gameObject);
        }
    }
}
