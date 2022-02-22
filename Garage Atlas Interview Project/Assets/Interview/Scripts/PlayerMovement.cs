using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _cameraMainTransform;
    private PlayerMovementActions _playerMovementActions;

    [Header("Movement Variables")]
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    private float _movementAngle;
    private float _horizontalInput;
    private float _verticalInput;

    [Header("State Machine")]
    private playerState _playerState;
    enum playerState
    {
        idle,
        moving
    }

    private void Awake()
    {
        _playerMovementActions = new PlayerMovementActions();
    }

    private void OnEnable()
    {
        _playerMovementActions.Enable();
    }

    private void OnDisable()
    {
        _playerMovementActions.Disable();
    }

    private void Start()
    {
        if (_characterController == null || _animator == null)
        {
            Debug.Log("PlayerMovements - A component is missing.");
            return;
        }
        if (Camera.main == null)
        {
            Debug.Log("There is no camera on scene");
            return;
        }
        _cameraMainTransform = Camera.main.transform;
    }
    void Update()
    {
        // Update the input values with the callback
        _horizontalInput = _playerMovementActions.Player.Movement.ReadValue<Vector2>()[0];
        _verticalInput = _playerMovementActions.Player.Movement.ReadValue<Vector2>()[1];

        // Set the pseudo velocity vector according to input and camera position
        Vector3 move = new Vector3(_horizontalInput, 0, _verticalInput);
        move = _cameraMainTransform.forward * move.z + _cameraMainTransform.right * move.x;
        move.y = 0;
        _characterController.Move(move * Time.deltaTime * _movementSpeed);

        // Set player state
        if (move != Vector3.zero && _playerState == playerState.idle)
        {
            _playerState = playerState.moving;
        }
        else if (move == Vector3.zero && _playerState == playerState.moving)
        {
            _playerState = playerState.idle;
        }

        // Handle moving state logic
        if (_playerState == playerState.moving)
        {
            _animator.Play("Walking");
            // Relocation
            gameObject.transform.forward = move;
            // Rotation
            _movementAngle = Mathf.Atan2(_horizontalInput, _verticalInput) * Mathf.Rad2Deg + _cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, _movementAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }

        // Handle idle state logic
        else if (_playerState == playerState.idle)
        {
            _animator.Play("Idle");
        }
    }
}
