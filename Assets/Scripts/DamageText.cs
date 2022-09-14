using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshPro>();
    }

    public void Display(int damage)
    {
        _tmp.text = $"{damage}";
    }
}
