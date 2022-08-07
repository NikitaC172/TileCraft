using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBox : MonoBehaviour
{
    [SerializeField] private Transform _pointCollectTile;
    [SerializeField] private GameObject _poolTile;
    [SerializeField] private Animator _Cover;

    private float delayBetweenDrop = 0.1f;
    private bool isWork = false;
    private const string CoverUPAnimation = "CoverUp";
    private const string CloseCoverAnimation = "CloseCover";


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Bag>(out Bag bag))
        {
            if (bag.IsEmpty == false && isWork == false)
            {
                isWork = true;
                StartCoroutine(DropTile(bag));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Bag>(out Bag bag))
        {
            StopCoroutine(DropTile(bag));

            if (isWork == true)
            {
                _Cover.Play(CloseCoverAnimation);
            }

            isWork = false;
        }
    }

    private IEnumerator DropTile(Bag bag)
    {
        var waitSecond = new WaitForSeconds(delayBetweenDrop);
        _Cover.Play(CoverUPAnimation);

        while (bag.IsEmpty == false && isWork == true)
        {
            bag.Unload(_poolTile, _pointCollectTile);
            yield return waitSecond;
        }

        isWork = false;
        _Cover.Play(CloseCoverAnimation);
    }
}
