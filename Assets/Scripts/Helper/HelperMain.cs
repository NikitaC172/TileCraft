using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMain : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    //[SerializeField] private HelperDirection _pointerOne;
    //[SerializeField] private HelperDirection _pointerTwo;
    //[SerializeField] private HelperDirection _pointerThree;
    //[SerializeField] private Transform _parentTile;
    [SerializeField] private List<PrinterState> _printers;
    [SerializeField] private List<HelperDirection> _pointers;
    [SerializeField] private List<TrashBox> _TrashBoxes;

    private CollectorTile _collectorTile;
    private Material _material;

    private void Awake()
    {
        //_collectorTile = _printer.GetCollectorTile();
    }

    private void OnEnable()
    {
        _bag.ChangedMaterial += ActivatePointers;
        //_collectorTile.Completed += SetPointerToTrashCart;
    }

    private void OnDisable()
    {
        _bag.ChangedMaterial -= ActivatePointers;
        _collectorTile.Completed -= SetPointerToTrashCart;
    }

    private void Start()
    {
        //_material = _printer.Material;
    }

    /*public TileOnPlane GetTarget()
    {
        TileOnPlane target;
        target = _tiles[Random.Range(0, _tiles.Count)];
        return target;
    }*/

    public TileOnPlane GetTileOnPlane()
    {
        TileOnPlane tileOnPlane = _collectorTile.GetTileOnPlane();

        if (tileOnPlane == null)
        {
            DeactivatePointers();
        }

        return tileOnPlane;
    }

    private void SetPointerToTrashCart()
    {
        for (int i = 0; i < _pointers.Count; i++)
        {
            if (i < _TrashBoxes.Count)
            {
                _pointers[i].gameObject.SetActive(true);
                _pointers[i].SetDirectionToTrashBox(_TrashBoxes[i]);
            }
            else
            {
                _pointers[i].gameObject.SetActive(false);
            }
        }
    }

    private void DeactivatePointers()
    {
        if (_bag.IsEmpty == true)
        { 
            foreach (var pointer in _pointers)
            {
                pointer.gameObject.SetActive(false);
            }
        }
    }

    private void ActivatePointers(Material material)
    {
        _material = material;

        if (_material != null)
        {
            GetPrinterState();

            foreach (var pointer in _pointers)
            {
                pointer.gameObject.SetActive(true);
                pointer.SetDirectionFromTile();
            }
        }
        else
        {
            foreach (var pointer in _pointers)
            {
                pointer.gameObject.SetActive(false);
            }
        }
    }

    private void GetPrinterState()
    {
        foreach (var printer in _printers)
        {
            if (printer.Material == _material)
            {
                _collectorTile = printer.GetCollectorTile();
                _collectorTile.Completed += SetPointerToTrashCart;
            }
        }
    }

}
