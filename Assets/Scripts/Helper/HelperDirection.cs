using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperDirection : MonoBehaviour
{
    [SerializeField] private GameObject _tileSpriteObject;
    [SerializeField] private GameObject _RemoveSpriteObject;
    [SerializeField] private HelperMain _helperMain;

    //private Transform _transformTarget;
    private TileOnPlane _tileTarget = null;

    private void OnEnable()
    {
        SetDirection(_tileTarget);
    }

    private void FixedUpdate()
    {
        if (_tileTarget != null)
        {
            transform.LookAt(_tileTarget.transform.position, Vector3.up);
        }
    }

    private void OnDisable()
    {
        _tileTarget.Activated -= TryGetNewTarget;
    }

    private void SetDirection(TileOnPlane tileOnPlane)
    {
        _tileTarget = _helperMain.GetTarget();
        _tileTarget.Activated += TryGetNewTarget;
        //_transformTarget = tileOnPlane.transform;
    }

    private void TryGetNewTarget()
    {
        _helperMain.RemoveTileFromList(_tileTarget);
        _tileTarget = null;
        SetDirection(_tileTarget);
    }
}
