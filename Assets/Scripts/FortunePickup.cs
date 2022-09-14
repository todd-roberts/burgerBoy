using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortunePickup : MonoBehaviour
{
    private FortuneUI _fortuneUI;

    private void Start()
    {
        _fortuneUI = FindObjectOfType<FortuneUI>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {

    }
}
