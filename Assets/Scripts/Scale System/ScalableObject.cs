using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalableObject : MonoBehaviour
{
    [System.Serializable]
    public struct Constrain
    {
        public float min, max;
    }

    public Constrain constrain_x;
    public Constrain constrain_y;
    public Constrain constrain_z;

    [SerializeField] private PivotController _controller;
    [SerializeField] private Transform _graphic;

    private Vector3 _originalPos;
    private Vector3 _originalScale;

    void Start()
    {
        _originalPos = _graphic.position;
        _originalScale = _graphic.localScale;
    }

    void Update()
    {
        if (_controller.IsScaling)
        {
            var scaleFactor = Vector3.ClampMagnitude(_controller.ScaleDelta, 1f);

            Vector3 absScaleFactor = new Vector3(
                Mathf.Abs(scaleFactor.x),
                Mathf.Abs(scaleFactor.y),
                Mathf.Abs(scaleFactor.z)
            );

            var minScale = new Vector3(constrain_x.min, constrain_y.min, constrain_z.min);
            var maxScale = new Vector3(constrain_x.max, constrain_y.max, constrain_z.max);

            Vector3 newScale = _originalScale + Vector3.Scale(absScaleFactor, maxScale - _originalScale);

            newScale = Vector3.Max(newScale, minScale);
            newScale = Vector3.Min(newScale, maxScale);

            Vector3 scaleDifference = newScale - _originalScale;

            Vector3 positionOffset = new Vector3(
                (scaleFactor.x < 0) ? scaleDifference.x * -0.5f : scaleDifference.x * 0.5f,
                (scaleFactor.y < 0) ? scaleDifference.y * -0.5f : scaleDifference.y * 0.5f,
                (scaleFactor.z < 0) ? scaleDifference.z * -0.5f : scaleDifference.z * 0.5f
            );

            _graphic.localScale = newScale;
            _graphic.position = _originalPos + positionOffset;
        }
    }
}
