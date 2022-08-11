using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollectorTile : MonoBehaviour
{
    [SerializeField] private Transform _parentTileOnBoard;
    [SerializeField] private Slider _sliderPrinter;
    [SerializeField] private TextMesh _textPrinter;

    private List<TileOnPlane> _tileOnPlanes = new List<TileOnPlane>();
    private int _maximumNumber = 0;
    private int _count = 0;

    public event UnityAction Completed;

    private void Awake()
    {
        _maximumNumber = _parentTileOnBoard.childCount;
        SetSlider();
        SetText();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _parentTileOnBoard.childCount; i++)
        {
            TileOnPlane tile = _parentTileOnBoard.GetChild(i).GetComponentInChildren<TileOnPlane>();
            tile.Activated += IncreaseNumberActivated;
            tile.Activated += RemoveTileFromList;
            _tileOnPlanes.Add(tile);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _tileOnPlanes.Count; i++)
        {
            _tileOnPlanes[i].Activated -= IncreaseNumberActivated;
            _tileOnPlanes[i].Activated -= RemoveTileFromList;
        }
    }

    public TileOnPlane GetTileOnPlane()
    {
        if (_tileOnPlanes.Count > 0)
        {
            TileOnPlane tileOnPlane = _tileOnPlanes[Random.Range(0, _tileOnPlanes.Count)];
            return tileOnPlane;
        }
        else
        {
            return null;
        }
    }

    private void RemoveTileFromList(TileOnPlane tile)
    {
        _tileOnPlanes.Remove(tile);
    }

    private void IncreaseNumberActivated(TileOnPlane tile)
    {
        _count++;
        SetSlider();
        SetText();

        if (_count == _maximumNumber)
        {
            Completed?.Invoke();
        }
    }

    private void SetSlider()
    {
        float value = (float)_count / _maximumNumber;
        _sliderPrinter.value = value;
    }

    private void SetText()
    {
        int oneHundredPercent = 100;
        int value = oneHundredPercent * _count / _maximumNumber;
        _textPrinter.text = $"{value}%";
    }
}
