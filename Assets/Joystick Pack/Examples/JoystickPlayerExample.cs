using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _variableJoystick;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;

    private bool _isMove = false;
    private float _error = 0.5f;
    private const string MoveAnimation = "Speed";

    public bool IsMove => _isMove;

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;
        _rb.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        _animator.SetFloat(MoveAnimation, _rb.velocity.magnitude);
        _isMove = _rb.velocity.magnitude > _error;

        if (_variableJoystick.Vertical != 0 || _variableJoystick.Horizontal != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
    }
}