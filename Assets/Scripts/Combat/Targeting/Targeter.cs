using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    private SphereCollider _sphereCollider;

    public CinemachineTargetGroup targetGroup;

    public List<Target> _possibleTargets = new List<Target>();

    private Target _currentTarget;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target != null)
        {
            _possibleTargets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target != null)
        {
            _possibleTargets.Remove(target);

            if (target == _currentTarget)
            {
                ClearTarget();
            }
        }
    }

    public Target GetCurrentTarget() => _currentTarget;

    private Target[] GetSortedTargets() => _possibleTargets
        .OrderBy(target => Vector3.Distance(transform.parent.position, target.transform.position))
        .ToArray();

    public void TargetInitial()
    {
        if (_currentTarget == null)
        {
            Target();
        }

        LockOn();
    }

    public void Target()
    {
        Target[] targets = GetSortedTargets();

        if (_currentTarget == null)
        {
            _currentTarget = targets[0];
        }
        else
        {
            int currentTargetIndex = Array.IndexOf(targets, _currentTarget);

            int nextTargetIndex = currentTargetIndex == targets.Length - 1 ? 0 : currentTargetIndex + 1;
            _currentTarget = targets[nextTargetIndex];
        }

        LockOn();
    }

    private void LockOn()
    {
        targetGroup.m_Targets[1].target = _currentTarget.transform;
    }


    public void ClearTarget() => _currentTarget = null;

    public bool HasTargets() => _possibleTargets.Count > 0;

    public bool HasValidTarget() => _currentTarget != null;
}
