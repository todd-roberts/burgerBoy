using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("hereeeee!");
        if (collider.gameObject.name == "burgerBoy")
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
