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

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 roundedPosition = new Vector3(
           RoundToMultipleOfHalf(transform.position.x),
           RoundToMultipleOfHalf(transform.position.y),
           RoundToMultipleOfHalf(transform.position.z)
       );

        transform.position = roundedPosition;

        _graphic.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, constrain_x.min, constrain_x.max), Mathf.Clamp(transform.localScale.y, constrain_y.min, constrain_y.max), Mathf.Clamp(transform.localScale.z, constrain_z.min, constrain_z.max));
    }

    private float RoundToMultipleOfHalf(float value)
    {
        return Mathf.Round(value * 2) / 2f;
    }
}
