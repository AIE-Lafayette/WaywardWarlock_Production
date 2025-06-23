using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 30;
    [SerializeField]
    float _strength = 0.5f;
    [SerializeField]
    float _speed = 1f;
    Vector3 _startPosition;
    Transform _mesh;
    Vector3 _center;

    // Start is called before the first frame update
    void Start()
    {
        _center = GetComponentInChildren<Renderer>().bounds.center;
        _mesh = transform.GetChild(1);
        _startPosition = _mesh.transform.position;
        _mesh.transform.position = transform.position;
    }

    void HeightChange()
    {
        float newY = _startPosition.y + Mathf.Sin(Time.time * _speed) * _strength;
        _mesh.transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
    }

    private void Update()
    {
        HeightChange();
        _mesh.RotateAround(_center, new Vector3(0, 1, 0), _rotationSpeed * Time.deltaTime);
        
    }

}
