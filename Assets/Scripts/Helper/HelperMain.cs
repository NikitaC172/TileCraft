using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMain : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private List<PrinterState> _printers;
    [SerializeField] private List<HelperDirection> _pointers;
    [SerializeField] private List<TrashBox> _TrashBoxes;

    private CollectorTile _collectorTile;
    private Material _material;

    private void OnEnable()
    {
        _bag.ChangedMaterial += ActivatePointers;
    }

    private void OnDisable()
    {
        _bag.ChangedMaterial -= ActivatePointers;

        if (_collectorTile != null)
        {
            _collectorTile.Completed -= SetPointerToTrashCart;
        }
    }

    public TileOnPlane GetTileOnPlane()
    {
        TileOnPlane tileOnPlane = _collectorTile.GetTileOnPlane();

        if (tileOnPlane == null)
        {
            DeactivatePointers();
        }

        return tileOnPlane;
    }

    private void SetPointerToTrashCart(CollectorTile collectorTile)
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
