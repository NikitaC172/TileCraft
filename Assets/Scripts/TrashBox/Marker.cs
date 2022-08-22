using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private GameObject _objectRemoveMarker;

    private void OnBecameVisible()
    {
        if (_objectRemoveMarker != null)
        {
            _objectRemoveMarker.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        if (_objectRemoveMarker != null)
        {
            _objectRemoveMarker.SetActive(true);
        }
    }
}
