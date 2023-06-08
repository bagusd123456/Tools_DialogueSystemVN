using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    public Transform positionGO;
    public Transform rotationGO;

    protected Quaternion _targetCameraRotation;
    public Camera _targetCamera;
    // Start is called before the first frame update
    void Start()
    {
        if(_targetCamera == null)
        {
            _targetCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _targetCameraRotation = _targetCamera.transform.rotation;
        rotationGO.transform.LookAt(positionGO.transform.position + _targetCameraRotation * Vector3.forward, _targetCameraRotation * positionGO.up);
    }
}
