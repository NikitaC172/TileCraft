using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDirection : MonoBehaviour
{
    [SerializeField] private GameObject _tileSpriteObject;
    [SerializeField] private GameObject _RemoveSpriteObject;
    [SerializeField] private HelperMain _helperMain;

    private TileOnPlane _tileTarget = null;
    private GameObject _target = null;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.LookAt(_target.transform.position, Vector3.up);
        }
    }

    private void OnDisable()
    {
        if (_tileTarget != null)
        {
            _tileTarget.Activated -= TryGetNewTargetTile;
        }
    }

    public void SetDirectionToTrashBox(TrashBox trashBox)
    {
        SetTileTrashBox();
        _target = trashBox.gameObject;
        Debug.LogWarning(_target);
    }

    public void SetDirectionFromTile()
    {
        TileOnPlane tileOnPlane = _helperMain.GetTileOnPlane();

        if (tileOnPlane != null)
        {
            _tileTarget = tileOnPlane;
            SetTileSprite();
            _tileTarget.Activated += TryGetNewTargetTile;
            _target = _tileTarget.gameObject;
        }
    }

    private void SetTileTrashBox()
    {
        _tileSpriteObject.SetActive(false);
        _RemoveSpriteObject.SetActive(true);
    }

    private void SetTileSprite()
    {
        _RemoveSpriteObject.SetActive(false);
        _tileSpriteObject.SetActive(true);
    }

    private void TryGetNewTargetTile(TileOnPlane tileOnPlane)
    {
        _tileTarget.Activated -= TryGetNewTargetTile;
        _tileTarget = null;
        SetDirectionFromTile();
    }
}
