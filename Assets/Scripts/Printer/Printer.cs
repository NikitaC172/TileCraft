using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private Material _materialTile;
    [SerializeField] private Transform _poolTemplateTile;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Animator _animator;
    [SerializeField] private CollectorTile _collector;
    [SerializeField] private ParticleSystem _particleSystem;
    private float delayBetweenCreate = 0.1f;
    private bool isWork = false;
    private bool isActive = true;

    private const string Work = "WorkAnim";
    private const string Idle = "Idle";

    public Material MaterialTile => _materialTile;

    private void OnEnable()
    {
        _collector.Completed += ChangeActive;
    }

    private void OnDisable()
    {
        _collector.Completed -= ChangeActive;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isActive == true && collision.gameObject.TryGetComponent<Bag>(out Bag bag))
        {
            if (isWork == false)
            {
                if (bag.IsFull == false)
                {
                    if (bag.IsEmpty == true)
                    {
                        isWork = true;
                        StartCoroutine(CreateTile(bag));
                    }
                    else if (bag.Material == _materialTile)
                    {
                        isWork = true;
                        StartCoroutine(CreateTile(bag));
                    }
                }
                else
                {
                    bag.ShowMaxSpritel();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Bag>(out Bag bag))
        {
            _animator.Play(Idle);
            StopCoroutine(CreateTile(bag));
            isWork = false;
        }
    }

    private void ChangeActive(CollectorTile collectorTile)
    {
        isActive = false;
    }

    private IEnumerator CreateTile(Bag bag)
    {
        var waitSecond = new WaitForSeconds(delayBetweenCreate);
        _animator.Play(Work);
        _particleSystem.Play();

        while (bag.IsFull != true && isWork == true)
        {
            Tile tile = _poolTemplateTile.GetChild(0).GetComponent<Tile>();
            tile.gameObject.SetActive(true);
            tile.SetMeshRenderer(_materialTile);
            tile.gameObject.transform.parent = _pointSpawn;
            tile.gameObject.transform.localPosition = Vector3.zero;
            bag.Load(tile, _materialTile);
            yield return waitSecond;
        }

        _particleSystem.Stop();
        _animator.Play(Idle);
        StopCoroutine(CreateTile(bag));
        isWork = false;
    }
}
