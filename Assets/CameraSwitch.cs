using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        _camera.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _camera.SetActive(false);
    }
}
