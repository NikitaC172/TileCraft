using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoverTileOnPlane : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private GameObject _poolTile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<TileOnPlane>(out TileOnPlane tileOnPlane))
        {
            if(_bag.Material == tileOnPlane.Material)
            {
                if(_bag.IsEmpty == false)
                {
                    _bag.Unload(_poolTile, tileOnPlane.PointCollect);
                    tileOnPlane.FillTile();
                }
            }
        }
    }
}
