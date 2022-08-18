using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TileOnPlane : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Transform _pointCollect;
    [SerializeField] private GameObject _tileRoot;
    [SerializeField] private GameObject _whiteBackground;
    [SerializeField] private GameObject _greyBackground;
    [SerializeField] private GameObject _parentCoin;
    [SerializeField] private Animator _animatorTile;
    [SerializeField] private Highlight _highlight;
    [SerializeField] private MeshRenderer _tileRenderer;
    [SerializeField] private Bag _bag;
    [SerializeField] private float CoinDropChancePercent = 50.0f;

    private BoxCollider _collider;
    private bool _isWhiteBackground = true;

    private const string AnimateFill = "SetOnField";

    public event UnityAction<TileOnPlane> Activated;

    public Material Material => _material;
    public Transform PointCollect => _pointCollect;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _tileRenderer.material = _material;
    }

    private void OnEnable()
    {
        _bag.ChangedMaterial += ChangeBackground;
    }

    private void OnDisable()
    {
        _bag.ChangedMaterial -= ChangeBackground;
    }

    public void FillTile()
    {
        Activated?.Invoke(this);
        _collider.enabled = false;
        _tileRoot.SetActive(true);
        _animatorTile.Play(AnimateFill);

        if (Random.Range(0, 100) > CoinDropChancePercent)
        {
            _parentCoin.SetActive(true);
        }
    }

    private void ChangeBackground(Material material)
    {
        if (material == _material)
        {
            _greyBackground.SetActive(true);
            _whiteBackground.SetActive(false);
            _isWhiteBackground = false;
        }
        else if (_isWhiteBackground == false)
        {
            _whiteBackground.SetActive(true);
            _greyBackground.SetActive(false);
            _isWhiteBackground = true;
        }
    }
}
