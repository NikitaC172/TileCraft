using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TileOnPlane _tile;

    private bool _isActive = false;

    private const string AnimationHighlight = "Highlight";

    private void OnEnable()
    {
        _tile.Activated += SetActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isActive == true && other.TryGetComponent<Bag>(out Bag bag))
        {
            Show();
        }
    }

    private void SetActive(TileOnPlane tileOnPlane)
    {
        _isActive = true;
    }

    private void Show()
    {
        _animator.Play(AnimationHighlight);
    }
}
