using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private GameObject _objectRemoveMarker;

    private void OnBecameVisible()
    {
        _objectRemoveMarker.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        _objectRemoveMarker.SetActive(true);
    }
}
