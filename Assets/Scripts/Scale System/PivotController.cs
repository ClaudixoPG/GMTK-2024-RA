using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PivotController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;

    private InputAction _mousePositionAction;
    private InputAction _mouseLeftButtonAction;

    private List<Transform> _selectedGizmosAxis = new List<Transform>();
    private Transform _overGizmosAxis;
    private GizmoAxis _interactingGizmo;

    private Vector3 _scaleDelta;

    public Vector3 ScaleDelta => -_scaleDelta;

    void Start()
    {
        _selectedGizmosAxis = new List<Transform>();
        _actionAsset.FindActionMap("Gameplay").Enable();
        _mousePositionAction = _actionAsset.FindActionMap("Gameplay").FindAction("Cursor Position");
        _mouseLeftButtonAction = _actionAsset.FindActionMap("Gameplay").FindAction("Left Click");
    }

    void Update()
    {
        GizmoInteractions();
        InteractionController();
    }

    private void OnDisable()
    {
        _selectedGizmosAxis.Clear();
        _overGizmosAxis = null;
        _interactingGizmo = null;
    }

    private void InteractionController()
    {
        var isPressingLeftMouseButton = _mouseLeftButtonAction.IsPressed();

        if (_mouseLeftButtonAction.WasPressedThisFrame() && _overGizmosAxis != null)
        {
            _interactingGizmo = _overGizmosAxis.GetComponent<GizmoAxis>();
        }

        if (isPressingLeftMouseButton && _interactingGizmo != null)
        {
            Vector2 mousePosition = _mousePositionAction.ReadValue<Vector2>();

            Vector3 objectScreenPosition = Camera.main.WorldToScreenPoint(_interactingGizmo.transform.position);

            float distance = mousePosition.x - objectScreenPosition.x;

            var worldMouse = new Vector3(mousePosition.x, mousePosition.y, _interactingGizmo.transform.position.z);

            switch (_interactingGizmo.name)
            {
                case "+X":
                case "-X":
                case "+Z":
                case "-Z":
                    distance = worldMouse.x - objectScreenPosition.x;
                    break;
                case "+Y":
                case "-Y":
                    distance = worldMouse.y - objectScreenPosition.y;
                    break;
            }

            var extends = Mathf.Clamp(Mathf.Abs(distance),0, 100) / 100;

            var mouseSide = objectScreenPosition.x > Camera.main.WorldToScreenPoint(transform.position).x? 1 : -1;

            if (distance >= 0)
            {
                _interactingGizmo.extends = extends * mouseSide;
            }
            else
            {
                _interactingGizmo.extends = -extends * mouseSide;
            }
        }

        if (_mouseLeftButtonAction.WasReleasedThisFrame() && _interactingGizmo != null)
        {
            _interactingGizmo.extends = 0;
            _interactingGizmo = null;
        }

        _scaleDelta = Vector3.zero;

        foreach (Transform child in transform)
        {
            _scaleDelta += child.localPosition;
        }

        _scaleDelta = new Vector3(Mathf.Round(_scaleDelta.x * 2) / 2f, Mathf.Round(_scaleDelta.y * 2) / 2f, Mathf.Round(_scaleDelta.z * 2) / 2f);
    }

    private void GizmoInteractions()
    {
        Vector2 mousePosition = _mousePositionAction.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Gizmo")))
        {
            if (!_selectedGizmosAxis.Contains(hit.collider.transform))
            {
                _selectedGizmosAxis.Add(hit.collider.transform);
            }
            else
            {
                _selectedGizmosAxis.Remove(hit.collider.transform);
                _selectedGizmosAxis.Add(hit.collider.transform);
            }

            _overGizmosAxis = hit.collider.transform;
        }
        else
        {
            _overGizmosAxis = null;
        }

        if (_selectedGizmosAxis.Count == 0) return;

        for (int i = 0; i <= _selectedGizmosAxis.Count - 1; i++)
        {
            if (_selectedGizmosAxis[i] != _overGizmosAxis)
                _selectedGizmosAxis[i].transform.localScale = Vector3.MoveTowards(_selectedGizmosAxis[i].transform.localScale, Vector3.one * 0.1f, Time.deltaTime * 3);
        }

        if(_overGizmosAxis != null)
            _overGizmosAxis.localScale = Vector3.MoveTowards(_overGizmosAxis.localScale, Vector3.one * 0.14f, Time.deltaTime * 2);

        for (int i = _selectedGizmosAxis.Count - 1; i >= 0; i--)
        {
            if (_selectedGizmosAxis[i].localScale == Vector3.one * 0.1f)
            {
                _selectedGizmosAxis.RemoveAt(i);
            }
        }
    }
}
