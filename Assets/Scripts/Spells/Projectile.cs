using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _direction;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private float _maxDistance = 50f;

    private Vector3 _initialPosition;

    public void Init(Vector3 direction)
    {
        _direction = direction;

        _initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction != null)
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, _initialPosition) >= _maxDistance)
        {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        // play hit sound.

        if (collider.gameObject.name == "burgerBoy")
        {
            HitPlayer();
        }
        else
        {
            // Debug.Log(collider.gameObject.name);
        }
    }

    private void HitPlayer()
    {

    }
}
