using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float _playerSpeed;

    [SerializeField]
    public float _bulletTimer;

    [SerializeField]
    public GameObject _projectile;

    [SerializeField]
    public GameObject _targetObject;


    [SerializeField]
    private InputActionReference _move;

    private Vector3 _relativePosition;

    public Vector3 _mousePosition;

    private Vector3 _moveDirection;

    private Vector3 _playerVelocity;
  
  


    void Update()
    {
       
        _mousePosition = Mouse.current.position.ReadValue();
        _relativePosition = Camera.main.ScreenToWorldPoint(_mousePosition);



        Debug.Log(_relativePosition.ToString());
        


        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0,_move.action.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection * _playerSpeed * Time.deltaTime;
        transform.Translate(_playerVelocity, Space.Self);



    }

   



}
