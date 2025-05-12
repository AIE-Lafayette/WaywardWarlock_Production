using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private InputActionReference _move;
    [SerializeField]
    public float _playerSpeed;
    [SerializeField]
    private LayerMask _layerMask;

    
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    private Transform _meshTransform;
    private void Start()
    {
        _meshTransform = transform.GetChild(0);
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue,_layerMask))
        {
            Vector3 mousePosition = raycastHit.point;

            Vector3 direction = mousePosition - transform.position;
            direction.y = 0f;

            if(direction != Vector3.zero)
            {
                
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                _meshTransform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }

        }

        _moveDirection = new Vector3(_move.action.ReadValue<Vector2>().x, 0,_move.action.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection * _playerSpeed * Time.deltaTime;
        transform.Translate(_playerVelocity, Space.Self);

    }

   



}
