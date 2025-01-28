using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedPlayer;

    private Vector3 _moveInput;

    private Rigidbody _rb;
    private Joystick _joystick;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        Movement(_moveInput);
    }

    private void Movement(Vector3 move)
    {
        Vector3 moveInput = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);

        Vector3 moveDirection = transform.TransformDirection(moveInput.normalized);

        _rb.velocity = new Vector3(moveDirection.x * _speedPlayer, _rb.velocity.y, moveDirection.z * _speedPlayer);
    }
}
