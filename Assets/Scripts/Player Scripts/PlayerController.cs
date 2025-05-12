using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  
    [SerializeField]
    public float _bulletTimer;
    [SerializeField]
    public float _bulletSpeed;
    [SerializeField]
    public GameObject _projectilePrefab;
    [SerializeField]
    private InputActionReference _move;
    [SerializeField]
    public float _playerSpeed;

    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    private Transform _meshTransform;
    private float _angle;
    public Camera _mainCamera;

    private void Start()
    {
        _meshTransform = transform.GetChild(0);



    }



    void Update()
    {
        
       

        Vector3 _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);


        //var bullet = Instantiate(_projectilePrefab, _targetPosition, lookRotation);
        //bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;



      

       

        





        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0,_move.action.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection * _playerSpeed * Time.deltaTime;
        _meshTransform.Translate(_playerVelocity, Space.Self);



    }

   



}
