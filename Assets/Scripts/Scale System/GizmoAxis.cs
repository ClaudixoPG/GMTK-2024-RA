using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoAxis : MonoBehaviour
{
    private Vector3 _originalLocalPosition;

    public float extends;

    void Awake()
    {
        _originalLocalPosition = transform.localPosition;    
    }

    void Update()
    {
        if (extends != 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalLocalPosition + (_originalLocalPosition * extends), Time.deltaTime * 3);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalLocalPosition + (_originalLocalPosition * extends), Time.deltaTime * 8);
        }
    }
}
