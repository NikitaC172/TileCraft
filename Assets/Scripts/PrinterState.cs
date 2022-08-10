using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterState : MonoBehaviour
{
    [SerializeField] private GameObject _numberImage;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _cover;
    [SerializeField] private GameObject _blockCover;
    [SerializeField] private Bag _bag;
    [SerializeField] private Printer _printer;
    [SerializeField] private CollectorTile _collector;

    private Material _material;
    private bool _isActive = true;

    public Material Material  => _material;

    private void Awake()
    {
        _material = _printer.MaterialTile;
    }

    private void OnEnable()
    {
        _bag.ChangedMaterial += ChangeState;
        _collector.Completed += SetCoverComplite;
    }

    private void OnDisable()
    {
        _bag.ChangedMaterial -= ChangeState;
        _collector.Completed -= SetCoverComplite;
    }

    private void ChangeState(Material material)
    {
        if (_isActive == true)
        {
            if (material == _material || material == null)
            {
                _lockImage.SetActive(false);
                _blockCover.SetActive(false);
                _numberImage.SetActive(true);
            }
            else if (material != _material)
            {
                _numberImage.SetActive(false);
                _lockImage.SetActive(true);
                _blockCover.SetActive(true);
            }
        }
    }

    private void SetCoverComplite()
    {
        _lockImage.SetActive(false);
        _blockCover.SetActive(false);
        _numberImage.SetActive(true);
        _cover.SetActive(true);
        _isActive = false;
    }
}
