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
    [SerializeField]
    public float _fallSpeed;


    bool _isDead = false;
    private Vector3 _angle;
    private Vector3 _moveDirection;
    private Vector3 _playerVelocity;
    private Transform _meshTransform;
    private ForbiddenSpell _forbiddenSpell;
    private CharacterController _playerController;
    private bool _gamePaused;
    

    private void Start()
    {
        _forbiddenSpell = GetComponent<ForbiddenSpell>();
        _playerController = GetComponent<CharacterController>();
        _meshTransform = transform.GetChild(0);
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        _playerVelocity = _moveDirection.normalized * _playerSpeed;
    }

    void Update()
    {
        _playerController.Move(_playerVelocity * Time.deltaTime);
        CheckGround();
        if (_angle != Vector3.zero)
        {

            Quaternion lookRotation = Quaternion.LookRotation(_angle);
            _meshTransform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        }
    }
 
    public void PauseGame(InputAction.CallbackContext context)
    {
        if(!GameManager.instance.GamePaused)
        {
            Time.timeScale = 0;
            GameManager.instance.GamePaused = true;
            UIManager.instance.TogglePauseUI(true);
        }    
        else
        {
            Time.timeScale = 1.0f;
            GameManager.instance.GamePaused = false;
            UIManager.instance.TogglePauseUI(false);
        }
        
    }

    public void LookController(InputAction.CallbackContext context)
    {
        if(!_isDead && !GameManager.instance.GamePaused)
        {
            Vector3 lookPosition = new Vector3(context.action.ReadValue<Vector2>().x, 0, context.action.ReadValue<Vector2>().y) + transform.position;
            _angle = lookPosition - transform.position;
        }
    }
    public void LookMouse(InputAction.CallbackContext context)
    {
        if(!_isDead && !GameManager.instance.GamePaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(context.action.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit Hit, float.MaxValue, _layerMask) && _isDead == false)
            {
                Vector3 mousePosition = Hit.point;
                _angle = mousePosition - transform.position;
                _angle.y = 0f;
            }
        }
    }
    private void CheckGround()
    {
        if(_playerController.isGrounded == false)
        {
            _playerController.Move(-transform.up * _fallSpeed * Time.deltaTime);
        }

    }
    public void FreezePlayer()
    {
        _playerSpeed = 0;
        _playerVelocity = new Vector3 ( 0,0,0);
        _isDead = true;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (_isDead == false)
            _forbiddenSpell.SpecialAttack();

    }

}
