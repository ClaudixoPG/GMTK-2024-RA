using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GizmoLines : MonoBehaviour
{
    private LineRenderer[] _lines;

    void Update()
    {
        if (_lines == null)
            _lines = GetComponentsInChildren<LineRenderer>();

        foreach (var line in _lines)
        {
            line.positionCount = 2;
            line.SetPosition(0, line.transform.position);
            line.SetPosition(1, transform.position);
        }
    }
}
