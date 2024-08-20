using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScalableObjectGizmoHandler : MonoBehaviour
{
    [SerializeField] private float _outlineWidth = 8;
    [SerializeField] private InputActionAsset _actionAsset;
    private InputAction _mousePositionAction;
    private InputAction _mouseLeftButtonAction;

    private Outline _over;

    private PivotController _current;

    private void Start()
    {
        _actionAsset.FindActionMap("Gameplay").Enable();
        _mousePositionAction = _actionAsset.FindActionMap("Gameplay").FindAction("Cursor Position");
        _mouseLeftButtonAction = _actionAsset.FindActionMap("Gameplay").FindAction("Left Click");
    }

    private void Update()
    {
        var mousepos = _mousePositionAction.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousepos);
        int layerMask = LayerMask.GetMask("ScalableObject");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            var over = hit.transform.GetComponent<Outline>();

            if (over != null)
            {
                if (_over != over)
                {
                    if (_over != null)
                        _over.OutlineWidth = 0;

                    _over = over;
                    _over.OutlineWidth = _outlineWidth;
                }
            }
        }
        else
        {
            if (_over != null)
            {
                _over.OutlineWidth = 0;
                _over = null;
            }
        }

        if (_mouseLeftButtonAction.WasPerformedThisFrame())
        {
            if (_current != null && _current.IsOver)
                return;

            if (_over != null)
            {
                var pivot = _over.transform.parent.GetComponentInParent<ScalableObject>().GetComponentInChildren<PivotController>(true);

                if (pivot != null && pivot != _current)
                {
                    if (_current != null)
                        _current.gameObject.SetActive(false);

                    _current = pivot;

                    _current.gameObject.SetActive(true);
                }
            }
        }

        if (_mouseLeftButtonAction.WasReleasedThisFrame())
        {
            if (_over == null)
            {
                if (_current != null && !_current.IsScaling)
                {
                    _current.gameObject.SetActive(false);
                    _current = null;
                }
            }
        }
    }
}
