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

  


    void Update()
    {
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            _targetPosition = hit.point;

            _direction = _targetPosition - transform.position;
            _direction.y = 0f;

            if (_direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

                var bullet = Instantiate(_projectilePrefab, _targetPosition, lookRotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;




            }


        }
       
        



       
        


        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0,_move.action.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection * _playerSpeed * Time.deltaTime;
        transform.Translate(_playerVelocity, Space.Self);



    }

   



}
