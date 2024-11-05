using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float sprintSpeed = 2f;
    public float slideSpeed = 0.5f;
    public float slide = 1f;
    public float gravity = 9.8f;

    private Vector3 _moveVector;
    private CharacterController _characterController;
    [SerializeField] private CameraController _cameraController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraController = Camera.main.GetComponent<CameraController>();
        slideSpeed *= speed;
        sprintSpeed *= speed;
    }

    void Update()
    {
        _moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= Vector3.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += Vector3.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        if (_moveVector.magnitude > 0.1f)
        {
            _moveVector.Normalize();
            Vector3 moveDirection = transform.TransformDirection(_moveVector);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _characterController.Move(moveDirection * slideSpeed * Time.fixedDeltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                _characterController.Move(moveDirection * sprintSpeed * Time.fixedDeltaTime);
                StartCoroutine(_cameraController.Shake(0.3f, 0.003f));
            }
            else
            {
                _characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
                StartCoroutine(_cameraController.Shake(0.1f, 0.001f));
            }
        }
    }
}