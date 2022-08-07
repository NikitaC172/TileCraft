using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void FixedUpdate()
    {
        transform.LookAt(_camera.transform.position, Vector3.up);
    }
}
