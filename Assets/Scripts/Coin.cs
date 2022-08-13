using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string IsActive = "isActive";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CollectorCoin>(out CollectorCoin _collector))
        {
            _animator.SetBool(IsActive, false);
        }
    }
}
