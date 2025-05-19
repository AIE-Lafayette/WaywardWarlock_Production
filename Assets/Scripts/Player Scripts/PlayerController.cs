using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float _playerSpeed;
    [SerializeField]
    private LayerMask _layerMask;

    bool _isDead = false;
    private Vector3 _angleTwords;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    private Transform _meshTransform;
    private void Start()
    {
        _meshTransform = transform.GetChild(0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {


        _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection.normalized * _playerSpeed;

    }

    void Update()
    {
        
        transform.Translate(_playerVelocity * Time.deltaTime, Space.Self);

        if (_angleTwords != Vector3.zero)
        {

            Quaternion lookRotation = Quaternion.LookRotation(_angleTwords);
            _meshTransform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }

    }


    public void LookController(InputAction.CallbackContext context)
    {

        if(_isDead == false)
        {

        Vector3 lookPosition = new Vector3(context.action.ReadValue<Vector2>().x, 0, context.action.ReadValue<Vector2>().y) + transform.position;

        _angleTwords = lookPosition - transform.position;
        }
        

    }

    public void LookMouse(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(context.action.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask) && _isDead == false)
        {
            Vector3 mousePosition = raycastHit.point;

            _angleTwords = mousePosition - transform.position;
            _angleTwords.y = 0f;
        }
    }
   public void FreezePlayer()
    {
        _playerSpeed = 0;
        _playerVelocity = new Vector3 ( 0,0,0);
        _isDead = true;
    }

}
