using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostMovementManager : MonoBehaviour
{
    
    public float WalkSpeed = 4f;
    public float RunSpeed = 7f;

    public CharacterController Controller;
    public Animator Animator;

    private Vector3 _direction;
    private float _currentSpeed;
    private bool _isRunning = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _currentSpeed = WalkSpeed;
    }

    private void Update()
    {
        ToggleRun();
    }

    private void FixedUpdate()
    {
        Move();

        if (_direction != Vector3.zero)
        {
            HandleRotation();
        }
    }

    public void ToggleRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_isRunning)
            {
                _isRunning = false;
                _currentSpeed = WalkSpeed;
                Animator.SetBool("isRunning", false);
                return;
            }

            _isRunning = true;
            _currentSpeed = RunSpeed;
            Animator.SetBool("isRunning", true);
        }
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(h, 0, v);

        _direction = _direction.normalized;
        Animator.SetFloat("velocity", _direction.magnitude);


        Vector3 cameraRelativeDirection = Camera.main.transform.TransformDirection(_direction) * _currentSpeed * Time.deltaTime;
        cameraRelativeDirection.y = 0f;

        Controller.Move(cameraRelativeDirection);
    }

    public void HandleRotation()
    {
        float targetRotation = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Quaternion lookAt = Quaternion.Slerp(transform.rotation,
                                      Quaternion.Euler(0, targetRotation, 0),
                                      0.5f);
        transform.rotation = lookAt;

    }
}
