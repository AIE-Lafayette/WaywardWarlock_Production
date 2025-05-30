using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hover : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 30;
    Transform _mesh;
    Vector3 _center;

    // Start is called before the first frame update
    void Start()
    {
        _center = GetComponentInChildren<Renderer>().bounds.center;
        _mesh = transform.GetChild(1);
        
        if(_mesh)
        {
            
            _mesh.DOMoveY(1, .5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        }
    }

    private void Update()
    {
        _mesh.RotateAround(_center, new Vector3(0, 1, 0), _rotationSpeed * Time.deltaTime);
    }
}
