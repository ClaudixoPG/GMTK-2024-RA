using UnityEngine;

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

    private Vector3[] _vertices = new Vector3[8];
    private Vector3[] _lastValidVertices = new Vector3[8];

    void Start()
    {
        InitializeVertices();
        UpdateGraphicTransform();

        
    }

    private void OnEnable()
    {
        _controller.OnScaling += OnScaling;
        _controller.OnEndScaling += OnEndScalling;
    }

    private void OnDisable()
    {
        _controller.OnScaling -= OnScaling;
        _controller.OnEndScaling -= OnEndScalling;
    }

    private void OnScaling(Vector3 axis, float amount, float mouseDeltaMgn)
    {
        Vector3 center = _controller.transform.position;

        amount = -0.25f * Mathf.Sign(amount) * mouseDeltaMgn;

        if (axis == Vector3.one) 
        {
            //float maxDimension = Mathf.Min(
            //    constrain_x.max,
            //    constrain_y.max,
            //    constrain_z.max
            //);

            //float minDimension = Mathf.Max(
            //    constrain_x.min,
            //    constrain_y.min,
            //    constrain_z.min
            //);

            //float newSize = Mathf.Clamp(amount, minDimension, maxDimension);

            //Vector3 min = new Vector3(
            //    center.x - newSize / 2,
            //    center.y - newSize / 2,
            //    center.z - newSize / 2
            //);

            //Vector3 max = new Vector3(
            //    center.x + newSize / 2,
            //    center.y + newSize / 2,
            //    center.z + newSize / 2
            //);

            //_vertices[0] = new Vector3(min.x, min.y, min.z);
            //_vertices[1] = new Vector3(min.x, min.y, max.z);
            //_vertices[2] = new Vector3(min.x, max.y, min.z);
            //_vertices[3] = new Vector3(min.x, max.y, max.z);
            //_vertices[4] = new Vector3(max.x, min.y, min.z);
            //_vertices[5] = new Vector3(max.x, min.y, max.z);
            //_vertices[6] = new Vector3(max.x, max.y, min.z);
            //_vertices[7] = new Vector3(max.x, max.y, max.z);
        }
        else
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                // Right (+X)
                if (axis == Vector3.right && _vertices[i].x > center.x)
                {
                    _vertices[i].x += amount;
                    _vertices[i].x = Mathf.Clamp(_vertices[i].x, center.x + (constrain_x.min / 2), center.x + (constrain_x.max / 2));
                }
                // Left (-X)
                else if (axis == Vector3.left && _vertices[i].x < center.x)
                {
                    _vertices[i].x += amount;
                    _vertices[i].x = Mathf.Clamp(_vertices[i].x, center.x - (constrain_x.max / 2), center.x - (constrain_x.min / 2));
                }
                // Up (+Y)
                else if (axis == Vector3.up && _vertices[i].y > center.y)
                {
                    _vertices[i].y += amount;
                    _vertices[i].y = Mathf.Clamp(_vertices[i].y, center.y + (constrain_y.min / 2), center.y + (constrain_y.max / 2));
                }
                // Down (-Y)
                else if (axis == Vector3.down && _vertices[i].y < center.y)
                {
                    _vertices[i].y += amount;
                    _vertices[i].y = Mathf.Clamp(_vertices[i].y, center.y - (constrain_y.max / 2), center.y - (constrain_y.min / 2));
                }
                // Forward (+Z)
                else if (axis == Vector3.forward && _vertices[i].z > center.z)
                {
                    _vertices[i].z += amount;
                    _vertices[i].z = Mathf.Clamp(_vertices[i].z, center.z + (constrain_z.min / 2), center.z + (constrain_z.max / 2));
                }
                // Backward (-Z)
                else if (axis == Vector3.back && _vertices[i].z < center.z)
                {
                    _vertices[i].z += amount;
                    _vertices[i].z = Mathf.Clamp(_vertices[i].z, center.z - (constrain_z.max / 2), center.z - (constrain_z.min / 2));
                }
            }
        }

        UpdateGraphicTransform();
    }

    private void OnEndScalling()
    {
        if (IsAnyColliderInsideBounds())
        {
            _vertices = new Vector3[_lastValidVertices.Length];

            for (int i = 0; i < _lastValidVertices.Length; i++)
                _vertices[i] = new Vector3(_lastValidVertices[i].x, _lastValidVertices[i].y, _lastValidVertices[i].z);
        }
        else
        {
            _lastValidVertices = new Vector3[_vertices.Length];

            for (int i = 0; i < _vertices.Length; i++)
                _lastValidVertices[i] = new Vector3(_vertices[i].x, _vertices[i].y, _vertices[i].z);
        }

        UpdateGraphicTransform ();
    }

    private bool IsAnyColliderInsideBounds()
    {
        Bounds bounds = new Bounds(_vertices[0], Vector3.zero);
        foreach (Vector3 vertex in _vertices)
        {
            bounds.Encapsulate(vertex);
        }

        int layerMask = ~LayerMask.GetMask("Gizmo");

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.isTrigger)
                continue;

            if (collider.transform == _graphic.GetChild(0))
                continue;

            if (IsColliderIntersectingBounds(collider, bounds))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsColliderIntersectingBounds(Collider collider, Bounds bounds)
    {
        Bounds colliderBounds = collider.bounds;

        return bounds.Intersects(colliderBounds);
    }

    private bool IsColliderInsideBounds(Collider collider, Bounds bounds)
    {
        Bounds colliderBounds = collider.bounds;

        Vector3[] colliderCorners = new Vector3[8];
        colliderCorners[0] = new Vector3(colliderBounds.min.x, colliderBounds.min.y, colliderBounds.min.z);
        colliderCorners[1] = new Vector3(colliderBounds.min.x, colliderBounds.min.y, colliderBounds.max.z);
        colliderCorners[2] = new Vector3(colliderBounds.min.x, colliderBounds.max.y, colliderBounds.min.z);
        colliderCorners[3] = new Vector3(colliderBounds.min.x, colliderBounds.max.y, colliderBounds.max.z);
        colliderCorners[4] = new Vector3(colliderBounds.max.x, colliderBounds.min.y, colliderBounds.min.z);
        colliderCorners[5] = new Vector3(colliderBounds.max.x, colliderBounds.min.y, colliderBounds.max.z);
        colliderCorners[6] = new Vector3(colliderBounds.max.x, colliderBounds.max.y, colliderBounds.min.z);
        colliderCorners[7] = new Vector3(colliderBounds.max.x, colliderBounds.max.y, colliderBounds.max.z);

        foreach (Vector3 corner in colliderCorners)
        {
            if (!bounds.Contains(corner))
            {
                return false;
            }
        }

        return true;
    }

    void UpdateGraphicTransform()
    {
        if (_graphic == null) return;

        Vector3 min = _vertices[0];
        Vector3 max = _vertices[0];

        foreach (Vector3 v in _vertices)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        Vector3 center = (min + max) / 2;
        Vector3 size = max - min;

        _graphic.position = center; 
        _graphic.localScale = size;
    }

    void InitializeVertices()
    {
        Bounds bounds = new Bounds(_controller.transform.position, Vector3.one);

        _vertices[0] = bounds.min;
        _vertices[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        _vertices[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        _vertices[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        _vertices[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        _vertices[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        _vertices[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        _vertices[7] = bounds.max;

        _lastValidVertices = _vertices;

        _lastValidVertices = new Vector3[_vertices.Length];

        for (int i = 0; i < _vertices.Length; i++)
            _lastValidVertices[i] = new Vector3(_vertices[i].x, _vertices[i].y, _vertices[i].z);
    }

    private void OnDrawGizmos()
    {
        if (_vertices == null || _vertices.Length != 8)
            return;

        Gizmos.color = Color.yellow;

        // Dibujar los 12 bordes de la caja usando los vértices
        Gizmos.DrawLine(_vertices[0], _vertices[1]);
        Gizmos.DrawLine(_vertices[0], _vertices[2]);
        Gizmos.DrawLine(_vertices[0], _vertices[4]);
        Gizmos.DrawLine(_vertices[1], _vertices[3]);
        Gizmos.DrawLine(_vertices[1], _vertices[5]);
        Gizmos.DrawLine(_vertices[2], _vertices[3]);
        Gizmos.DrawLine(_vertices[2], _vertices[6]);
        Gizmos.DrawLine(_vertices[3], _vertices[7]);
        Gizmos.DrawLine(_vertices[4], _vertices[5]);
        Gizmos.DrawLine(_vertices[4], _vertices[6]);
        Gizmos.DrawLine(_vertices[5], _vertices[7]);
        Gizmos.DrawLine(_vertices[6], _vertices[7]);
    }
}

