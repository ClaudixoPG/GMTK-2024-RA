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
    private Vector3 _lastScale;

    void Start()
    {
        _originalPos = _graphic.position;
        _originalScale = _graphic.localScale;
        _lastScale = _graphic.localScale;
    }

    private void OnEnable()
    {
        _controller.onEndScaling += UpdateLastScale;
    }

    private void OnDisable()
    {
        _controller.onEndScaling -= UpdateLastScale;
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

            Vector3 newScale = /*_originalScale*/_lastScale + Vector3.Scale(absScaleFactor, maxScale - _lastScale /*_originalScale*/);

            newScale = Vector3.Max(newScale, minScale);
            newScale = Vector3.Min(newScale, maxScale);

            Vector3 _scaleDifference = Vector3.one * 0.5f; /*newScale - _lastScale/*_originalScale*/;
            Vector3 scaleDifference = Vector3.one * 0.5f; /*newScale - _lastScale/*_originalScale*/;

            Vector3 positionOffset = new Vector3(
                (scaleFactor.x < 0) ? scaleDifference.x * -0.5f : scaleDifference.x * 0.5f,
                (scaleFactor.y < 0) ? scaleDifference.y * -0.5f : scaleDifference.y * 0.5f,
                (scaleFactor.z < 0) ? scaleDifference.z * -0.5f : scaleDifference.z * 0.5f
            );

            _graphic.localScale = newScale;
            _graphic.position = _originalPos + positionOffset;
        }
    }

    private void UpdateLastScale()
    {
        _lastScale = _graphic.localScale;

        var bounds = new Bounds(_graphic.transform.position, _graphic.localScale);

        _controller.transform.position = bounds.center;
    }
}
