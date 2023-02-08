using UnityEngine;
using System;

public class Detection : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

    public bool PlayerDetected = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "burgerBoy")
        {
            PlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "burgerBoy")
        {
            PlayerDetected = false;
        }
    }
}
