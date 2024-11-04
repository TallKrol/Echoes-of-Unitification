using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float sprintSpeed = 2;
    public float slideSpeed = 0.5f;
    public float slide = 1;
    public float gravity = 9.8f;
    

    private Vector3 _moveVector;
    private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        slideSpeed *= speed;
        sprintSpeed *= speed;
    }

    // Update is called once per frame
    void Update()
    {
        _moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
        }
    }
    private void FixedUpdate()
    {
        if (_moveVector.magnitude > 0.1f)
        {
            _moveVector = _moveVector.normalized;
        }

        else
        {
            _moveVector *= slide;

            if (_moveVector.magnitude < 0.1f)
            {
                _moveVector = Vector3.zero;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _characterController.Move(_moveVector * slideSpeed * Time.fixedDeltaTime);
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            _characterController.Move(_moveVector * sprintSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _characterController.Move(_moveVector * speed * Time.fixedDeltaTime);
        }
    }
}
