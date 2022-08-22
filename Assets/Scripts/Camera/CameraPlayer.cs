using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    private float _offsetDelay = 0.15f;
    private Vector3 _positionObject;
    private Vector3 _distanceSmoothing;

    private void FixedUpdate()
    {
        _positionObject = _player.transform.position + _offset;
        _distanceSmoothing = Vector3.Lerp(transform.position, _positionObject, _offsetDelay);
        transform.position = _distanceSmoothing;
    }
}
