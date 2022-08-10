using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMain : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private HelperDirection _pointerOne;
    [SerializeField] private HelperDirection _pointerTwo;
    [SerializeField] private HelperDirection _pointerThree;
    [SerializeField] private Transform _parentTile;
    [SerializeField] private PrinterState _printer;

    private List<TileOnPlane> _tiles = new List<TileOnPlane>();
    private Material _material;

    private void Awake()
    {
        for (int i = 0; i < _parentTile.childCount; i++)
        {
            _tiles.Add(_parentTile.GetChild(i).GetComponentInChildren<TileOnPlane>());
        }
    }

    private void OnEnable()
    {
        _bag.ChangedMaterial += SetPointer;
    }

    private void Start()
    {
        _material = _printer.Material;
    }

    public TileOnPlane GetTarget()
    {
        TileOnPlane target;
        target = _tiles[Random.Range(0, _tiles.Count)];
        return target;
    }

    public void RemoveTileFromList(TileOnPlane tileOnPlane)
    {
        _tiles.Remove(tileOnPlane);
    }

    private void SetPointer(Material material)
    {
        if (material = _material)
        {
            _pointerOne.enabled = true;
            _pointerTwo.enabled = true;
            _pointerThree.enabled = true;
        }
        else
        {
            _pointerOne.enabled = false;
            _pointerTwo.enabled = false;
            _pointerThree.enabled = false;
        }
    }

}
