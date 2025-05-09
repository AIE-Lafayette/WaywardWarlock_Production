using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  
    //player movement

    [SerializeField]
    private InputActionReference _move;

    [SerializeField]
    public float _playerSpeed;

    private Vector3 _moveDirection;

    private Vector3 _playerVelocity;

    //camera facing

    [SerializeField]
    public GameObject _targetObject;

    private Vector3 _targetPosition;

    private Vector3 _direction;

    private Ray ray;
    
    public Camera _mainCamera;

    public LayerMask groundLayer;

    //projectile firing

    [SerializeField]
    public float _bulletTimer;

    [SerializeField]
    public float _bulletSpeed;

    [SerializeField]
    public GameObject _projectilePrefab;



    private Transform _meshTransform;

    private float _angle;

    private void Start()
    {
        _meshTransform = transform.GetChild(0);



    }



    void Update()
    {
        
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        Vector3 _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            _targetPosition = hit.point;

            _direction = _targetPosition - transform.position;
            _direction.y = 0f;

            _angle = Vector3.Angle(hit.point, transform.position);

            if (_direction != Vector3.zero)
            {
                _meshTransform.rotation = Quaternion.Euler(new Vector3(0, _angle));

                //var bullet = Instantiate(_projectilePrefab, _targetPosition, lookRotation);
                //bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;




            }


        }
       
        



       
        


        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0,_move.action.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection * _playerSpeed * Time.deltaTime;
        transform.Translate(_playerVelocity, Space.Self);



    }

   



}
