using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] int _maxCountTile = 30;
    [SerializeField] Material _material = null;
    [SerializeField] GameObject _maxSprite = null;
    [SerializeField] JoystickPlayerExample _characterBody;

    private int _countTile = 0;
    private float _stepBetweenCell = 0.15f;
    private bool _isFull = false;
    private bool _isEmpty = true;

    public bool IsFull => _isFull;
    public bool IsEmpty => _isEmpty;
    public Material Material => _material;

    public void Load(Tile tile, Material material)
    {
        float positionY = _countTile * _stepBetweenCell;

        if (_material == null)
        {
            _material = material;
        }

        _isEmpty = false;
        tile.SetMoveInBag(gameObject, positionY);
        _countTile++;

        if (_countTile == _maxCountTile)
        {
            _isFull = true;
            ShowMaxSpritel();
        }
    }

    public void ShowMaxSpritel()
    {
        float offsetForSprite = 0.3f;
        float positionY = _countTile * _stepBetweenCell + offsetForSprite;        
        _maxSprite.transform.localPosition = new Vector3(0, positionY, 0);
        _maxSprite.SetActive(true);
        StartCoroutine(WaitMovement());
    }

    public void Unload(GameObject nextParentObject, Transform pointCollect)
    {
        if (_countTile > 0)
        {
            gameObject.transform.GetChild(_countTile - 1).TryGetComponent<Tile>(out Tile tile);
            if (tile != null)
            {
                tile.SetMoveToBoard(nextParentObject, pointCollect);
                _countTile--;
            }
            else
            {
                Debug.LogError("Bag: " + "Кол-во: " + _countTile + " !!! Счетчик: " + gameObject.transform.childCount);
            }
        }
        else
        {
            _isEmpty = true;
            _isFull = false;
        }
    }

    private IEnumerator WaitMovement()
    {
        yield return new WaitWhile(() => _characterBody.IsMove == false);
        _maxSprite.SetActive(false);
    }
}
