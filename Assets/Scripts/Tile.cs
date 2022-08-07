using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRender;
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _defaultMaterial;
    private Vector3 _position;
    private int _angelInBag;
    private int _maxAngel = 15;
    private int _minAngel = -15;
    private GameObject bag = null;
    private float _time = 0;
    private float _pathTimeToPlayer = 0.15f;
    private float _pathTimeToBoard = 1.0f;

    private const string SpinAnimation = "Spin";
    private const string MoveAnimation = "Move";
    private const string SetOnFieldAnimation = "SetOnField";
    private const string IdleAnimation = "Idle";

    public void SetMeshRenderer(Material material)
    {
        _meshRender.material = material;
    }

    public void SetMoveInBag(GameObject gameObject, float positionY)
    {
        _position = new Vector3(0, positionY, 0);
        bag = gameObject;
        _angelInBag = Random.Range(_minAngel, _maxAngel);
        StartCoroutine(MoveToPlayer());
    }

    public void SetMoveToBoard(GameObject parent, Transform pointToMove)
    {
        transform.parent = parent.transform;
        StartCoroutine(MoveToBoard(pointToMove));
    }

    private IEnumerator MoveToBoard(Transform pointToMove)
    {        
        _time = 0;
        _animator.Play(MoveAnimation);  

        while (_time < _pathTimeToBoard)
        {
            _time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pointToMove.position, _time / _pathTimeToBoard);
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        _animator.Play(IdleAnimation);
    }

    private IEnumerator MoveToPlayer()
    {
        _time = 0;
        _animator.Play(SpinAnimation);

        while (_time < _pathTimeToPlayer)
        {
            _time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, bag.transform.position + _position, _time / _pathTimeToPlayer);

            yield return null;
        }

        transform.parent = bag.transform;
        transform.localPosition = _position;
        transform.localEulerAngles = new Vector3(0, _angelInBag, 0);
        _animator.Play(IdleAnimation);
    }
}
