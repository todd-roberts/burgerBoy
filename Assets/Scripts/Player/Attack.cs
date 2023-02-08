using System;
using UnityEngine;

public enum AttackName
{
    Basic,
    Power
}

public enum AttackAnimation
{
    Swing,
    Swing2
}

[Serializable]
public class Attack
{
    [field: SerializeField]
    public AttackName Name { get; private set; }

    [field: SerializeField]
    public AttackAnimation Animation { get; private set; }

    [field: SerializeField]
    public int BaseDamage { get; private set; }

    [field: SerializeField]
    public AudioClip AudioClip { get; private set; }

    [field: SerializeField]
    public float AudioDelay { get; private set; }

}