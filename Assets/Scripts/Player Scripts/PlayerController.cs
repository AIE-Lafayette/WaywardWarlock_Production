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

<<<<<<< HEAD
=======
    bool _isDead = false;
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
    private Vector3 _angleTwords;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    private Transform _meshTransform;
<<<<<<< HEAD
    private void Start()
    {
=======
    private ForbiddenSpell _forbiddenSpell;
    

    private void Start()
    {
        _forbiddenSpell = GetComponent<ForbiddenSpell>();
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        _meshTransform = transform.GetChild(0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {


        _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection.normalized * _playerSpeed;

    }

    void Update()
    {
<<<<<<< HEAD
=======
        
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        transform.Translate(_playerVelocity * Time.deltaTime, Space.Self);

        if (_angleTwords != Vector3.zero)
        {

            Quaternion lookRotation = Quaternion.LookRotation(_angleTwords);
            _meshTransform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }

    }


    public void LookController(InputAction.CallbackContext context)
    {

<<<<<<< HEAD
=======
        if(_isDead == false)
        {
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

        Vector3 lookPosition = new Vector3(context.action.ReadValue<Vector2>().x, 0, context.action.ReadValue<Vector2>().y) + transform.position;

        _angleTwords = lookPosition - transform.position;
<<<<<<< HEAD
=======
        }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        

    }

    public void LookMouse(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(context.action.ReadValue<Vector2>());
<<<<<<< HEAD
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
=======
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask) && _isDead == false)
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")
        {
            Vector3 mousePosition = raycastHit.point;

            _angleTwords = mousePosition - transform.position;
            _angleTwords.y = 0f;
        }
    }
<<<<<<< HEAD

=======
   public void FreezePlayer()
    {
        _playerSpeed = 0;
        _playerVelocity = new Vector3 ( 0,0,0);
        _isDead = true;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        _forbiddenSpell.SpecialAttack();

    }
>>>>>>> parent of 055ffb5 (Revert "Merge branch 'Dev' into Jack")

}
