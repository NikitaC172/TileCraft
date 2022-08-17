using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private Material _materialTile;
    [SerializeField] private Transform _poolTemplateTile;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Animator _animatorPrinter;
    [SerializeField] private Animator _animatorTile;
    [SerializeField] private CollectorTile _collector;
    [SerializeField] private ParticleSystem _particleSystem;
    private float delayBetweenCreate = 0.05f;
    private bool isWork = false;
    private bool isActive = true;

    private const string Work = "WorkAnim";
    private const string Jump = "jump";
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
            _animatorPrinter.Play(Idle);
            _animatorTile.Play(Idle);
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
        bool isCurrentWork = false;
        var waitSecond = new WaitForSeconds(delayBetweenCreate);

        while (bag.IsFull != true && isWork == true)
        {
            if (bag.IsMove == false && isCurrentWork == false)
            {
                isCurrentWork = true;
                _animatorPrinter.Play(Work);
                _animatorTile.Play(Jump);
                _particleSystem.Play();
            }

            if(bag.IsMove == true && isCurrentWork == true)
            {
                isCurrentWork = false;
                _particleSystem.Stop();
                _animatorPrinter.Play(Idle);
                _animatorTile.Play(Idle);
            }

            if (isCurrentWork == true)
            {
                Tile tile = _poolTemplateTile.GetChild(0).GetComponent<Tile>();
                tile.gameObject.SetActive(true);
                tile.SetMeshRenderer(_materialTile);
                tile.gameObject.transform.parent = _pointSpawn;
                tile.gameObject.transform.localPosition = Vector3.zero;
                bag.Load(tile, _materialTile);
            }

            yield return waitSecond;
        }

        _particleSystem.Stop();
        _animatorPrinter.Play(Idle);
        _animatorTile.Play(Idle);
        StopCoroutine(CreateTile(bag));
        isWork = false;
    }
}
