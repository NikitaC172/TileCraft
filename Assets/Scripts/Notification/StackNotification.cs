using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackNotification : MonoBehaviour
{
    [SerializeField] private GameObject _panelWithNotifaction;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private Bag _bag;
    [SerializeField] private TextMeshProUGUI _text;
    private float delay = 6.0f;
    private bool isActive = true;

    private int _numberForText = 0;

    private void OnEnable()
    {
        _bag.ChangedMaterial += TryStart;
    }

    private void OnDisable()
    {
        _bag.ChangedMaterial -= TryStart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Printer>(out Printer printer) || other.TryGetComponent<TrashBox>(out TrashBox trashBox))
        {
            isActive = false;
            _panelWithNotifaction.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Printer>(out Printer printer) || other.TryGetComponent<TrashBox>(out TrashBox trashBox))
        {
            isActive = true;
        }
    }

    private void TryStart(Material materialInBag)
    {
        if (materialInBag != null)
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                if (materialInBag == _materials[i])
                {
                    _numberForText = i + 1;
                    _text.text = _numberForText.ToString();
                }
            }
        }

        if (isActive == true)
        {
            if (materialInBag == null)
            {
                _panelWithNotifaction.SetActive(true);
                Invoke(nameof(SwitchOff), delay);
            }
        }
    }

    private void SwitchOff()
    {
        _panelWithNotifaction.SetActive(false);
    }
}
