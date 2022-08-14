using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComliteNotification : MonoBehaviour
{
    [SerializeField] private List<CollectorTile> _collectorTiles;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private ParticleSystem _compliteEffect;
    [SerializeField] private GameObject _panelComliteNotification;
    private float _delay = 5.0f;

    private void OnEnable()
    {
        foreach (var collectorTile in _collectorTiles)
        {
            collectorTile.Completed += Show;
        }
    }

    private void Show(CollectorTile collectorTile)
    {
        int number = _collectorTiles.IndexOf(collectorTile) + 1;
        _text.text = number.ToString();
        _panelComliteNotification.SetActive(true);
        _compliteEffect.Play();
        Invoke(nameof(SwitchOff), _delay);
    }

    private void SwitchOff()
    {
        _panelComliteNotification.SetActive(false);
    }
}
