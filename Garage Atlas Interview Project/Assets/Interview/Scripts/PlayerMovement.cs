using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerMovementActions _playerMovementActions;

    [Header("Movement Variables")]
    [SerializeField]
    private float _movementSpeed;
    private float _horizontalInput;
    private float _verticalInput;

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

    void Update()
    {
        // Update the input values with the callback
        _horizontalInput = _playerMovementActions.Player.Movement.ReadValue<Vector2>()[0];
        _verticalInput = _playerMovementActions.Player.Movement.ReadValue<Vector2>()[1];
    }
}
